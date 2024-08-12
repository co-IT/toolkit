using coIT.Database.Entities;
using coIT.Database.Entities.ValueObjects;

namespace coIT.Database;

internal static class BusinessRules
{
    public static bool IsHecoGroup(this Invoice invoice)
    {
        var internalDebitorNumbers = new HashSet<int> { 50000, 50001, 50035, 50039 };

        var debitor = invoice.Debitor.Number;

        return internalDebitorNumbers.Contains(debitor);
    }

    public static bool IsService(this InvoiceLine line, string invoiceNumber)
    {
        var wv_cyber_development = new HashSet<string> { "RE2022-11-075", "RE2022-11-076" };

        if (wv_cyber_development.Contains(invoiceNumber))
            return true;

        var resellingDummyNumbers = new HashSet<int> { 99998, 99999 };

        return !resellingDummyNumbers.Contains(line.Employee.PersonnelNumber.Number);
    }
}
