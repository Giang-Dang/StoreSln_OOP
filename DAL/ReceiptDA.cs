using StoreSln_OOP.Entities;

namespace StoreSln_OOP.DAL
{
    public class ReceiptDA : DALBase, IDALBase
    {
        private static ReceiptDA? _receiptDA;
        private static readonly object lockObject = new object();

        private ReceiptDA()
        {
            dataFilePath = dataFilePath = $@"{Global.DATA_DIR}/Receipts.json";
        }

        //create unique instance
        public static ReceiptDA GetDALFunction()
        {
            //thread safe
            if (_receiptDA == null)
            {
                lock (lockObject)
                {
                    if (_receiptDA == null)
                    {
                        _receiptDA = new ReceiptDA();
                    }
                }
            }
            return _receiptDA;
        }

        IDALBase IDALBase.GetDALFunction()
        {
            return CategoryDA.GetDALFunction();
        }

        public bool ReadData(out List<IContainsID> resInstances)
        {
            return ReadData(ReceiptDA.GetDALFunction().dataFilePath, out resInstances);
        }

        public bool SaveData(List<IContainsID> saveInstances)
        {
            return SaveData(ReceiptDA.GetDALFunction().dataFilePath, saveInstances);
        }

        public bool Add(IContainsID addInstance)
        {
            return Add(ReceiptDA.GetDALFunction().dataFilePath, addInstance);
        }

        public bool Update(IContainsID updateInstance)
        {
            return Update(ReceiptDA.GetDALFunction().dataFilePath, updateInstance);
        }

        public bool DeleteAtID(int id)
        {
            return DeleteAtID(ReceiptDA.GetDALFunction().dataFilePath, id);
        }
    }
}
