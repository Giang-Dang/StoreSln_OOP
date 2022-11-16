using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;

namespace StoreSln_OOP.Pages.Invoices
{
    public class DeleteInvoiceModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }


        public List<Entities.Invoice> Invoices = new List<Entities.Invoice>();
        public void OnGet()
        {
            InvoiceBL.ReadData(out Invoices);

            string noti = $"Xóa hóa đơn nhập hàng (Mã: {ID}) thất bại. Do có không đảm bảo số hàng tồn sau khi xóa.";
                
            if(InvoiceBL.CanDelete(ID))
            {
                var deleteRes = InvoiceBL.RemoveAtID(ID);
            }    
            var previousPageAddress = $"../Invoice?deletenoti={noti}";
            Response.Redirect(previousPageAddress);

        }

    }
}
