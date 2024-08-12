using CSharpFunctionalExtensions;

namespace coIT.Database.Entities.ValueObjects
{
    public class InvoiceNumber : IEquatable<InvoiceNumber>
    {
        private InvoiceNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentOutOfRangeException("Die Rechnungsnummer muss angegeben werden");

            Number = number;
        }

        public InvoiceNumber() { }

        public static Result<InvoiceNumber> TryCreate(string number)
        {
            try
            {
                return Result.Success(new InvoiceNumber(number));
            }
            catch (Exception)
            {
                return Result.Failure<InvoiceNumber>("Die Rechnungsnummer ist nicht korrekt");
            }
        }

        public string Number { get; private set; }

        public static implicit operator string(InvoiceNumber wrappedObject) => wrappedObject.Number;

        public static bool operator ==(InvoiceNumber obj1, InvoiceNumber obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }

            if (obj1 is null || obj2 is null)
            {
                return false;
            }

            return obj1.Number == obj2.Number;
        }

        public static bool operator !=(InvoiceNumber obj1, InvoiceNumber obj2) => !(obj1 == obj2);

        public bool Equals(InvoiceNumber other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Number == other.Number;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;

            return Equals((InvoiceNumber)obj);
        }

        public override int GetHashCode() => Number != null ? Number.GetHashCode() : 0;

        public override string ToString() => Number;
    }
}
