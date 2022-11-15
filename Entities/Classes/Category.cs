namespace StoreSln_OOP.Entities
{
    public class Category : IContainsID
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Category(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
