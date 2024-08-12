namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    public class ChangeRequest
    {
        internal ChangeRequest(RawChangeRequest rawChangeRequest, IEnumerable<UserWithTeam> users)
        {
            Id = rawChangeRequest.Id;
            User = users.FirstOrDefault(user => user.Id == rawChangeRequest.UserId);

            Date = DateTime.Parse(rawChangeRequest.Date);

            DeclinedAt = rawChangeRequest.DeclinedAt is not null
                ? DateTime.Parse(rawChangeRequest.DeclinedAt)
                : null;

            DeclinedBy = rawChangeRequest.DeclinedBy is not null
                ? DateTime.Parse(rawChangeRequest.DeclinedBy)
                : null;

            Status = Enum.IsDefined(typeof(ChangeRequestStatus), rawChangeRequest.Status)
                ? (ChangeRequestStatus)rawChangeRequest.Status
                : ChangeRequestStatus.Unbekannt;
        }

        public int Id { get; }
        public DateTime Date { get; }
        public UserWithTeam? User { get; }
        public ChangeRequestStatus Status { get; }
        public DateTime? DeclinedAt { get; }
        public DateTime? DeclinedBy { get; }
    }
}
