using StoreSln_OOP.Entities;

namespace StoreSln_OOP.DAL
{
    public class InvoiceDA : DALBase, IDALBase
    {
        private static InvoiceDA? _invoiceDA;
        private static readonly object lockObject = new object();

        private InvoiceDA()
        {
            dataFilePath = dataFilePath = $@"{Global.DATA_DIR}/Invoices.json";
        }

        //create unique instance
        public static InvoiceDA GetDALFunction()
        {
            //thread safe
            if (_invoiceDA == null)
            {
                lock (lockObject)
                {
                    if (_invoiceDA == null)
                    {
                        _invoiceDA = new InvoiceDA();
                    }
                }
            }
            return _invoiceDA;
        }
        IDALBase IDALBase.GetDALFunction()
        {
            return CategoryDA.GetDALFunction();
        }

        public bool ReadData(out List<IContainsID> resInstances)
        {
            return ReadData(InvoiceDA.GetDALFunction().dataFilePath, out resInstances);
        }

        public bool SaveData(List<IContainsID> saveInstances)
        {
            return SaveData(InvoiceDA.GetDALFunction().dataFilePath, saveInstances);
        }

        public bool Add(IContainsID addInstance)
        {
            return Add(InvoiceDA.GetDALFunction().dataFilePath, addInstance);
        }

        public bool Update(IContainsID updateInstance)
        {
            return Update(InvoiceDA.GetDALFunction().dataFilePath, updateInstance);
        }

        public bool DeleteAtID(int id)
        {
            return DeleteAtID(InvoiceDA.GetDALFunction().dataFilePath, id);
        }
    }
}
