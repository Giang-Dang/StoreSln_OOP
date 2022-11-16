using StoreSln_OOP.DAL;
using StoreSln_OOP.Entities;
using System.Globalization;

namespace StoreSln_OOP.BLL
{
    public class ReceiptBL
    {
        public static bool ReadData(out List<Receipt> receipts)
        {
            return ReceiptDA.GetDALFunction().ReadData(out receipts);
        }

        public static bool Add(Receipt receipt)
        {
            return ReceiptDA.GetDALFunction().Add(receipt);
        }

        public static bool Update(Receipt receipt)
        {
            return ReceiptDA.GetDALFunction().Update(receipt);
        }

        public static bool RemoveAtID(int id)
        {
            return ReceiptDA.GetDALFunction().RemoveAtID(id);
        }

        public static int GetNextID()
        {
            List<Receipt> receipts;
            ReceiptDA.GetDALFunction().ReadData(out receipts);

            int res = 1;
            if (receipts.Any())
            {
                res = receipts.Max(i => i.ID) + 1;
            }
            return res;
        }

        public static bool AddBillRecord(int receiptID, int productID, int productCount)
        {
            List<Product> products;
            ProductDA.GetDALFunction().ReadData(out products);

            List<Receipt> receipts;
            ReceiptDA.GetDALFunction().ReadData(out receipts);

            var receipt = receipts.FirstOrDefault(i => i.ID == receiptID);

            if(receipt == default)
            {
                return false;
            }

            int recordID = 1;
            if (receipt.BillRecords == null)
            {
                receipt.BillRecords = new List<BillRecord>();
            }
            if (receipt.BillRecords.Any())
            {
                recordID = receipt.BillRecords.Max(r => r.ID) + 1;
            }
            BillRecord record = new BillRecord(recordID, productID, productCount);
            receipt.BillRecords.Add(record);

            ReceiptDA.GetDALFunction().Update(receipt);

            return true;
        }

        public static bool AnyMatchID(int id)
        {
            List<Receipt> receipts;
            ReceiptDA.GetDALFunction().ReadData(out receipts);

            return receipts.Any(i => i.ID == id);
        }

        public static Receipt? FindByID(int id)
        {
            List<Receipt> receipts;
            ReceiptDA.GetDALFunction().ReadData(out receipts);

            return receipts.SingleOrDefault(i => i.ID == id);
        }

        public static bool AnyProductMatchID(int productID)
        {
            List<Receipt> receipts;
            ReceiptDA.GetDALFunction().ReadData(out receipts);

            var res = false;
            foreach (var receipt in receipts)
            {
                foreach (var productRecord in receipt.BillRecords)
                {
                    if (productRecord.ProductID == productID)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }

        public static List<Receipt> Filter(string? str_MinCreationDateTime, string? str_MaxCreationDateTime)
        {
            List<Receipt> queryReceipts;
            ReceiptDA.GetDALFunction().ReadData(out queryReceipts);

            if (str_MinCreationDateTime != null)
            {
                DateTime minCreationDateTime;
                if (DateTime.TryParseExact(str_MinCreationDateTime, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out minCreationDateTime))
                {
                    queryReceipts = queryReceipts.Where(i => i.CreationDateTime >= minCreationDateTime).ToList();
                }
            }

            if (str_MaxCreationDateTime != null)
            {
                DateTime maxCreationDateTime;
                if (DateTime.TryParseExact(str_MaxCreationDateTime, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out maxCreationDateTime))
                {
                    queryReceipts = queryReceipts.Where(i => i.CreationDateTime <= maxCreationDateTime).ToList();
                }
            }

            return queryReceipts;
        }
    }
}
