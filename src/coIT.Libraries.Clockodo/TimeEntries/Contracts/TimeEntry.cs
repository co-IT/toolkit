using CSharpFunctionalExtensions;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts;

public class TimeEntry : Entity<int>
{
    internal TimeEntry(RawTimeEntry rawTimeEntry, Customer customer)
        : base(rawTimeEntry.StoreId)
    {
        Start = DateTime.Parse(rawTimeEntry.TimeSince);
        End = DateTime.Parse(rawTimeEntry.TimeUntil!); // Laufende Zeiteinträge werden zuvor entfernt
        Duration = rawTimeEntry.Duration ?? 0;
        Customer = customer;
        ProjectName = rawTimeEntry.ProjectsName;
        EmployeeName = rawTimeEntry.UserName;
        Revenue = rawTimeEntry.Revenue;
        ServicesName = rawTimeEntry.ServiceName;
        Text = rawTimeEntry.Text;
        Id = rawTimeEntry.StoreId;

        BillStatus = Enum.IsDefined(typeof(BillStatus), rawTimeEntry.Billable)
            ? (BillStatus)rawTimeEntry.Billable
            : BillStatus.Unbekannt;

        CostumerID = rawTimeEntry.CustomerId;
        ProjectID = rawTimeEntry.ProjectId;
        ServiceID = rawTimeEntry.ServiceId;
    }

    public DateTime Start { get; }
    public DateTime End { get; }
    public int Duration { get; }
    public Customer Customer { get; }
    public string ProjectName { get; }
    public string EmployeeName { get; }
    public decimal Revenue { get; }
    public string ServicesName { get; }
    public string Text { get; }
    public int Id { get; }
    public BillStatus BillStatus { get; }
    public int CostumerID { get; }
    public int? ProjectID { get; }
    public int? ServiceID { get; }
}
