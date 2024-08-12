namespace coIT.Database.Entities.ValueObjects
{
    public class AccountName
    {
        public string Name { get; private set; }

        public AccountName() { }

        public AccountName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException($"Kontoname muss angegeben werden");

            Name = name;
        }

        public static implicit operator string(AccountName accountName) => accountName.Name;
    }
}
