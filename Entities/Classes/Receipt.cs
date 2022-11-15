namespace StoreSln_OOP.Entities
{
    public class Receipt : Bill
    {
        public Receipt(int id, DateTime creationDateTime) : base(id, creationDateTime)
        {
        }

        public Receipt(int id, DateTime creationDateTime, List<BillRecord> billRecords) : base(id, creationDateTime, billRecords)
        {
        }
    }
}
