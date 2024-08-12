using LiteDB;

namespace coIT.Database.Entities.ValueObjects
{
    public class InvoiceLine : IEquatable<InvoiceLine>
    {
        [BsonRef("BillingAccount")]
        public BillingAccount Account { get; private set; }

        [BsonRef("Employee")]
        public Employee Employee { get; private set; }

        public Amount Amount { get; private set; }

        public double TaxRate { get; private set; }

        public InvoiceLine(BillingAccount account, Employee employee, Amount amount, double taxRate)
        {
            Account = account;
            Employee = employee;
            Amount = amount;
            TaxRate = taxRate;
        }

        public InvoiceLine() { }

        public string ProductDescription =>
            $"{Account.RevenueAccount.Number}-{Employee.PersonnelNumber.Number}: {Employee.Name}";

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((InvoiceLine)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Account != null ? Account.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Employee != null ? Employee.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Amount != null ? Amount.GetHashCode() : 0);
                return hashCode;
            }
        }

        public bool Equals(InvoiceLine other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            var accountEq = Equals(
                Account.RevenueAccount.Number,
                other.Account.RevenueAccount.Number
            );
            var employeeEq = Equals(
                Employee.PersonnelNumber.Number,
                other.Employee.PersonnelNumber.Number
            );
            var amountEq = Equals(Amount.Value, other.Amount.Value);

            var all =
                accountEq
                && employeeEq
                && amountEq
                && Equals(Amount.CurrencySymbol, other.Amount.CurrencySymbol);

            return all;
        }

        public void ChangeEmployee(Employee newEmployee) =>
            Employee = newEmployee ?? throw new NullReferenceException(nameof(newEmployee));

        public void ChangeBillingAccount(BillingAccount newAccount) =>
            Account = newAccount ?? throw new NullReferenceException(nameof(newAccount));
    }
}
