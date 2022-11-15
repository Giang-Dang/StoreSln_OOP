namespace StoreSln_OOP.Entities
{
    public abstract class Bill : IContainsID
    {
        public int ID { get; set; }
        protected DateTime CreationDateTime { get; set; }
        protected List<BillRecord> BillRecords { get; set; }

        protected Bill(int id, DateTime creationDateTime)
        {
            ID = id;
            CreationDateTime = creationDateTime;
            BillRecords = new List<BillRecord>();
        }

        protected Bill(int id, DateTime creationDateTime, List<BillRecord> billRecords)
        {
            ID = id;
            CreationDateTime = creationDateTime;
            BillRecords = billRecords;
        }

        protected void AddBillRecord(BillRecord billRecord)
        {
            BillRecords.Add(billRecord);
        }
    }
}
