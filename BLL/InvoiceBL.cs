using StoreSln_OOP.DAL;
using StoreSln_OOP.Entities;
using System.Globalization;

namespace StoreSln_OOP.BLL
{
    public class InvoiceBL
    {
        public static bool ReadData(out List<Invoice> invoices)
        {
            return InvoiceDA.GetDALFunction().ReadData(out invoices);
        }

        public static bool Add(Invoice invoice)
        {
            return InvoiceDA.GetDALFunction().Add(invoice);
        }

        public static bool Update(Invoice invoice)
        {
            return InvoiceDA.GetDALFunction().Update(invoice);
        }

        public static bool RemoveAtID(int id)
        {
            return InvoiceDA.GetDALFunction().RemoveAtID(id);
        }

        public static int GetNextID()
        {
            List<Invoice> invoices;
            ReadData(out invoices);
            int res = 1;
            if (invoices.Any())
            {
                res = invoices.Max(i => i.ID) + 1;
            }
            return res;
        }

        public static bool AddBillRecord(int invoiceID, int productID, int productCount)
        {
            List<Product> products;
            ProductDA.GetDALFunction().ReadData(out products);

            List<Invoice> invoices;
            InvoiceDA.GetDALFunction().ReadData(out invoices);

            var invoice = invoices.FirstOrDefault(i => i.ID == invoiceID);

            int recordID = 1;
            if (invoice.BillRecords == null)
            {
                invoice.BillRecords = new List<BillRecord>();
            }
            if (invoice.BillRecords.Any())
            {
                recordID = invoice.BillRecords.Max(r => r.ID) + 1;
            }
            BillRecord record = new BillRecord(recordID, productID, productCount);
            invoice.BillRecords.Add(record);

            InvoiceDA.GetDALFunction().Update(invoice);

            return true;
        }
        public static bool AnyMatchID(int id)
        {
            List<Invoice> invoices;
            InvoiceDA.GetDALFunction().ReadData(out invoices);

            return invoices.Any(i => i.ID == id);
        }

        public static Invoice? FindByID(int id)
        {
            List<Invoice> invoices;
            InvoiceDA.GetDALFunction().ReadData(out invoices);

            return invoices.FirstOrDefault(i => i.ID == id);
        }

        public static bool AnyProductMatchID(int productID)
        {
            List<Invoice> invoices;
            InvoiceDA.GetDALFunction().ReadData(out invoices);

            var res = false;
            foreach (var invoice in invoices)
            {
                foreach (var billRecord in invoice.BillRecords)
                {
                    if (billRecord.ProductID == productID)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }

        public static List<Invoice> Filter(string? str_MinCreationDateTime, string? str_MaxCreationDateTime)
        {
            List<Invoice> queryInvoices;
            InvoiceDA.GetDALFunction().ReadData(out queryInvoices);

            if (str_MinCreationDateTime != null)
            {
                DateTime minCreationDateTime;
                if (DateTime.TryParseExact(str_MinCreationDateTime, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out minCreationDateTime))
                {
                    queryInvoices = queryInvoices.Where(i => i.CreationDateTime >= minCreationDateTime).ToList();
                }
            }

            if (str_MaxCreationDateTime != null)
            {
                DateTime maxCreationDateTime;
                if (DateTime.TryParseExact(str_MaxCreationDateTime, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out maxCreationDateTime))
                {
                    queryInvoices = queryInvoices.Where(i => i.CreationDateTime <= maxCreationDateTime).ToList();
                }
            }

            return queryInvoices;
        }

        public static bool CanDelete(int invoiceID)
        {
            var invoice = FindByID(invoiceID);
            foreach (var record in invoice.BillRecords)
            {
                if (!BillRecordBL.CanDeleteInInvoice(invoiceID, record.ID))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
