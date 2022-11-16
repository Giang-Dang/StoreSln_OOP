using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;

namespace StoreSln_OOP.Pages.Receipt
{
    public class DeleteReceiptModel : PageModel
    {
            [BindProperty(SupportsGet = true)]
            public int ID { get; set; }


            public List<Entities.Receipt> Receipts = new List<Entities.Receipt>();
            public void OnGet()
            {
                ReceiptBL.ReadData(out Receipts);
                var deleteRes = ReceiptBL.RemoveAtID(ID);

                var previousPageAddress = $"../Receipt";
                Response.Redirect(previousPageAddress);

            }

    }
}
