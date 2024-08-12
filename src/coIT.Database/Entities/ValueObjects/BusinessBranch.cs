namespace coIT.Database.Entities.ValueObjects
{
    public class BusinessBranch
    {
        public string Name { get; private set; }

        public static string Market => "Markt";
        public static string Corporate => "Corporate";
        public static string FiscalUnity => "Organschaft";

        public BusinessBranch(string name)
        {
            if (name != Market && name != FiscalUnity && name != Corporate)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(name),
                    "Die Geschäftssparte kann nur Corporate, Markt oder Organschaft sein"
                );
            }

            Name = name;
        }

        public BusinessBranch() { }

        public static implicit operator string(BusinessBranch bb) => bb.Name;
    }
}
