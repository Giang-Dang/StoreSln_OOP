using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;

namespace StoreSln_OOP.Pages.Category
{
    [BindProperties]
    public class CategoriesModel : PageModel
    {
        public List<Entities.Category> Categories = new List<Entities.Category>();
        [BindProperty]
        public string? SearchName { get; set; }
        public void OnGet()
        {
            CategoryBL.ReadData(out Categories);
        }

        public void OnPost()
        {
            if (SearchName != null)
            {
                Categories = CategoryBL.FindByStringInName(SearchName);
            }
        }
    }
}
