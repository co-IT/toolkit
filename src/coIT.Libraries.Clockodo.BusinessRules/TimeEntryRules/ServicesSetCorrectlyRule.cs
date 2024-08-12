using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.TimeEntryRules;

internal class ServicesSetCorrectlyRule : IClockodoTimeEntryRule
{
    private readonly Dictionary<string, List<string>> _companyServiceRestrictions;

    public ServicesSetCorrectlyRule(Dictionary<string, List<string>> companyServiceRestrictions)
    {
        _companyServiceRestrictions = companyServiceRestrictions;
    }

    public Result<TimeEntry, ClockodoFailure> TimeEntryUsesAllowedService(
        TimeEntry timeEntry,
        List<string> allowedServicesForCompany
    )
    {
        return Result.SuccessIf(
            allowedServicesForCompany.Contains(timeEntry.ServicesName),
            timeEntry,
            ClockodoFailure.FromClockodoTimeEntry(
                timeEntry,
                $"Aktualisiere den Eintrag, damit '{timeEntry.Customer.Name}' als Konto '{string.Join(", ", allowedServicesForCompany)}' benutzt. (Derzeit {timeEntry.ServicesName}",
                ClockodoFailureType.TimeEntryServiceSetIncorrectly
            )
        );
    }

    public Result<TimeEntry, ClockodoFailure> ServicesNameIs8500(TimeEntry timeEntry)
    {
        return Result.SuccessIf(
            timeEntry.Customer is null || timeEntry.ServicesName == "8500",
            timeEntry,
            ClockodoFailure.FromClockodoTimeEntry(
                timeEntry,
                $"Aktualisiere den Eintrag, damit der Kunde '{timeEntry.Customer.Name}' das Konto 8500 benutzt. (Derzeit {timeEntry.ServicesName})",
                ClockodoFailureType.TimeEntryServiceSetIncorrectly
            )
        );
    }

    public Result<TimeEntry, ClockodoFailure> Evaluate(TimeEntry timeEntry)
    {
        return _companyServiceRestrictions.ContainsKey(timeEntry.Customer.Name)
            ? TimeEntryUsesAllowedService(
                timeEntry,
                _companyServiceRestrictions[timeEntry.Customer.Name]
            )
            : ServicesNameIs8500(timeEntry);
    }
}
