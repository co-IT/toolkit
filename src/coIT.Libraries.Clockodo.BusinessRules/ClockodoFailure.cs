using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Libraries.Clockodo.BusinessRules;

public class ClockodoFailure
{
    public string Message { get; }
    public string Context { get; set; }
    public DateTime Date { get; }
    public string EmployeeName { get; }
    public Uri DirectLinkToTimeEntry { get; }
    public ClockodoFailureType FailureType { get; }
    public bool CanBeEdited { get; }

    private ClockodoFailure(
        string message,
        string context,
        DateTime date,
        string employeeName,
        Uri directLinkToTimeEntry,
        ClockodoFailureType failureType,
        bool canBeEdited
    )
    {
        Message = message;
        Context = context;
        Date = date;
        EmployeeName = employeeName;
        DirectLinkToTimeEntry = directLinkToTimeEntry;
        FailureType = failureType;
        CanBeEdited = canBeEdited;
    }

    public static ClockodoFailure FromClockodoTimeEntry(
        TimeEntry entry,
        string message,
        ClockodoFailureType failureType
    )
    {
        var uri = GenerateEntryUri(entry.Id);
        return new ClockodoFailure(
            message,
            $"{entry.Customer.Name} / {entry.ProjectName} - {entry.ServicesName}",
            entry.Start,
            entry.EmployeeName,
            uri,
            failureType,
            entry.BillStatus is not BillStatus.BereitsAbgerechnet
        );
    }

    public static ClockodoFailure FromClockodoUserReport(
        UserReportDayWithUser userReport,
        string message,
        ClockodoFailureType failureType
    )
    {
        var uri = GenerateDayUri(userReport.Date);
        return new ClockodoFailure(
            message,
            string.Empty,
            userReport.Date,
            userReport.Name,
            uri,
            failureType,
            true
        );
    }

    public static ClockodoFailure FromChangeRequest(
        ChangeRequest changeRequest,
        string message,
        ClockodoFailureType failureType
    )
    {
        var uri = GenerateDayUri(changeRequest.Date);
        return new ClockodoFailure(
            message,
            string.Empty,
            changeRequest.Date,
            changeRequest.User?.Name ?? string.Empty,
            uri,
            failureType,
            true
        );
    }

    public static ClockodoFailure FromTimeEntriesOnDay(
        HashSet<TimeEntry> timeEntries,
        string message,
        ClockodoFailureType failureType
    )
    {
        var editableTimeEntries =
            timeEntries.Count(entry => entry.BillStatus is not BillStatus.BereitsAbgerechnet) > 0;

        var firstTimeEntry = timeEntries.First();
        var uri = GenerateDayUri(firstTimeEntry.Start);
        return new ClockodoFailure(
            message,
            string.Empty,
            firstTimeEntry.Start.Date,
            firstTimeEntry.EmployeeName,
            uri,
            failureType,
            editableTimeEntries
        );
    }

    private static Uri GenerateEntryUri(int id)
    {
        return new Uri($"https://my.clockodo.com/de/entries/editentry?id={id}");
    }

    private static Uri GenerateDayUri(DateTime date)
    {
        return new Uri($"https://my.clockodo.com/de/entries/?view=day&day={date:yyyy-MM-dd}");
    }
}
