using System.Web;

namespace coIT.Libraries.LexOffice;

internal static class LexofficeApiAddressesBuilder
{
    private static readonly string BaseAddress = @"https://api.lexoffice.io";

    private static readonly string AllVouchers = "v1/voucherlist";
    private static readonly string Invoice = "v1/invoices";
    private static readonly string Contacts = "v1/contacts";
    private static readonly string Countries = "v1/countries";

    public static string AllVouchersUri(string voucherType, string statusType, int page, int size)
    {
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters["voucherType"] = voucherType;
        queryParameters["voucherStatus"] = statusType;
        queryParameters["page"] = page.ToString();
        queryParameters["size"] = size.ToString();

        return $"{BaseAddress}/{AllVouchers}?{queryParameters}";
    }

    public static string InvoiceUri(string id)
    {
        return $"{BaseAddress}/{Invoice}/{id}";
    }

    public static string ContactsUri(int page, int size)
    {
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters["page"] = page.ToString();
        queryParameters["size"] = size.ToString();

        return $"{BaseAddress}/{Contacts}?{queryParameters}";
    }

    public static string CountriesUri()
    {
        return $"{BaseAddress}/{Countries}";
    }
}
