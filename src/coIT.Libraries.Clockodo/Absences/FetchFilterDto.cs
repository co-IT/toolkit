namespace coIT.Libraries.Clockodo.Absences
{
    public class FetchFilterDto
    {
        public string From { get; set; }
        public string To { get; set; }

        public FetchFilterDto(string from, string to)
        {
            From = from;
            To = to;
        }

        public FetchFilterDto() { }

        public DateTime FromDate => Convert.ToDateTime(From);
        public DateTime ToDate => Convert.ToDateTime(To);

        public bool IsInPeriod(DateTime val) => val.IsInPeriod(FromDate, ToDate);
    }
}
