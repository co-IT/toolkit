using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.ChangeRequestRules
{
    internal class ChangeRequestMustBeAcceptedRule : IChangeRequestRule
    {
        public Result<ChangeRequest, ClockodoFailure> Evaluate(ChangeRequest changeRequest)
        {
            bool requestIsPending = changeRequest.Status == ChangeRequestStatus.Beantragt;

            return Result.FailureIf(requestIsPending, changeRequest, CreateFailure(changeRequest));
        }

        private static ClockodoFailure CreateFailure(ChangeRequest changeRequest)
        {
            return ClockodoFailure.FromChangeRequest(
                changeRequest,
                "Kontaktiere Sandra und Uli, um die Arbeitszeitänderung zu genehmigen",
                ClockodoFailureType.PendingChangeRequest
            );
        }
    }
}
