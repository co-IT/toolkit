using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.UserReportRules
{
    internal class NotWorkingLongerThan6HoursReportRule : IUserReportRule
    {
        public Result<UserReportDayWithUser, ClockodoFailure> Evaluate(
            UserReportDayWithUser userReportDay
        )
        {
            var workedMoreThan6HoursWithoutBreak = WorksMoreThan6HoursWithoutBreak(userReportDay);
            return Result.FailureIf(
                workedMoreThan6HoursWithoutBreak,
                userReportDay,
                ClockodoFailure.FromClockodoUserReport(
                    userReportDay,
                    "Aktualisiere die Zeiteinträge, damit nach 6h Arbeit eine Pause eingelegt wurde",
                    ClockodoFailureType.WorkedLongerThan6HoursWithoutBreak
                )
            );
        }

        private static bool WorksMoreThan6HoursWithoutBreak(UserReportDayWithUser userReport)
        {
            if (userReport.Breaks is null)
                return userReport.WorkedHours > 6 * 60 * 60;

            var workTimes = userReport
                .Breaks.SelectMany(reportBreak => new List<DateTime>
                {
                    reportBreak.Start,
                    reportBreak.End
                })
                .Prepend(userReport.WorkStarted)
                .Append(userReport.WorkEnded)
                .ToList()
                .Chunk(2);

            var longestWorkTime = workTimes
                .Select(workTimePair => workTimePair[1] - workTimePair[0])
                .Select(workTimeTimespan => workTimeTimespan.TotalHours)
                .Max();

            return longestWorkTime > 6;
        }
    }
}
