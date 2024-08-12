using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.UserReportRules
{
    internal class BreakNotLongEnoughRule : IUserReportRule
    {
        public Result<UserReportDayWithUser, ClockodoFailure> Evaluate(
            UserReportDayWithUser userReportDay
        )
        {
            var totalBeaktime =
                userReportDay.Breaks?.Sum(reportedBreak => reportedBreak.Length) ?? 0;

            if (userReportDay.WorkedHours < 6 * 60 * 60)
                return Result.Success<UserReportDayWithUser, ClockodoFailure>(userReportDay);

            return userReportDay.WorkedHours <= 9 * 60 * 60
                ? EvaluateBreakTimeWorkedLessThan9Hours(userReportDay, totalBeaktime)
                : EvaluateBreakTimeWorkedMoreThan9Hours(userReportDay, totalBeaktime);
        }

        private Result<
            UserReportDayWithUser,
            ClockodoFailure
        > EvaluateBreakTimeWorkedLessThan9Hours(
            UserReportDayWithUser userReportDay,
            int totalBeaktime
        )
        {
            return Result.SuccessIf(
                totalBeaktime >= 30 * 60,
                userReportDay,
                ClockodoFailure.FromClockodoUserReport(
                    userReportDay,
                    "Bei 6 bis 9 Stunden Arbeitszeit muss die Pausenzeit mindestens 30 Minuten betragen!",
                    ClockodoFailureType.BreakOfAtLeast30MinNeeded
                )
            );
        }

        private Result<
            UserReportDayWithUser,
            ClockodoFailure
        > EvaluateBreakTimeWorkedMoreThan9Hours(
            UserReportDayWithUser userReportDay,
            int totalBeaktime
        )
        {
            return Result.SuccessIf(
                totalBeaktime >= 45 * 60,
                userReportDay,
                ClockodoFailure.FromClockodoUserReport(
                    userReportDay,
                    "Ab 9 Stunden Arbeitszeit muss die Pausenzeit mindestens 45 Minuten betragen!",
                    ClockodoFailureType.BreakOfAtlEast45MinNeeded
                )
            );
        }
    }
}
