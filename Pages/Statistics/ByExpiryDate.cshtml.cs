using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;
using System.Globalization;

namespace StoreSln_OOP.Pages.Statistics
{
    public class ByExpiryDateModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string str_ExpiryDate { get; set; }
        [BindProperty]
        public int CategoryID { get; set; }

        public DateTime ExpiryDate;
        public List<Entities.Product> Products = new List<Entities.Product>();
        public List<Entities.Category> Categories = new List<Entities.Category>();

        public void OnGet()
        {
            ExpiryDate = DateTime.ParseExact(str_ExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            CategoryBL.ReadData(out Categories);
            Products = ProductBL.GetExpiredProducts(str_ExpiryDate);
        }

    }
}
