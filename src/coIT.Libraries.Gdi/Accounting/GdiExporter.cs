using System.Globalization;
using System.Text;
using coIT.Libraries.Gdi.Accounting.Contracts;

namespace coIT.Libraries.Gdi.Accounting;

public static class GdiExporter
{
    private static readonly Encoding GdiEncoding = CodePagesEncodingProvider.Instance.GetEncoding(
        1252
    )!;
    private static readonly string BaseAddress = "https://controlling.co-IT.eu:2999";

    public static (string Filename, byte[] FileContent, string Lines) CreateExport(
        IEnumerable<Invoice> invoices,
        IEnumerable<Customer> customers
    )
    {
        var gdiLines = CreateGdiLines(customers, invoices);

        return Create(gdiLines);
    }

    private static (string Filename, byte[] FileContent, string Lines) Create(string gdiLines)
    {
        using var memoryStream = new MemoryStream();
        {
            using var writer = new StreamWriter(memoryStream, GdiEncoding);
            writer.Write(gdiLines);
        }

        var filecontent = memoryStream.ToArray();
        var filename = "GDIFIBU2.STD";

        return (filename, filecontent, gdiLines);
    }

    private static string CreateGdiLines(
        IEnumerable<Customer> customers,
        IEnumerable<Invoice> invoices
    )
    {
        var gdiLines = new StringBuilder();

        gdiLines.AppendLine("[GDI-Fibu]");
        gdiLines.AppendLine("[ANSI]");
        gdiLines.AppendLine(string.Empty);

        customers
            .Where(IgnoreOneTimeAndInternalCustomers)
            .OrderBy(customer => customer.Number)
            .Select(CreateGdiLine)
            .ToList()
            .ForEach(line => gdiLines.AppendLine(line));

        gdiLines.AppendLine(string.Empty);

        var orderedInvoices = invoices.OrderBy(invoice => invoice.Number);

        foreach (var invoice in orderedInvoices)
        {
            var directLink = GenerateDocumentStoreLink(invoice);

            var formatProvider = new CultureInfo("en-US");

            gdiLines.AppendLine(
                $"[Bu]={invoice.DebitorNumber} <ZsF>=URL={directLink} <Art>={InvoiceTypeMapper.GdiInvoiceTypeToken(invoice.Type)} <BNr>={invoice.Number} <Dat>={invoice.Date:ddMMyy} <BDa>={invoice.Date:ddMMyy} <ISO>=EUR <Txt>={invoice.DebitorName} <Btr>={invoice.GrossAmount.ToString("F2", formatProvider)} <RBt>={invoice.GrossAmount.ToString("F2", formatProvider)} <OZi>={invoice.DataSource}:{invoice.RemoteId} <GKt>={invoice.RevenueAccountNumber} <StS>={invoice.BillingAccountNumber} <Btr>={(-1 * invoice.NetAmount).ToString("F2", formatProvider)} <StB>={(-1 * invoice.TaxAmount).ToString("F2", formatProvider)}"
            );
        }

        gdiLines.AppendLine(string.Empty);

        return gdiLines.ToString();
    }

    private static bool IgnoreOneTimeAndInternalCustomers(Customer customer)
    {
        if (customer.Number == Customer.OneTimeCustomerNumber)
            return false;

        if (customer.Type == CustomerType.Internal)
            return false;

        return true;
    }

    private static string GenerateDocumentStoreLink(Invoice invoice)
    {
        return $"{BaseAddress}/Revenues/PrimaryInvoiceDocument?invoiceNumber={invoice.Number}";
    }

    private static string CreateGdiLine(Customer customer)
    {
        return $"[Pk]={customer.Number} "
            + $"<Nam>={DistributeNameToFit(customer.Name)} "
            + $"<Str>={customer.Address.Street.Trim()} "
            + $"<PLZ>={customer.Address.Zip} "
            + $"<ORT>={customer.Address.City} "
            + $"<Ort>={customer.Address.Zip} {customer.Address.City} "
            + $"<LKz>={customer.Address.CountryCode} "
            + $"<Lnd>={ShortenCountryToFit(customer.Address.Country)}";
    }

    private static string DistributeNameToFit(string name)
    {
        name = name.Trim();
        var distributedName = name;

        if (name.Length > 40)
            distributedName = $"{name[..40]} <Bra>={name[40..]}";

        if (name.Length > 80)
            distributedName += $" <AdZ>={name[80..]}";

        return distributedName;
    }

    private static string ShortenCountryToFit(string country)
    {
        return country[..5];
    }
}
