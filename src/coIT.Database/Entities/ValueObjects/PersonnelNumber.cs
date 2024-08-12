namespace coIT.Database.Entities.ValueObjects
{
    public class PersonnelNumber
    {
        public PersonnelNumber(int number)
        {
            if (number < 10000)
                throw new ArgumentOutOfRangeException(
                    $"Personalnummern fangen bei 10000 an {number}"
                );

            if (number > CollectiveAccount)
                throw new ArgumentOutOfRangeException(
                    $"Personalnummern hören bei {CollectiveAccount} auf {number}"
                );

            Number = number;
        }

        public PersonnelNumber() { }

        public int Number { get; private set; }

        public static implicit operator int(PersonnelNumber y) => y.Number;

        public static int CollectiveAccount => 99999;
    }
}
