using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;

namespace StoreSln_OOP.Pages.Category
{
    public class EditCategoryModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }
        [BindProperty]
        public string? Name { get; set; }

        public bool IsNotiActive;
        public string?[] Notifications = new string[1]; 

        public Entities.Category? Category;
        public void OnGet()
        {
            IsNotiActive = false;
            if(ID != 0)
            {
                Category = CategoryBL.FindByID(ID);
            }    
        }
        public void OnPost()
        {
            OnGet();
            bool isInputValid = CategoryBL.IsInputValidAndReturnNoti(Name, out Notifications);
            if(isInputValid)
            {
                var category = new Entities.Category(ID, Name);
                bool editRes = CategoryBL.Update(category);
                Response.Redirect("/Category/Index");
            }
            IsNotiActive = true;
        }
    }
}
