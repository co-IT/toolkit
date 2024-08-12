namespace coIT.Libraries.Clockodo.Absences.Contracts
{
    public class CalendarEntry
    {
        public CalendarEntry() { }

        public string NameWithPersonnelNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAllDayEvent { get; set; }
        public string Notice { get; set; }
        public int TypeOfLeave { get; set; }
        public double AmountOfDays { get; set; }
        public string Text { get; set; }
    }
}
