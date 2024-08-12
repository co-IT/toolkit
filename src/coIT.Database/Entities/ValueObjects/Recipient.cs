namespace coIT.Database.Entities.ValueObjects
{
    public class Recipient
    {
        public Recipient() { }

        public Recipient(string name, int number)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException(
                    $"Leistungempfängername muss angegeben werden"
                );

            if (number < 0)
                throw new ArgumentOutOfRangeException(
                    $"Leistungsempfängernummer darf nicht negativ sein {number}"
                );

            Name = name;
            Number = number;
        }

        public string Name { get; private set; }
        public int Number { get; private set; }
    }
}
