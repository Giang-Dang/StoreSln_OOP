using StoreSln_OOP.Entities;

namespace StoreSln_OOP.DAL
{
    public class ProductDA
    {
        private static DALBase<Product>? _productDA;
        private static readonly object lockObject = new object();

        private ProductDA() { }

        //create unique instance
        public static DALBase<Product> GetDALFunction()
        {
            //thread safe
            if (_productDA == null)
            {
                lock (lockObject)
                {
                    if (_productDA == null)
                    {
                        _productDA = new DALBase<Product>("Products.json");
                    }
                }
            }
            return _productDA;
        }
    }
}
