using StoreSln_OOP.Entities;

namespace StoreSln_OOP.DAL
{
    public class CategoryDA : DALBase, IDALBase
    {
        private static CategoryDA? _categoryDA;
        private static readonly object lockObject = new object();

        private CategoryDA()
        {
            dataFilePath = dataFilePath = $@"{Global.DATA_DIR}/Categories.json";
        }

        //create unique instance
        public static CategoryDA GetDALFunction()
        {
            //thread safe
            if (_categoryDA == null)
            {
                lock(lockObject)
                {
                    if (_categoryDA == null)
                    {
                        _categoryDA = new CategoryDA();
                    }
                }
            }
            return _categoryDA;
        }

        IDALBase IDALBase.GetDALFunction()
        {
            return CategoryDA.GetDALFunction();
        }

        public bool ReadData(out List<IContainsID> resInstances)
        {
            return ReadData(CategoryDA.GetDALFunction().dataFilePath, out resInstances);
        }

        public bool SaveData(List<IContainsID> saveInstances)
        {
            return SaveData(CategoryDA.GetDALFunction().dataFilePath, saveInstances);
        }

        public bool Add(IContainsID addInstance)
        {
            return Add(CategoryDA.GetDALFunction().dataFilePath, addInstance);
        }

        public bool Update(IContainsID updateInstance)
        {
            return Update(CategoryDA.GetDALFunction().dataFilePath, updateInstance);
        }

        public bool DeleteAtID(int id)
        {
            return DeleteAtID(CategoryDA.GetDALFunction().dataFilePath, id);
        }
    }
}
