using Newtonsoft.Json;

namespace coIT.Database.Entities
{
    public class ClockodoFailure : Entity
    {
        public int ClockodoEntryId { get; private set; }
        public string Message { get; private set; }
        public int PersonnelNumber { get; private set; }
        public bool PreventsImport { get; private set; }
        public DateTime Date { get; private set; }

        public Uri DirectLinkToTimeEntry { get; private set; }
        public string MessageWithLink =>
            $"{Message} <a href='{DirectLinkToTimeEntry}' target='_blank'>Link</a>";

        public ClockodoFailure() { }

        [JsonConstructor]
        private ClockodoFailure(
            string message,
            int clockodoEntryId,
            DateTime date,
            int personnelNumber,
            bool preventsImport,
            Uri directLinkToTimeEntry
        )
        {
            Message = message;
            ClockodoEntryId = clockodoEntryId;
            Date = date;
            PersonnelNumber = personnelNumber;
            PreventsImport = preventsImport;
            DirectLinkToTimeEntry = directLinkToTimeEntry;
        }

        private static Uri GenerateUri(int id)
        {
            return new Uri($"https://my.clockodo.com/de/entries/editentry?id={id}");
        }

        private static Uri GenerateUri(DateTime date, int employeeId)
        {
            return new Uri(
                $"https://my.clockodo.com/de/reports?since={date:yyyy-MM-dd}&until={date:yyyy-MM-dd}&order=users_projects_services&restrUser%5B0%5D={employeeId}"
            );
        }

        private static Uri GenerateUri(DateTime date)
        {
            return new Uri(
                $"https://my.clockodo.com/de/entries?day={date:yyyy-MM-dd}&listType=chron"
            );
        }
    }
}
