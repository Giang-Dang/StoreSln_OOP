using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;

namespace StoreSln_OOP.Pages.Product
{
    public class EditProductModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }
        [BindProperty]
        public string? Name { get; set; }
        [BindProperty]
        public int CategoryID { get; set; }
        [BindProperty]
        public Decimal Price { get; set; }
        [BindProperty]
        public string? Manufacturer { get; set; }
        [BindProperty]
        public string? str_ExpiryDate { get; set; }
        [BindProperty]
        public string? str_ManufacturingDate { get; set; }

        public Entities.Product? Product;
        public Entities.Category? Category;
        public List<Entities.Category> Categories = new List<Entities.Category>();
        
        public bool IsNotiActive = false;
        public string[] Notifications = new string[1];
        public void OnGet()
        {
            Product = ProductBL.FindByID(ID);
            if(Product == default)
            {
                return;
            }

            Category = CategoryBL.FindByID(Product.CategoryID);
            CategoryBL.ReadData(out Categories);
            IsNotiActive = false;
        }

        public void OnPost()
        {
            Product = ProductBL.FindByID(ID);
            if(Product == default)
            {
                return;
            }
            Category = CategoryBL.FindByID(Product.CategoryID);

            DateTime expiryDate;
            if(!DateTime.TryParse(str_ExpiryDate, out expiryDate))
            {
                expiryDate = Product.ExpiryDate;
            }    

            DateTime manufacturingDate;
            if (!DateTime.TryParse(str_ManufacturingDate, out manufacturingDate))
            {
                manufacturingDate = Product.ManufacturingDate;
            }

            bool isInputValid = ProductBL.IsValidAndReturnNoti(Name, CategoryID, Manufacturer, str_ExpiryDate, str_ManufacturingDate, Price,out Notifications);
            if(isInputValid)
            {
                Product = new Entities.Product(ID, Name, Price, CategoryID, expiryDate, manufacturingDate, Manufacturer);
                var editRes = ProductBL.Update(Product);
                Response.Redirect("/Product");
            }
            IsNotiActive = true;
        }
    }
}
