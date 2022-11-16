using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;

namespace StoreSln_OOP.Pages.Receipt
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string? str_MinCreationDateTime { get; set; }
        [BindProperty]
        public string? str_MaxCreationDateTime { get; set; }
        public int nextID { get; set; }
        public List<Entities.Receipt> Receipts = new List<Entities.Receipt>();
        public List<Entities.Category> Categories = new List<Entities.Category>();

        public string[] Notifications = new string[1];
        public bool IsNotiActive = false;
        public void OnGet()
        {
            ReceiptBL.ReadData(out Receipts);
            nextID = ReceiptBL.GetNextID();

            CategoryBL.ReadData(out Categories);

            IsNotiActive = false;
        }

        public void OnPost()
        {
            OnGet();
            Receipts = ReceiptBL.Filter(str_MinCreationDateTime, str_MaxCreationDateTime);
            if(!Receipts.Any())
            {

                Notifications = new string[1];
                Notifications[0] = "Không có hóa đơn nào thỏa yêu cầu tìm kiếm.";
                IsNotiActive = true;
            }    
        }
    }
}
