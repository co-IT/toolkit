namespace coIT.Database.Entities.ValueObjects
{
    public class FullName
    {
        public FullName(string firstName, string lastName)
        {
            if (firstName.Equals(string.Empty) || lastName.Equals(string.Empty))
                throw new ArgumentOutOfRangeException(
                    $"Der volle Name muss angegeben werden {firstName} {lastName}"
                );

            LastName = lastName;
            FirstName = firstName;
        }

        public FullName() { }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public static implicit operator string(FullName fn) => $"{fn.FirstName} {fn.LastName}";

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
