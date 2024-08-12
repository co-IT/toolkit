using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.UserReportRules
{
    internal class DidNotWorkAtAllRule : IUserReportRule
    {
        public Result<UserReportDayWithUser, ClockodoFailure> Evaluate(
            UserReportDayWithUser userReportDay
        )
        {
            var workedHours = userReportDay.WorkedHours + userReportDay.ReductionByOvertime;

            return Result.FailureIf(
                workedHours == 0 && userReportDay.TargetHours > 0,
                userReportDay,
                ClockodoFailure.FromClockodoUserReport(
                    userReportDay,
                    "Füge diesem Tag Zeiteinträge oder eine Abwesenheit hinzu, es liegen keine Zeiteinträge vor",
                    ClockodoFailureType.DidNotWork
                )
            );
        }
    }
}
