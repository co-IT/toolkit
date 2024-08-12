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
    private readonly Random _random = new();

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

    public async Task<ImmutableList<Voucher>> GetVouchersInPeriod(DateOnly start, DateOnly end)
    {
        var lexOfficeVouchers = await GetAllInvoiceVouchersAsync();

        var vouchersInPeriod = lexOfficeVouchers
            .Where(voucher =>
                DateOnly.FromDateTime(voucher.VoucherDate) >= start
                && DateOnly.FromDateTime(voucher.VoucherDate) <= end
            )
            .ToImmutableList();
        return vouchersInPeriod;
    }

    public async Task<IImmutableList<Voucher>> GetAllInvoiceVouchersAsync()
    {
        var vouchers = new List<Voucher>();

        var type = VoucherType.Invoice;
        var status =
            VoucherStatus.Paid | VoucherStatus.Paidoff | VoucherStatus.Open | VoucherStatus.Voided;

        var wrapper = await GetVouchersAsync(type, status).ConfigureAwait(false);
        vouchers.AddRange(wrapper.Content);

        for (var page = 1; page < wrapper.TotalPages; page++)
        {
            var pageWrapper = await GetVouchersAsync(type, status, page).ConfigureAwait(false);
            vouchers.AddRange(pageWrapper.Content);
        }

        return vouchers.ToImmutableList();
    }

    public async Task<IImmutableList<Invoice>> GetInvoicesAsync(IImmutableList<Voucher> vouchers)
    {
        var tasks = new List<Task<Invoice>>();
        var throttler = new SemaphoreSlim(3);

        foreach (var voucher in vouchers)
        {
            await throttler.WaitAsync().ConfigureAwait(false);

            tasks.Add(
                Task.Run(async () =>
                {
                    try
                    {
                        var retryPolicy = Policy
                            .Handle<HttpRequestException>()
                            .WaitAndRetryAsync(
                                10,
                                retryAttempt =>
                                    TimeSpan.FromMilliseconds(
                                        Math.Pow(2, retryAttempt) + _random.Next(-1000, 1000)
                                    )
                            );

                        return await retryPolicy.ExecuteAsync(
                            async () => await GetInvoiceAsync(voucher.Id).ConfigureAwait(false)
                        );
                    }
                    finally
                    {
                        throttler.Release();
                    }
                })
            );
        }

        var invoices = await Task.WhenAll(tasks.ToArray()).ConfigureAwait(false);

        return invoices.ToImmutableList();
    }

    private async Task<VoucherResponseWrapper> GetVouchersAsync(
        int type,
        int status,
        int page = 0,
        int size = 250
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

        var response = await _client.GetAsync(uri).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var contents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        await Task.Delay(3000);

        return JsonConvert.DeserializeObject<VoucherResponseWrapper>(contents);
    }

    public async Task<List<ContactInformation>> GetContactsAsync()
    {
        var contactInformation = new List<ContactInformation>();

        var wrapper = await GetContactsAsync(0);
        contactInformation.AddRange(wrapper.ContactInformation);

        for (var page = 1; page < wrapper.TotalPages; page++)
        {
            var pageWrapper = await GetContactsAsync(page);
            contactInformation.AddRange(pageWrapper.ContactInformation);
        }

        await AddCountriesToContactAddresses(contactInformation);
        FixIncorrectlyFormattedZips(contactInformation);
        return contactInformation;
    }

    private async Task<ContactsResponseWrapper> GetContactsAsync(int page, int size = 250)
    {
        var uri = LexofficeApiAddressesBuilder.ContactsUri(page, size);

        var response = await _client.GetAsync(uri).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var contents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        await Task.Delay(2000);

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

    private async Task AddCountriesToContactAddresses(List<ContactInformation> contacts)
    {
        var countries = await GetAllCountries();

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

    public async Task<List<CountryInformation>> GetAllCountries()
    {
        var uri = LexofficeApiAddressesBuilder.CountriesUri();

        var response = await _client.GetAsync(uri).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var contents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        await Task.Delay(2000);

        return JsonConvert.DeserializeObject<List<CountryInformation>>(
            contents,
            new StringEnumConverter()
        );
    }

    private async Task<Invoice> GetInvoiceAsync(string id)
    {
        var uri = LexofficeApiAddressesBuilder.InvoiceUri(id);

        var response = await _client.GetAsync(uri).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var contents = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        await Task.Delay(3000);

        var jsonSerializerSettings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        return JsonConvert.DeserializeObject<Invoice>(contents, jsonSerializerSettings);
    }
}
