namespace coIT.Database.Entities.ValueObjects
{
    public class IncomeStatementAccount : Entity
    {
        public AccountName Name { get; private set; }

        public IncomeStatementAccount() { }

        public IncomeStatementAccount(int id, AccountName name)
        {
            Id = id;
            Name = name;
        }

        public static implicit operator string(IncomeStatementAccount incomeStatementAccount) =>
            incomeStatementAccount.Name;
    }
}
