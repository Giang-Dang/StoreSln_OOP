using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;
using StoreSln_OOP.Entities;

namespace StoreSln_OOP.Pages.BillRecord
{
    public class DeleteProductRecordModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Type { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Action { get; set; }
        [BindProperty(SupportsGet = true)]
        public int RecordID { get; set; }

        public List<Entities.Invoice> Invoices = new List<Entities.Invoice>();
        public List<Entities.Receipt> Receipts = new List<Entities.Receipt>();
        public void OnGet()
        {
            var previousPageAddress = $"../{Type}s/{Action}{Type}?id={ID}";

            if (Type == "Invoice")
            {
                InvoiceBL.ReadData(out Invoices);
                var invoice = InvoiceBL.FindByID(ID);
                if(invoice == null)
                {
                    return;
                }

                var productRecord = invoice.BillRecords.First(r => r.ID == RecordID);
                invoice.BillRecords.Remove(productRecord);
                InvoiceBL.Update(new Entities.Invoice(invoice.ID, invoice.CreationDateTime, invoice.BillRecords));
            }

            if (Type == "Receipt")
            {
                ReceiptBL.ReadData(out Receipts);
                var receipt = ReceiptBL.FindByID(ID);
                if (receipt == null)
                {
                    return;
                }

                var productRecord = receipt.BillRecords.First(r => r.ID == RecordID);
                receipt.BillRecords.Remove(productRecord);
                ReceiptBL.Update(new Entities.Receipt(receipt.ID, receipt.CreationDateTime, receipt.BillRecords));
            }
            Response.Redirect(previousPageAddress);
        }

    }
}
