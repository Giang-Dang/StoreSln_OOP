using StoreSln_OOP.Entities;

namespace StoreSln_OOP.DAL
{
    public class ProductDA : DALBase, IDALBase
    {
        private static ProductDA? _productDA;
        private static readonly object lockObject = new object();

        private ProductDA()
        {
            dataFilePath = dataFilePath = $@"{Global.DATA_DIR}/Products.json";
        }

        //create unique instance
        public static ProductDA GetDALFunction()
        {
            //thread safe
            if (_productDA == null)
            {
                lock (lockObject)
                {
                    if (_productDA == null)
                    {
                        _productDA = new ProductDA();
                    }
                }
            }
            return _productDA;
        }

        IDALBase IDALBase.GetDALFunction()
        {
            return CategoryDA.GetDALFunction();
        }

        public bool ReadData(out List<IContainsID> resInstances)
        {
            return ReadData(ProductDA.GetDALFunction().dataFilePath, out resInstances);
        }

        public bool SaveData(List<IContainsID> saveInstances)
        {
            return SaveData(ProductDA.GetDALFunction().dataFilePath, saveInstances);
        }

        public bool Add(IContainsID addInstance)
        {
            return Add(ProductDA.GetDALFunction().dataFilePath, addInstance);
        }

        public bool Update(IContainsID updateInstance)
        {
            return Update(ProductDA.GetDALFunction().dataFilePath, updateInstance);
        }

        public bool DeleteAtID(int id)
        {
            return DeleteAtID(ProductDA.GetDALFunction().dataFilePath, id);
        }
    }
}
