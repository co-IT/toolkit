using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.UserReportRules
{
    internal interface IUserReportRule
    {
        public Result<UserReportDayWithUser, ClockodoFailure> Evaluate(
            UserReportDayWithUser userReportDay
        );
    }
}
