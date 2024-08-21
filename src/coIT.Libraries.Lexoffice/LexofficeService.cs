using System.Collections.Immutable;
using coIT.Libraries.LexOffice.DataContracts.Contacts;
using coIT.Libraries.LexOffice.DataContracts.Country;
using coIT.Libraries.LexOffice.DataContracts.Invoice;
using coIT.Libraries.LexOffice.DataContracts.Voucher;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Polly;

namespace coIT.Libraries.LexOffice;

public class LexofficeService : IInvoiceService
{
    private readonly HttpClient _client;

    private static DateTime _lastRequest = DateTime.MinValue;
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public LexofficeService(string accessToken)
    {
        _client = new HttpClient
        {
            DefaultRequestHeaders =
            {
                { "Authorization", $"Bearer {accessToken}" },
                { "Accept", "application/json" }
            }
        };
    }

    public async Task<ImmutableList<Voucher>> GetVouchersInPeriod(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken = default
    )
    {
        var lexOfficeVouchers = await GetAllInvoiceVouchersAsync(cancellationToken);

        var vouchersInPeriod = lexOfficeVouchers
            .Where(voucher =>
                DateOnly.FromDateTime(voucher.VoucherDate) >= start
                && DateOnly.FromDateTime(voucher.VoucherDate) <= end
            )
            .ToImmutableList();
        return vouchersInPeriod;
    }

    public async Task<IImmutableList<Voucher>> GetAllInvoiceVouchersAsync(
        CancellationToken cancellationToken = default
    )
    {
        var vouchers = new List<Voucher>();

        var type = VoucherType.Invoice;
        var status =
            VoucherStatus.Paid | VoucherStatus.Paidoff | VoucherStatus.Open | VoucherStatus.Voided;

        var wrapper = await GetVouchersAsync(type, status, 0, 250, cancellationToken)
            .ConfigureAwait(false);
        vouchers.AddRange(wrapper.Content);

        for (var page = 1; page < wrapper.TotalPages; page++)
        {
            var pageWrapper = await GetVouchersAsync(type, status, page, 250, cancellationToken)
                .ConfigureAwait(false);
            vouchers.AddRange(pageWrapper.Content);
        }

        return vouchers.ToImmutableList();
    }

    public async Task<IImmutableList<Invoice>> GetInvoicesAsync(
        IImmutableList<Voucher> vouchers,
        CancellationToken cancellationToken = default
    )
    {
        var invoices = new List<Invoice>();

        foreach (var voucher in vouchers)
        {
            var neueInvoice = await GetInvoiceAsync(voucher.Id, cancellationToken)
                .ConfigureAwait(false);
            invoices.Add(neueInvoice);
        }

        return invoices.ToImmutableList();
    }

    private async Task<VoucherResponseWrapper> GetVouchersAsync(
        int type,
        int status,
        int page = 0,
        int size = 250,
        CancellationToken cancellationToken = default
    )
    {
        var voucherTypeString = VoucherType.FromValueToString(type).Replace(" ", "");
        var statusTypeString = VoucherStatus.FromValueToString(status).Replace(" ", "");

        var uri = LexofficeApiAddressesBuilder.AllVouchersUri(
            voucherTypeString,
            statusTypeString,
            page,
            size
        );

        var response = await GetWithRateLimitingAsync(uri, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var contents = await response
            .Content.ReadAsStringAsync(cancellationToken)
            .ConfigureAwait(false);

        return JsonConvert.DeserializeObject<VoucherResponseWrapper>(contents);
    }

    public async Task<List<ContactInformation>> GetContactsAsync(
        CancellationToken cancellationToken = default
    )
    {
        var contactInformation = new List<ContactInformation>();

        var wrapper = await GetContactsAsync(0, 250, cancellationToken).ConfigureAwait(false);
        ;
        contactInformation.AddRange(wrapper.ContactInformation);

        for (var page = 1; page < wrapper.TotalPages; page++)
        {
            var pageWrapper = await GetContactsAsync(page, 250, cancellationToken)
                .ConfigureAwait(false);
            ;
            contactInformation.AddRange(pageWrapper.ContactInformation);
        }

        await AddCountriesToContactAddresses(contactInformation).ConfigureAwait(false);
        ;
        FixIncorrectlyFormattedZips(contactInformation);
        return contactInformation;
    }

    private async Task<ContactsResponseWrapper> GetContactsAsync(
        int page,
        int size = 250,
        CancellationToken cancellationToken = default
    )
    {
        var uri = LexofficeApiAddressesBuilder.ContactsUri(page, size);

        var response = await GetWithRateLimitingAsync(uri, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var contents = await response
            .Content.ReadAsStringAsync(cancellationToken)
            .ConfigureAwait(false);

        return JsonConvert.DeserializeObject<ContactsResponseWrapper>(contents);
    }

    private void FixIncorrectlyFormattedZips(List<ContactInformation> contacts)
    {
        foreach (var contact in contacts)
        {
            contact.Addresses?.Billing?.ForEach(FixZipForContactAddress);
            contact.Addresses?.Shipping?.ForEach(FixZipForContactAddress);
        }
    }

    private void FixZipForContactAddress(ContactAddress address)
    {
        address.Zip = ZipFix.AddLeadingZeroes(address.Zip, address.Countrycode);
    }

    private async Task AddCountriesToContactAddresses(
        List<ContactInformation> contacts,
        CancellationToken cancellationToken = default
    )
    {
        var countries = await GetAllCountries(cancellationToken).ConfigureAwait(false);

        AddCountriesToAddresses(
            contacts
                .Where(contact => contact.Addresses.Shipping is not null)
                .SelectMany(contact => contact.Addresses.Shipping),
            countries
        );

        AddCountriesToAddresses(
            contacts
                .Where(contact => contact.Addresses.Billing is not null)
                .SelectMany(contact => contact.Addresses.Billing),
            countries
        );
    }

    private void AddCountriesToAddresses(
        IEnumerable<ContactAddress> address,
        List<CountryInformation> countries
    )
    {
        var test = address.ToList();
        address.ToList().ForEach(address => AddCountryToAddress(address, countries));
    }

    private void AddCountryToAddress(ContactAddress contact, List<CountryInformation> countries)
    {
        contact.Country = countries.Single(country => country.Code == contact.Countrycode);
    }

    public async Task<List<CountryInformation>> GetAllCountries(
        CancellationToken cancellationToken = default
    )
    {
        var uri = LexofficeApiAddressesBuilder.CountriesUri();

        var response = await GetWithRateLimitingAsync(uri, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var contents = await response
            .Content.ReadAsStringAsync(cancellationToken)
            .ConfigureAwait(false);

        return JsonConvert.DeserializeObject<List<CountryInformation>>(
            contents,
            new StringEnumConverter()
        );
    }

    public async Task<Invoice> GetInvoiceAsync(
        string id,
        CancellationToken cancellationToken = default
    )
    {
        var uri = LexofficeApiAddressesBuilder.InvoiceUri(id);

        var response = await GetWithRateLimitingAsync(uri, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var contents = await response
            .Content.ReadAsStringAsync(cancellationToken)
            .ConfigureAwait(false);

        var jsonSerializerSettings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        return JsonConvert.DeserializeObject<Invoice>(contents, jsonSerializerSettings);
    }

    private async Task<HttpResponseMessage> GetWithRateLimitingAsync(
        string uri,
        CancellationToken cancellationToken = default
    )
    {
        var lexofficeRateLimit = 500;

        await _semaphore.WaitAsync().ConfigureAwait(false);
        while ((DateTime.Now - _lastRequest).TotalMilliseconds < lexofficeRateLimit)
        {
            await Task.Delay(10);
        }

        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromMilliseconds(lexofficeRateLimit));

        var ergebnis = await retryPolicy.ExecuteAsync(
            async () => await _client.GetAsync(uri, cancellationToken).ConfigureAwait(false)
        );

        _lastRequest = DateTime.Now;
        _semaphore.Release();

        return ergebnis;
    }
}
