using coIT.Database.Utilities;
using CSharpFunctionalExtensions;

namespace coIT.Database.Entities
{
    public class ControllingAccount : Entity, IEquatable<ControllingAccount>
    {
        public int Number { get; private set; }

        public string Description { get; private set; }

        public bool IsConsulting { get; private set; }

        public bool IsFiscalUnity { get; private set; }

        public ControllingAccount() { }

        public ControllingAccount(
            int number,
            string description,
            bool isConsulting,
            bool isFiscalUnity
        )
        {
            var results = new List<Result>
            {
                ChangeNumber(number),
                ChangeDescription(description),
                ChangeConsulting(isConsulting),
                ChangeFiscalUnity(isFiscalUnity)
            };

            if (results.Any(result => result.IsFailure))
                throw new ArgumentException(results.ContentToString());
        }

        public Result ChangeNumber(int number)
        {
            if (number < 100000 || number > 500000)
                return Result.Failure("Die Nummer muss zwischen 100000 und 500000 liegen.");

            Number = number;

            return Result.Ok();
        }

        public Result ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description) || description.Length < 5)
                return Result.Failure(
                    "Die Beschreibung darf nich leer und unter 5 Zeichen lang sein."
                );

            Description = description;

            return Result.Ok();
        }

        public Result ChangeConsulting(bool isConsulting)
        {
            IsConsulting = isConsulting;

            return Result.Ok();
        }

        public Result ChangeFiscalUnity(bool isFiscalUnity)
        {
            IsFiscalUnity = isFiscalUnity;

            return Result.Ok();
        }

        public static ControllingAccount ConsultingToFiscalUnity() =>
            new ControllingAccount(100000, "Organschaft: Dienstleistungen", true, true);

        public static ControllingAccount ConsultingToMarket() =>
            new ControllingAccount(200000, "Markt: Dienstleistungen", true, false);

        public static ControllingAccount ProductSaleToFiscalUnity() =>
            new ControllingAccount(100010, "Organschaft: Produktverkauf", false, true);

        public static ControllingAccount ProductSaleToMarket() =>
            new ControllingAccount(200010, "Markt: Produktverkauf", false, false);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((ControllingAccount)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Number;
                hashCode = (hashCode * 397) ^ IsConsulting.GetHashCode();
                hashCode = (hashCode * 397) ^ IsFiscalUnity.GetHashCode();
                return hashCode;
            }
        }

        public bool Equals(ControllingAccount other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Number == other.Number
                && IsConsulting == other.IsConsulting
                && IsFiscalUnity == other.IsFiscalUnity;
        }
    }
}
