using StoreSln_OOP.DAL;
using StoreSln_OOP.Entities;

namespace StoreSln_OOP.BLL
{
    public class BillRecordBL
    {
        public static bool IsInputValidAndReturnNoti(int productID, int productCount, bool isReceipt, out string[] notifications)
        {
            notifications = new string[3];
            bool res = true;

            if (productID <= 0)
            {
                notifications[0] = "Mã sản phẩm không được trống. (Nhập sản phẩm nếu chưa có sản phẩm nào trong danh sách)";
                res = false;
            }
            if (productCount < 1)
            {
                notifications[1] = "Số lượng sản phẩm không được để trống hoặc nhỏ hơn 1.";
                res = false;
            }

            if (isReceipt)
            {
                int productLeftCount = ProductBL.CountInInvoices(productID) - ProductBL.CountInReceipts(productID);
                
                List<Product> products;
                ProductDA.GetDALFunction().ReadData(out products);

                var productName = ProductBL.FindByID(productID)?.Name;
                if (productCount > productLeftCount)
                {
                    notifications[2] = $"Số lượng xuất kho của sản phẩm {productName} không được lớn số lượng tồn kho ({productLeftCount}).";
                    res = false;
                }
            }

            return res;
        }

        public static BillRecord? FindByID(bool isReceipt, int invoiceOrReceiptID, int recordID)
        {
            if (isReceipt)
            {
                return ReceiptBL.FindByID(invoiceOrReceiptID)?.BillRecords.FirstOrDefault(pr => pr.ID == recordID);
            }
            return InvoiceBL.FindByID(invoiceOrReceiptID)?.BillRecords.FirstOrDefault(pr => pr.ID == recordID);
        }

        public static bool CanDeleteInInvoice(int invoiceID, int recordID)
        {
            var billRecord = FindByID(false, invoiceID, recordID);
            
            if (billRecord == null)
            {
                throw new Exception($"Bill Record {recordID} in Invoice {invoiceID} does not exist.");
            }
            
            var productLeftCount = ProductBL.CountInInvoices(billRecord.ProductID) - ProductBL.CountInReceipts(billRecord.ProductID);
            return productLeftCount - billRecord.ProductCount >= 0;
        }
    }
}
