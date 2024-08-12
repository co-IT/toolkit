namespace coIT.Database.Entities.ValueObjects
{
    public class BalanceSheetAccount : Entity
    {
        public AccountName Name { get; set; }

        public BalanceSheetAccount() { }

        public BalanceSheetAccount(int id, AccountName name)
        {
            Id = id;
            Name = name;
        }

        public static implicit operator string(BalanceSheetAccount balanceSheetAccount) =>
            balanceSheetAccount.Name;
    }
}
