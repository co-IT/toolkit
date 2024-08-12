namespace coIT.Database.Entities
{
    public class Projection : Entity
    {
        public string Content { get; private set; }

        public Projection() { }

        public Projection(string content)
        {
            Id = 1;
            Content = content;
        }
    }
}
