using System.Collections.Immutable;
using coIT.Libraries.Clockodo.BusinessRules.ChangeRequestRules;
using coIT.Libraries.Clockodo.BusinessRules.TimeEntryOnDayRules;
using coIT.Libraries.Clockodo.BusinessRules.TimeEntryRules;
using coIT.Libraries.Clockodo.BusinessRules.UserReportRules;
using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules
{
    public class ClockodoBusinessRulesEvaluator
    {
        private readonly int _exemptFromUserReportRulesUserId = 10000;
        private readonly IImmutableList<IClockodoTimeEntryRule> _clockodoTimeEntryBusinessRules;
        private readonly IImmutableList<IUserReportRule> _userReportRules;
        private readonly IImmutableList<IChangeRequestRule> _changeRequestRules;
        private readonly IImmutableList<IDailyTimeEntriesRule> _timeEntryOnDayRules;

        private ClockodoBusinessRulesEvaluator(
            IEnumerable<IClockodoTimeEntryRule> clockodoTimeEntryBusinessRules,
            IEnumerable<IUserReportRule> userReportRules,
            IEnumerable<IChangeRequestRule> changeRequestRules,
            IEnumerable<IDailyTimeEntriesRule> timeEntryOnDayRules
        )
        {
            _clockodoTimeEntryBusinessRules = clockodoTimeEntryBusinessRules.ToImmutableList();
            _userReportRules = userReportRules.ToImmutableList();
            _changeRequestRules = changeRequestRules.ToImmutableList();
            _timeEntryOnDayRules = timeEntryOnDayRules.ToImmutableList();
        }

        public static ClockodoBusinessRulesEvaluator CreateWithDefaultCustomerToBillingAccountsAsssociationes()
        {
            var servicesRulesDictionary = new Dictionary<string, List<string>>
            {
                {
                    "heco gmbh",
                    new List<string> { "8504" }
                },
                {
                    "hecoform gmbh",
                    new List<string> { "8504" }
                },
                {
                    "VARINOX d.o.o.",
                    new List<string> { "8337" }
                },
                {
                    "co-IT.eu GmbH",
                    new List<string> { "co-IT" }
                },
            };

            var textRulesDictionary = new Dictionary<string, string> { { "comWORK", "heco gmbh" } };

            return CreateWithCustomRuleAssociations(servicesRulesDictionary, textRulesDictionary);
        }

        public static ClockodoBusinessRulesEvaluator CreateWithCustomRuleAssociations(
            Dictionary<string, List<string>> serviceRules,
            Dictionary<string, string> textRules
        )
        {
            var clockodoTimeEntryBusinessRules = new List<IClockodoTimeEntryRule>
            {
                new TimeTimeEntryRuleTextRule(),
                new ServicesSetCorrectlyRule(serviceRules),
                new TimeTimeEntryRuleDurationRule(),
                new TimeEntryTextContentRule(textRules)
            };

            var clockodoUserReportBusinessRules = new List<IUserReportRule>
            {
                new NotWorkingLongerThan6HoursReportRule(),
                new NotWorkedMoreThan10HoursRule(),
                new BreakNotLongEnoughRule(),
                new DidNotWorkAtAllRule(),
                new DidNotWorkLongEnoughRule()
            };

            var clockodoChangeRequestRules = new List<IChangeRequestRule>
            {
                new ChangeRequestMustBeAcceptedRule()
            };

            var timeEntriesOnDayRules = new List<IDailyTimeEntriesRule>
            {
                new DailyTimeEntriesDontOverlap()
            };

            return new ClockodoBusinessRulesEvaluator(
                clockodoTimeEntryBusinessRules,
                clockodoUserReportBusinessRules,
                clockodoChangeRequestRules,
                timeEntriesOnDayRules
            );
        }

        public IEnumerable<Result<TimeEntry, ClockodoFailure>> EvaluateTimeEntries(
            List<TimeEntry> timeEntries
        )
        {
            return timeEntries.SelectMany(EvaluateTimeEntry);
        }

        private IEnumerable<Result<TimeEntry, ClockodoFailure>> EvaluateTimeEntry(
            TimeEntry timeEntry
        )
        {
            return _clockodoTimeEntryBusinessRules.Select(clockodoErrorEvaluator =>
                clockodoErrorEvaluator.Evaluate(timeEntry)
            );
        }

        public IEnumerable<Result<HashSet<TimeEntry>, ClockodoFailure>> EvaluateDailyTimeEntries(
            List<TimeEntry> timeEntries
        )
        {
            var dailyEntryEvaluationResults = timeEntries
                .GroupBy(timeEntry => new { timeEntry.EmployeeName, timeEntry.Start.Date })
                .Select(t => t.ToHashSet())
                .SelectMany(EvaluateTimeEntriesForUserOnDay);

            return dailyEntryEvaluationResults;
        }

        private IEnumerable<
            Result<HashSet<TimeEntry>, ClockodoFailure>
        > EvaluateTimeEntriesForUserOnDay(HashSet<TimeEntry> timeEntriesForUserOnDay)
        {
            return _timeEntryOnDayRules.Select(dailyTimeEntriesRule =>
                dailyTimeEntriesRule.Evaluate(timeEntriesForUserOnDay)
            );
        }

        public IEnumerable<Result<UserReportDayWithUser, ClockodoFailure>> EvaluateUserReports(
            List<UserReportDayWithUser> userReports
        )
        {
            return userReports
                .Where(userReport => userReport.Date < DateTime.Today)
                .SelectMany(EvaluateUserReport);
        }

        private IEnumerable<Result<UserReportDayWithUser, ClockodoFailure>> EvaluateUserReport(
            UserReportDayWithUser userReport
        )
        {
            if (userReport.Name.Split(":")[0].Trim() == _exemptFromUserReportRulesUserId.ToString())
                return new List<Result<UserReportDayWithUser, ClockodoFailure>>();

            return _userReportRules.Select(clockodoErrorEvaluator =>
                clockodoErrorEvaluator.Evaluate(userReport)
            );
        }

        public IEnumerable<Result<ChangeRequest, ClockodoFailure>> EvaluateChangeRequests(
            List<ChangeRequest> changeRequests
        )
        {
            return changeRequests
                .Where(userReport => userReport.Date < DateTime.Today)
                .SelectMany(EvaluateChangeRequest);
        }

        private IEnumerable<Result<ChangeRequest, ClockodoFailure>> EvaluateChangeRequest(
            ChangeRequest changeRequests
        )
        {
            return _changeRequestRules.Select(clockodoErrorEvaluator =>
                clockodoErrorEvaluator.Evaluate(changeRequests)
            );
        }
    }
}
