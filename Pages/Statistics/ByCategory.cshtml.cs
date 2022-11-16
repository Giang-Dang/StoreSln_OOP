using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;


namespace StoreSln_OOP.Pages.Statistics
{
    public class ByCategoryModel : PageModel
    {
        [BindProperty]
        public int CategoryID { get; set; }
        public List<Entities.Product> Products = new List<Entities.Product>();
        public List<Entities.Category> Categories = new List<Entities.Category>();

        public void OnGet()
        {
            ProductBL.ReadData(out Products);
            CategoryBL.ReadData(out Categories);
        }

        public void OnPost()
        {
            OnGet();
            if(CategoryID > 0)
            {
                Products = ProductBL.Filter(null, CategoryID.ToString(), null, null, null, null, null, null, null);
            }    
        }
    }
}
