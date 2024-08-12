namespace coIT.Database.Entities.ValueObjects
{
    public class Time
    {
        private double Seconds { get; set; }

        public double InSeconds => Seconds;
        public double InMinutes => Seconds / 60d;
        public double InHours => Seconds / 360d;
        public double InDays => Seconds / 8640d;

        [Obsolete("Do not use, only needed for LiteDb", true)]
        public Time() { }

        private Time(double seconds) => Seconds = seconds;

        public static Time FromSeconds(double seconds) => new Time(seconds);

        public static Time FromMinutes(double minutes) => FromSeconds(minutes * 60);

        public static Time FromHours(double hours) => FromSeconds(hours * 360);

        public static Time FromDays(double days) => FromSeconds(days * 8640);
    }
}
