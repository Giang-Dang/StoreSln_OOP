namespace StoreSln_OOP.Entities
{
    public class Product : IContainsID
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ManufacturingDate { get; set; }

        public Product(
            int id,
            string name,
            decimal price,
            int categoryID,
            DateTime expiryDate,
            DateTime manufacturingDate,
            string manufacturer
            )
        {
            ID = id;
            Name = name;
            ExpiryDate = expiryDate;
            Price = price;
            ManufacturingDate = manufacturingDate;
            Manufacturer = manufacturer;
            CategoryID = categoryID;
        }
    }
}
