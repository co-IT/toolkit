using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.UserReportRules
{
    internal class DidNotWorkLongEnoughRule : IUserReportRule
    {
        public Result<UserReportDayWithUser, ClockodoFailure> Evaluate(
            UserReportDayWithUser userReportDay
        )
        {
            return Result.FailureIf(
                WorkedLessThenExpected(userReportDay) && userReportDay.WorkedHours > 0,
                userReportDay,
                ClockodoFailure.FromClockodoUserReport(
                    userReportDay,
                    "An diesem Tag fehlen Zeiteinträge, lege Zeiteinträge oder eine Abwesenheit an",
                    ClockodoFailureType.WorkedLessThanExpected
                )
            );
        }

        private static bool WorkedLessThenExpected(UserReportDayWithUser userReportDay)
        {
            var tolerance = 7.0 / 8;
            var should = TargetSeconds(userReportDay, tolerance);
            var actual = userReportDay.WorkedHours + userReportDay.ReductionByOvertime;

            return actual < should;
        }

        private static double TargetSeconds(UserReportDayWithUser day, double tolerance) =>
            day.TargetHours * tolerance - day.ReductionByOvertime;
    }
}
