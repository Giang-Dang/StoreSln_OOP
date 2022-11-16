using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;

namespace StoreSln_OOP.Pages.Invoice
{

    public class AddInvoiceModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }

        [BindProperty]
        public string? str_CreationDateTime { get; set; }

        public List<Entities.BillRecord> BillRecords = new List<Entities.BillRecord>();
        public List<Entities.Product> Products = new List<Entities.Product>();
        public List<Entities.Category> Categories = new List<Entities.Category>();
        public Entities.Invoice? Invoice;
        public void OnGet()
        {
            var now = DateTime.Now;
            str_CreationDateTime = now.ToString();
            Invoice = InvoiceBL.FindByID(ID);

            if (Invoice == default)
            {
                Invoice = new Entities.Invoice(ID, now);
                InvoiceBL.Add(Invoice);
            }

            BillRecords = Invoice.BillRecords ?? new List<Entities.BillRecord>();

            ProductBL.ReadData(out Products);
            CategoryBL.ReadData(out Categories);

            bool resAdd;
            str_CreationDateTime = Invoice.CreationDateTime.ToString();

            if (!InvoiceBL.AnyMatchID(ID))
            {
                resAdd = InvoiceBL.Add(new Entities.Invoice(ID, now));
                str_CreationDateTime = now.ToString();
            }

            Response.Redirect($"../Invoice/EditInvoice?id={ID}");
        }

        public void OnPost()
        {
        }
    }
}
