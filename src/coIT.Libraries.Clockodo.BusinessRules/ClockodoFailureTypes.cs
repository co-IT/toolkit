namespace coIT.Libraries.Clockodo.BusinessRules
{
    public enum ClockodoFailureType
    {
        TimeEntryServiceSetIncorrectly,
        TimeEntryShorterThan10Minutes,
        TimeEntryNoText,
        TimeEntryTextToShort,
        TimeEntryTextKeywordDoesntMatchCustomer,
        BreakOfAtLeast30MinNeeded,
        BreakOfAtlEast45MinNeeded,
        DidNotWork,
        WorkedLessThanExpected,
        WorkedLongerThan10Hours,
        WorkedLongerThan6HoursWithoutBreak,
        PendingChangeRequest
    }
}
