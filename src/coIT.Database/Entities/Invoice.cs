using coIT.Database.Entities.ValueObjects;
using CSharpFunctionalExtensions;
using LiteDB;

namespace coIT.Database.Entities
{
    public class Invoice : Entity, IEquatable<Invoice>
    {
        public DateTime Date { get; private set; }

        public InvoiceNumber Number { get; private set; }

        public List<InvoiceLine> Lines { get; private set; }

        [BsonIgnore]
        public decimal Sum => Lines.Sum(line => line.Amount.Value);

        [BsonIgnore]
        public Result<BillingAccount> BillingAccount
        {
            get
            {
                var usedBillingAccounts = Lines.Select(line => line.Account).Distinct().ToList();

                var message =
                    "Es wurde mehr als ein Konto angegeben. Pro Rechnung darf nur ein Umsatzkonto verwendet werden.";

                return usedBillingAccounts.Count == 1
                    ? Result.Ok(Lines.First().Account)
                    : Result.Failure<BillingAccount>(message);
            }
        }

        public Origin Origin { get; private set; }

        public InvoiceType Type { get; private set; }

        public Debitor Debitor { get; private set; }

        public bool Edited { get; private set; }

        public bool Sealed { get; private set; }

        public string AdditionalNotes { get; private set; }

        public Invoice() { }

        private Invoice(
            DateTime date,
            InvoiceNumber number,
            Origin origin,
            bool edited,
            InvoiceType type,
            Debitor debitor,
            List<InvoiceLine> lines,
            string additionalNotes = ""
        )
        {
            Date = date;
            Number = number;
            Origin = origin;
            Edited = edited;
            Type = type;
            Debitor = debitor;
            Lines = lines;
            AdditionalNotes = additionalNotes;

            if (type == InvoiceType.CreditNote)
                Sealed = true;
        }

        public static Invoice TryCreate(
            DateTime date,
            InvoiceNumber number,
            Origin origin,
            bool edited,
            InvoiceType type,
            Debitor debitor,
            List<InvoiceLine> lines,
            string additionNotes
        ) => new Invoice(date, number, origin, edited, type, debitor, lines, additionNotes);

        public Result<CalculationGroup> ToCalculationGroup(
            IReadOnlyList<ControllingAccount> validAccounts
        )
        {
            var calculationGroup = new CalculationGroup(Number, Date, Debitor, Origin);

            foreach (var line in Lines)
            {
                var autoMappedAccount = line.Account.BillingAccountToControllingAccount();

                if (autoMappedAccount.IsFailure)
                    return autoMappedAccount.ConvertFailure<CalculationGroup>();

                var validControllingAccount = validAccounts.Single(acc =>
                    acc.Equals(autoMappedAccount.Value)
                );

                calculationGroup.AddCalculation(
                    validControllingAccount,
                    line.Employee,
                    line.Amount
                );
            }

            return Result.Success(calculationGroup);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;

            return Equals((Invoice)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Date.GetHashCode();

                hashCode = (hashCode * 397) ^ (Number != null ? Number.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Lines != null ? Lines.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Origin != null ? Origin.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)Type;
                hashCode = (hashCode * 397) ^ (Debitor != null ? Debitor.GetHashCode() : 0);

                return hashCode;
            }
        }

        public bool Equals(Invoice other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            var numberEq = Equals(Number.Number, other.Number.Number);

            var linesEq = CompareLines(
                Lines
                    .OrderBy(l => l.Amount.Value)
                    .ThenBy(l => l.Account.RevenueAccount.Number)
                    .ThenBy(l => l.Employee.PersonnelNumber.Number),
                other
                    .Lines.OrderBy(l => l.Amount.Value)
                    .ThenBy(l => l.Account.RevenueAccount.Number)
                    .ThenBy(l => l.Employee.PersonnelNumber.Number)
            );

            var debitorNameEq = Equals(Debitor?.Name, other.Debitor?.Name);
            var debitorNumberEq = Equals(Debitor?.Number, other.Debitor?.Number);
            var additionalNotesEq = Equals(AdditionalNotes, other.AdditionalNotes);

            return Date.Equals(other.Date)
                && numberEq
                && Type == other.Type
                && linesEq
                && debitorNumberEq
                && debitorNameEq
                && additionalNotesEq;
        }

        private static bool CompareLines(
            IOrderedEnumerable<InvoiceLine> lines,
            IOrderedEnumerable<InvoiceLine> otherLines
        )
        {
            if (lines == null || otherLines == null)
                throw new InvalidDataException("Zeilen ungültig");

            if (lines.Count() != otherLines.Count())
                return false;

            for (var i = 0; i < lines.Count(); i++)
            {
                var lineThis = lines.Skip(i).Take(1).Single();
                var lineOther = otherLines.Skip(i).Take(1).Single();

                if (!lineThis.Equals(lineOther))
                    return false;
            }

            return true;
        }
    }
}
