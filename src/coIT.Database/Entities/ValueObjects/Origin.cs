using CSharpFunctionalExtensions;
using LiteDB;
using Newtonsoft.Json;

namespace coIT.Database.Entities.ValueObjects
{
    public class Origin : IEquatable<Origin>
    {
        public string RemoteId { get; private set; }
        public string DataSource { get; private set; }

        [BsonIgnore]
        [JsonIgnore]
        public bool IsUpdateable => DataSource != "old";

        private Origin(string id, string dataSource)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentOutOfRangeException($"Id muss angegeben werden.");

            if (string.IsNullOrWhiteSpace(dataSource))
                throw new ArgumentOutOfRangeException($"Datenquelle muss angegeben werden.");

            RemoteId = id;
            DataSource = dataSource;
        }

        public Origin() { }

        public static Result<Origin> TryCreate(string id, string dataSource)
        {
            try
            {
                return Result.Success(new Origin(id, dataSource));
            }
            catch (Exception)
            {
                return Result.Failure<Origin>("Die Quelle konnte nicht erstellt werden");
            }
        }

        public bool Equals(Origin other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return RemoteId == other.RemoteId && DataSource == other.DataSource;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((Origin)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((RemoteId != null ? RemoteId.GetHashCode() : 0) * 397)
                    ^ (DataSource != null ? DataSource.GetHashCode() : 0);
            }
        }
    }
}
