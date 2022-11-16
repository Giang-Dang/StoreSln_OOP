
using StoreSln_OOP.Entities;

namespace StoreSln_OOP.DAL
{
    public class ReceiptDA
    {
        private static DALBase<Receipt>? _receiptDA;
        private static readonly object lockObject = new object();

        private ReceiptDA() { }

        //create unique instance
        public static DALBase<Receipt> GetDALFunction()
        {
            //thread safe
            if (_receiptDA == null)
            {
                lock (lockObject)
                {
                    if (_receiptDA == null)
                    {
                        _receiptDA = new DALBase<Receipt>("Receipts.json");
                    }
                }
            }
            return _receiptDA;
        }
    }
}
