using StoreSln_OOP.DAL;
using StoreSln_OOP.Entities;
using System.Xml.Linq;

namespace StoreSln_OOP.BLL
{
    public class CategoryBL
    {
        public static bool ReadData(out List<Category> categories)
        {
            return CategoryDA.GetDALFunction().ReadData(out categories);
        }

        public static bool Add(Category category)
        {
            return CategoryDA.GetDALFunction().Add(category);
        }

        public static bool Update(Category category)
        {
            return CategoryDA.GetDALFunction().Update(category);
        }

        public static bool RemoveAtID(int id)
        {
            return CategoryDA.GetDALFunction().RemoveAtID(id);
        }

        public static Category? FindByID(int id)
        {
            List<Category> categories;
            if(ReadData(out categories))
            {
                return categories.First(c => c.ID == id);
            }
            return default;
        }
        public static Category? FindByName(string name)
        {
            List<Category> categories;
            if (ReadData(out categories))
            {
                return categories.First(c => c.Name == name);
            }
            return default;
        }
        public static List<Category> FindByStringInName(string sname)
        {
            List<Category> categories;
            if (ReadData(out categories))
            {
                return categories.Where(c => c.Name.Contains(sname ?? "")).ToList();
            }
            return new List<Category>();
        }

        public static bool IsInputValidAndReturnNoti(string name, out string[] notifications)
        {
            var res = true;
            notifications = new string[2];

            List<Category> categories;
            CategoryDA.GetDALFunction().ReadData(out categories);

            if (name == null)
            {
                notifications[0] = "Tên loại hàng không được để trống.";
                res = false;
            }

            if (categories.Any(c => c.Name == name))
            {
                notifications[1] = "Tên loại hàng đã được dùng. Xin vui lòng nhập tên khác.";
                res = false;
            }

            return res;
        }
    }
}
