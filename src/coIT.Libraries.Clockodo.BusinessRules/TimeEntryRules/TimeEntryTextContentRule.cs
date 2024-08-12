using System.Text;
using coIT.Libraries.Clockodo.TimeEntries.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.BusinessRules.TimeEntryRules;

public class TimeEntryTextContentRule : IClockodoTimeEntryRule
{
    private readonly Dictionary<string, string> _textRulesDictionary;

    public TimeEntryTextContentRule(Dictionary<string, string> textRulesDictionary)
    {
        _textRulesDictionary = textRulesDictionary;
    }

    private IEnumerable<KeyValuePair<string, string>> GetRelevantRules(TimeEntry timeEntry)
    {
        var entryText = timeEntry.Text?.ToLowerInvariant() ?? string.Empty;

        return _textRulesDictionary
            .Where(rule => entryText.Contains(rule.Key.ToLowerInvariant()))
            .ToList();
    }

    private IEnumerable<KeyValuePair<string, string>> GetBrokenRules(TimeEntry timeEntry)
    {
        var relevantRules = GetRelevantRules(timeEntry);

        return relevantRules.Where(rule =>
            !string.Equals(
                timeEntry.Customer.Name,
                rule.Value,
                StringComparison.InvariantCultureIgnoreCase
            )
        );
    }

    public Result<TimeEntry, ClockodoFailure> Evaluate(TimeEntry timeEntry)
    {
        var brokenRules = GetBrokenRules(timeEntry);

        if (!brokenRules.Any())
            return Result.Success<TimeEntry, ClockodoFailure>(timeEntry);

        var mistakes = new StringBuilder();

        foreach (var brokenRule in brokenRules)
            mistakes.AppendLine(
                $"Bei der Beschreibung '{brokenRule.Key}' wird Kunde '{brokenRule.Value}' erwartet."
            );

        var clockodoFailure = ClockodoFailure.FromClockodoTimeEntry(
            timeEntry,
            mistakes.ToString(),
            ClockodoFailureType.TimeEntryTextKeywordDoesntMatchCustomer
        );

        return Result.Failure<TimeEntry, ClockodoFailure>(clockodoFailure);
    }
}
