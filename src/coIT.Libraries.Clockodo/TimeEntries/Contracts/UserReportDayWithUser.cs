namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    public class UserReportDayWithUser : UserReportDay
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UserReportDayWithUser(UserReportDay dayReport, int id, string name)
        {
            Date = dayReport.Date;
            WeekDay = dayReport.WeekDay;
            Difference = dayReport.Difference;
            IsHoliday = dayReport.IsHoliday;
            IsSick = dayReport.IsSick;
            IsPersonalHoliday = dayReport.IsPersonalHoliday;
            TargetHours = dayReport.TargetHours;
            WorkedHours = dayReport.WorkedHours;
            Breaks = dayReport.Breaks;
            WorkStarted = dayReport.WorkStarted;
            WorkEnded = dayReport.WorkEnded;
            ReductionByOvertime = dayReport.ReductionByOvertime;
            Id = id;
            Name = name;
        }
    }
}
