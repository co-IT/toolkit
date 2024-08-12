using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.UserReportRules
{
    internal class NotWorkedMoreThan10HoursRule : IUserReportRule
    {
        public Result<UserReportDayWithUser, ClockodoFailure> Evaluate(
            UserReportDayWithUser userReportDay
        )
        {
            var workedHours = userReportDay.WorkedHours + userReportDay.ReductionByOvertime;

            return Result.SuccessIf(
                workedHours < 10 * 60 * 60,
                userReportDay,
                ClockodoFailure.FromClockodoUserReport(
                    userReportDay,
                    "Aktualisiere den Zeiteintrag, damit an diesem Tag nicht länger als 10 Stunden gearbeitet wurde",
                    ClockodoFailureType.WorkedLongerThan10Hours
                )
            );
        }
    }
}
