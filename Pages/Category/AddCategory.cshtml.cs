using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;



namespace StoreSln_OOP.Pages.Category
{
    public class AddCategoryModel : PageModel
    {
        [BindProperty]
        public string? Name { get; set; }

        public bool IsNotiActive = false;
        public string[] Notifications = new string[1];
        public void OnGet()
        {
            Name = String.Empty;
            IsNotiActive = false;
        }

        public void OnPost()
        {
            if (Name != null)
            {
                if (CategoryBL.IsInputValidAndReturnNoti(Name, out Notifications))
                {
                    
                    CategoryBL.Add(new Entities.Category(0, Name));
                    Response.Redirect("/Category/Index");
                }
            }
            
            IsNotiActive = true;
        }
    }
}
