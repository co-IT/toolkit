namespace coIT.Database.Entities.ValueObjects
{
    public class Debitor
    {
        public Debitor() { }

        public Debitor(string name, int number)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException($"Debitorname muss angegeben werden");

            if (number < 0)
                throw new ArgumentOutOfRangeException(
                    $"Debitornummer darf nicht negativ sein {number}"
                );

            Name = name;
            Number = number;
        }

        public string Name { get; private set; }
        public int Number { get; private set; }
    }
}
