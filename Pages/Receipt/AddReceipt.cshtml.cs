using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;

namespace StoreSln_OOP.Pages.Receipt
{

    public class AddReceiptModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }

        [BindProperty]
        public string? str_CreationDateTime { get; set; }

        public List<Entities.BillRecord> BillRecords = new List<Entities.BillRecord>();
        public List<Entities.Product> Products = new List<Entities.Product>();
        public List<Entities.Category> Categories = new List<Entities.Category>();
        public Entities.Receipt? Receipt;
        public void OnGet()
        {
            var now = DateTime.Now;
            str_CreationDateTime = now.ToString();
            Receipt = ReceiptBL.FindByID(ID);
            if (Receipt == default)
            {
                Receipt = new Entities.Receipt(ID, now);
                ReceiptBL.Add(Receipt);
            }

            BillRecords = Receipt.BillRecords ?? new List<Entities.BillRecord>();
            ProductBL.ReadData(out Products);
            CategoryBL.ReadData(out Categories);

            bool resAdd;
            str_CreationDateTime = Receipt.CreationDateTime.ToString();
            if (!ReceiptBL.AnyMatchID(ID))
            {
                resAdd = ReceiptBL.Add(new Entities.Receipt(ID, now));
                str_CreationDateTime = now.ToString();
            }

            Response.Redirect($"../Receipt/EditReceipt?id={ID}");
        }

        public void OnPost()
        {

        }
    }
}
