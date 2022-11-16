using StoreSln_OOP.Entities;

namespace StoreSln_OOP.DAL
{
    public class InvoiceDA
    {
        private static DALBase<Invoice>? _invoiceDA;
        private static readonly object lockObject = new object();

        private InvoiceDA() { }

        //create unique instance
        public static DALBase<Invoice> GetDALFunction()
        {
            //thread safe
            if (_invoiceDA == null)
            {
                lock (lockObject)
                {
                    if (_invoiceDA == null)
                    {
                        _invoiceDA = new DALBase<Invoice>("Invoices.json");
                    }
                }
            }
            return _invoiceDA;
        }
    }
}
