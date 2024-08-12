using coIT.Libraries.Clockodo.TimeEntries.Contracts;

namespace coIT.Libraries.Clockodo.TimeEntries.Filter;

public static class TimeEntryGrouper
{
    public static IEnumerable<CustomerEmployeeHourData> GroupByEmployeeAndCustomer(
        this IEnumerable<TimeEntry> entries
    )
    {
        return entries
            .GroupBy(g => new { g.EmployeeName, g.Customer.Name })
            .Select(g => new CustomerEmployeeHourData
            {
                Employee = g.Key.EmployeeName,
                Customer = g.Key.Name,
                Time = g.Sum(e => e.Duration) / 60m
            })
            .OrderByDescending(e => e.Time);
    }
}
