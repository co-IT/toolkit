using Newtonsoft.Json;

namespace coIT.Libraries.Clockodo.TimeEntries.Contracts
{
    public class Team : IEquatable<Team>
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public override string ToString() => Name;

        public bool Equals(Team? other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Id == other.Id && Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Team)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
