using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;

namespace StoreSln_OOP.Pages.Invoice
{
    public class EditInvoiceModel : PageModel
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
        public Entities.Invoice? Invoice;
        public void OnGet()
        {
            var now = DateTime.Now;
            Invoice = InvoiceBL.FindByID(ID);
            
            if(Invoice == null)
            {
                return;
            }
            BillRecords = Invoice.BillRecords ?? new List<Entities.BillRecord>();
            ProductBL.ReadData(out Products);
            CategoryBL.ReadData(out Categories);

            bool resAdd;
            if (!InvoiceBL.AnyMatchID(ID))
            {
                resAdd = InvoiceBL.Add(new Entities.Invoice(ID, now));
                str_CreationDateTime = now.ToString();
            }
            str_CreationDateTime = Invoice.CreationDateTime.ToString();

        }

        public void OnPost()
        {
            DateTime creationDateTime;
            Invoice = InvoiceBL.FindByID(ID);
            if(Invoice == null)
            {
                return;
            }    
            if (str_NewCreationDateTime == null || !DateTime.TryParse(str_NewCreationDateTime, out creationDateTime))
            {
                creationDateTime = Invoice.CreationDateTime;
            }    
            var editRes = InvoiceBL.Update(new Entities.Invoice(ID, creationDateTime, Invoice.BillRecords));
            Response.Redirect("../Invoice");
        }
    }
}
