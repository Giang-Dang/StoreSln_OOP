using Newtonsoft.Json;
using StoreSln_OOP.Entities;

namespace StoreSln_OOP.DAL
{
    public class CategoryDA
    {
        private static DALBase<Category>? _categoryDA;
        private static readonly object lockObject = new object();

        private CategoryDA() { }

        //create unique instance
        public static DALBase<Category> GetDALFunction()
        {
            //thread safe
            if (_categoryDA == null)
            {
                lock (lockObject)
                {
                    if (_categoryDA == null)
                    {
                        _categoryDA = new DALBase<Category>("Categories.json");
                    }
                }
            }
            return _categoryDA;
        }
    }
}
