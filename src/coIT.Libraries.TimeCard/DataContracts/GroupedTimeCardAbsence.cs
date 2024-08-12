namespace coIT.Libraries.TimeCard.DataContracts
{
    public class GroupedTimeCardAbsence : TimeCardAbsence
    {
        public DateTime End { get; set; }

        public GroupedTimeCardAbsence(TimeCardAbsence start, TimeCardAbsence end)
        {
            AbsenceType = start.AbsenceType;
            Employee = start.Employee;
            Date = start.Date;
            Department = start.Department;
            End = end.Date;
            Id = start.Id;
            TimeStamp = start.TimeStamp;
            Info = start.Info;
            Color = start.Color;
            Type = start.Type;
            Time = start.Time;
            Reason = start.Reason;
            InconsistentType = start.InconsistentType;
            Comment = start.Comment;
        }
    }
}
