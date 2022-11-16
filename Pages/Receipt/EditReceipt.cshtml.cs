using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;

namespace StoreSln_OOP.Pages.Receipt
{
    public class EditReceiptModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }

        [BindProperty]
        public string? str_CreationDateTime { get; set; }
        [BindProperty]
        public string? str_NewCreationDateTime { get; set; }

        public decimal TotalAmount = 0;

        public List<Entities.BillRecord> BillRecords = new List<Entities.BillRecord>();
        public List<Entities.Product> Products = new List<Entities.Product>();
        public List<Entities.Category> Categories = new List<Entities.Category>();
        public Entities.Receipt? Receipt;
        public void OnGet()
        {
            var now = DateTime.Now;
            Receipt = ReceiptBL.FindByID(ID);
            if(Receipt == null)
            {
                return;
            }

            BillRecords = Receipt.BillRecords;
            ProductBL.ReadData(out Products);
            CategoryBL.ReadData(out Categories);

            bool resAdd;
            if (!ReceiptBL.AnyMatchID(ID))
            {
                resAdd = ReceiptBL.Add(new Entities.Receipt(ID, now));
                str_CreationDateTime = now.ToString();
            }
            str_CreationDateTime = Receipt.CreationDateTime.ToString();

        }

        public void OnPost()
        {
            DateTime creationDateTime;
            Receipt = ReceiptBL.FindByID(ID);
            if(Receipt == null)
            {
                return;
            }

            if (str_NewCreationDateTime == null || !DateTime.TryParse(str_NewCreationDateTime, out creationDateTime))
            {
                creationDateTime = Receipt.CreationDateTime;
            }    
            var editRes = ReceiptBL.Update(new Entities.Receipt(ID, creationDateTime, Receipt.BillRecords));
            Response.Redirect("../Receipt");
        }
    }
}
