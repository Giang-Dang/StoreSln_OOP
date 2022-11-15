using StoreSln_OOP.DAL;
using StoreSln_OOP.Entities;

namespace StoreSln_OOP.BLL
{
    public class CategoryBL
    {
        public static bool ReadData(out List<Category> categories)
        {
            IDALBase dalInstance = CategoryDA.GetDALFunction();

            return dalInstance.GetDALFunction().ReadData(out categories);
        }

        public static bool Add(Category category)
        {
            IDALBase dalInstance = CategoryDA.GetDALFunction();

            return dalInstance.GetDALFunction().Add(category);
        }

        public static bool Update(Category category)
        {
            IDALBase dalInstance = CategoryDA.GetDALFunction();

            return dalInstance.GetDALFunction().Update(category);
        }

        public static bool DeleteAtID(int id)
        {
            IDALBase dalInstance = CategoryDA.GetDALFunction();

            return dalInstance.GetDALFunction().DeleteAtID(id);
        }

        public static Category FindByID(int id)
        {
            IContainsID categories;
            if(ReadData(out categories))
            return categories.First(c => c.ID == id);
        }
        public static Category FindByName(string name)
        {
            var categories = ReadData();
            return categories.First(c => c.Name == name);
        }
        public static List<Category> FindByStringInName(string sname)
        {
            var categories = ReadData();
            return categories.Where(c => c.Name.Contains(sname ?? "")).ToList();
        }
    }
}
