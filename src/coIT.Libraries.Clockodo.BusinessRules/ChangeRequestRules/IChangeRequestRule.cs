using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.ChangeRequestRules
{
    internal interface IChangeRequestRule
    {
        public Result<ChangeRequest, ClockodoFailure> Evaluate(ChangeRequest changeRequest);
    }
}
