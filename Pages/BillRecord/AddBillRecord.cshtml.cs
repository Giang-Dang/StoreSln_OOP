using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreSln_OOP.BLL;
using StoreSln_OOP.Entities;

namespace StoreSln_OOP.Pages.BillRecord
{
    public class AddBillRecordModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Type { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Action { get; set; }

        [BindProperty]
        public int ProductID { get; set; }

        [BindProperty]
        public int ProductCount { get; set; }

        public bool IsNotiActive = false;
        public string[] Notifications = new string[1];

        public string? PreviousPageAddress;
        public string? Title;

        public List<Entities.Product> Products = new List<Entities.Product>();
        public void OnGet()
        {
            ProductBL.ReadData(out Products);
            PreviousPageAddress = $"../{Type}/{Action}{Type}?id={ID}";
            if(Type == "Invoice")
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
            if (BillRecordBL.IsInputValidAndReturnNoti(ProductID, ProductCount, Type == "Receipt", out Notifications))
            {
                if(Type == "Invoice")
                {
                    var resAddProductRecord = InvoiceBL.AddBillRecord(ID, ProductID, ProductCount);

                    Response.Redirect(PreviousPageAddress ?? "./Error.html");
                }    
                if(Type == "Receipt")
                {
                    var resAddProductRecord = ReceiptBL.AddBillRecord(ID, ProductID, ProductCount);
                    Response.Redirect(PreviousPageAddress ?? "./Error.html");
                }    
            }
            IsNotiActive = true;
        }


    }
}
