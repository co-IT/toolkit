using System.Collections.Immutable;
using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Libraries.Clockodo.BusinessRules;

public class ClockodoBusinessRulesHelper
{
    private readonly ClockodoBusinessRulesEvaluator _clockodoBusinessRulesEvaluator;

    public ClockodoBusinessRulesHelper(
        ClockodoBusinessRulesEvaluator clockodoBusinessRulesEvaluator
    )
    {
        _clockodoBusinessRulesEvaluator = clockodoBusinessRulesEvaluator;
    }

    public List<ClockodoFailure> ListAllFailuresAndViolations(
        ImmutableHashSet<string> selectedEmployees,
        IEnumerable<TimeEntry> allTimeEntriesInPeriod,
        IEnumerable<UserReportDayWithUser> dailyUserReports,
        IEnumerable<ChangeRequest>? changeRequests = null
    )
    {
        var evaluatedTimeEntries = FindFailuresInTimeTracking(
            allTimeEntriesInPeriod,
            selectedEmployees
        );
        var evaluatedUserReports = FindLegalViolationsInWorkingHours(
            dailyUserReports,
            selectedEmployees
        );

        var timeEntryUserReportRules = evaluatedTimeEntries.Union(evaluatedUserReports);

        if (changeRequests is null)
            return timeEntryUserReportRules.ToList();

        var evaluatedChangeRequests = FindInvalidChangeRequests(changeRequests, selectedEmployees);
        return timeEntryUserReportRules.Union(evaluatedChangeRequests).ToList();
    }

    private IEnumerable<ClockodoFailure> FindFailuresInTimeTracking(
        IEnumerable<TimeEntry> allTimeEntriesInPeriod,
        ImmutableHashSet<string> selectedEmployees
    )
    {
        var timeEntriesForSelectedEmployees = allTimeEntriesInPeriod
            .Where(timeEntry => selectedEmployees.Contains(timeEntry.EmployeeName))
            .ToList();

        var individuallyEvaluatedTimeEntries = _clockodoBusinessRulesEvaluator
            .EvaluateTimeEntries(timeEntriesForSelectedEmployees)
            .Where(result => result.IsFailure)
            .Select(result => result.Error)
            .ToList();

        var dailyEvaluatedTimeEntries = _clockodoBusinessRulesEvaluator
            .EvaluateDailyTimeEntries(timeEntriesForSelectedEmployees)
            .Where(result => result.IsFailure)
            .Select(result => result.Error)
            .ToList();

        return individuallyEvaluatedTimeEntries.Union(dailyEvaluatedTimeEntries);
    }

    private IEnumerable<ClockodoFailure> FindLegalViolationsInWorkingHours(
        IEnumerable<UserReportDayWithUser> dailyUserReports,
        ImmutableHashSet<string> selectedEmployees
    )
    {
        var userReportsForSelectedEmployeesOnWorkday = dailyUserReports
            .Where(userReport => selectedEmployees.Contains(userReport.Name))
            .Where(userReport => userReport.TargetHours != 0)
            .ToList();

        var evaluatedUserReports = _clockodoBusinessRulesEvaluator
            .EvaluateUserReports(userReportsForSelectedEmployeesOnWorkday)
            .Where(result => result.IsFailure)
            .Select(result => result.Error)
            .ToList();

        return evaluatedUserReports;
    }

    public IEnumerable<ClockodoFailure> FindInvalidChangeRequests(
        IEnumerable<ChangeRequest> changeRequests,
        ImmutableHashSet<string> selectedEmployees
    )
    {
        var changeRequestsForSelectedEmployees = changeRequests
            .Where(request => request.User is not null)
            .Where(request => selectedEmployees.Contains(request.User!.Name!))
            .ToList();

        var evaluatedChangeRequests = _clockodoBusinessRulesEvaluator
            .EvaluateChangeRequests(changeRequestsForSelectedEmployees)
            .Where(result => result.IsFailure)
            .Select(result => result.Error)
            .ToList();

        return evaluatedChangeRequests;
    }
}
