using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;

namespace StoreSln_OOP.Pages.BillRecord
{
    public class EditBillRecordModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Type { get; set; }

        [BindProperty]
        public int ProductID { get; set; }

        [BindProperty]
        public int ProductCount { get; set; }

        public string? Title;

        public List<Entities.Product> Products = new List<Entities.Product>();

        public bool IsNotiActive = false;
        public string[] Notifications = new string[1];
        public void OnGet()
        {
            IsNotiActive = false;
            ProductBL.ReadData(out Products);

            if (Type == "Invoice")
            {
                Title = "Thêm danh mục vào hóa đơn nhập hàng";
            }
            if (Type == "Receipt")
            {
                Title = "Thêm danh mục vào hóa đơn bán hàng";
            }
        }

        public void OnPost()
        {
            OnGet();
            bool isInvoice = (Type == "Invoice");
            bool isReceipt = (Type == "Receipt");
            bool isInputValid = BillRecordBL.IsInputValidAndReturnNoti(ProductID, ProductCount, isReceipt, out Notifications);
            
            if(isInputValid)
            {
                bool resAddProductRecord;
                if (isInvoice)
                {
                    resAddProductRecord = InvoiceBL.AddBillRecord(ID, ProductID, ProductCount);
                }
                if (isReceipt)
                {
                    resAddProductRecord = ReceiptBL.AddBillRecord(ID, ProductID, ProductCount);
                }

                Response.Redirect($"../{Type}/Add{Type}?id={ID}");
            }

            IsNotiActive = true;
        }
    }
}
