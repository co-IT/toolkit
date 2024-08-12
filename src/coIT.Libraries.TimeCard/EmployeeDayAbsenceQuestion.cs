using System.Collections.Concurrent;
using System.Globalization;
using coIT.Libraries.TimeCard.DataContracts;

namespace coIT.Libraries.TimeCard;

internal class EmployeeDayAbsenceQuestion
{
    public readonly int EmployeeId;
    public readonly DateTime Date;
    public readonly string Department;
    public ConcurrentBag<TimeCardAbsence> Bag;

    public EmployeeDayAbsenceQuestion(int employeeId, DateTime date, string department)
    {
        EmployeeId = employeeId;
        Date = date;
        Department = department;
    }

    public Uri GetUri(string webAddress)
    {
        var dateAsString = Date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        return new Uri(
            "https://"
                + webAddress
                + "/Bookings/GetBookings?employeeID="
                + EmployeeId
                + "&Date="
                + dateAsString
        );
    }
}
