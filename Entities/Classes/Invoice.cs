﻿namespace StoreSln_OOP.Entities
{
    public class Invoice : Bill
    {
        public Invoice(int id, DateTime creationDateTime) : base(id, creationDateTime)
        {
        }

        public Invoice(int id, DateTime creationDateTime, List<BillRecord> billRecords) : base(id, creationDateTime, billRecords)
        {
        }
    }
}
