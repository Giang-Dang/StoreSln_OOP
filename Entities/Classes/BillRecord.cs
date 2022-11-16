namespace StoreSln_OOP.Entities
{
    public class BillRecord : IContainsID
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int ProductCount { get; set; }
        public BillRecord(int id, int productID, int productCount)
        {
            ID = id;
            ProductID = productID;
            ProductCount = productCount;
        }
    }
}
