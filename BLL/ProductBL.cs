using StoreSln_OOP.DAL;
using StoreSln_OOP.Entities;
using System.Globalization;

namespace StoreSln_OOP.BLL
{
    public class ProductBL
    {
        public static bool ReadData(out List<Product> products)
        {
            return ProductDA.GetDALFunction().ReadData(out products);
        }

        public static bool Add(Product product)
        {
            return ProductDA.GetDALFunction().Add(product);
        }

        public static bool Update(Product product)
        {
            return ProductDA.GetDALFunction().Update(product);
        }

        public static bool RemoveAtID(int id)
        {
            return ProductDA.GetDALFunction().RemoveAtID(id);
        }

        public static Product? FindByID(int id)
        {
            List<Product> products;
            ProductDA.GetDALFunction().ReadData(out products);

            return products.FirstOrDefault(p => p.ID == id);
        }
        public static bool AnyProductInCategory(int categoryID)
        {
            List<Product> products;
            ProductDA.GetDALFunction().ReadData(out products);

            return products.Any(p => p.CategoryID == categoryID);
        }

        public static bool IsValidAndReturnNoti(
            string? name,
            int? categoryID,
            string? manufacturer,
            string? str_ExpiryDate,
            string? str_ManufacturingDate,
            decimal? price, out string[] notifications)
        {
            notifications = new string[7];
            bool res = true;

            if (name == null)
            {
                notifications[0] = "Tên mặt hàng không được để trống và phải bắt đầu bằng một chữ cái.";
                res = false;
            }

            if (categoryID <= 0)
            {
                notifications[1] = "Loại hàng không được trống.";
                res = false;
            }

            if (manufacturer == null)
            {
                notifications[2] = "Tên nhà sản xuất không được để trống và phải bắt đầu bằng một chữ cái.";
                res = false;
            }

            DateTime expiryDate;
            if (!DateTime.TryParseExact(str_ExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out expiryDate))
            {
                notifications[3] = "Hạn sử dụng không được để trống hoặc nhập sai định dạng.";
                res = false;
            }

            DateTime manufacturingDate;
            if (!DateTime.TryParseExact(str_ManufacturingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out manufacturingDate))
            {
                notifications[4] = "Ngày sản xuất không được để trống hoặc nhập sai định dạng.";
                res = false;
            }

            if (manufacturingDate >= expiryDate)
            {
                notifications[5] = "Hạn sử dụng phải sau ngày sản xuất.";
                res = false;
            }

            if (price <= 0)
            {
                notifications[6] = "Giá phải có giá trị lớn hơn 0.";
                res = false;
            }
            return res;
        }

        public static List<Product> Filter(
            string? name,
            string? str_CategoryID,
            string? manufacturer,
            string? str_MinExpiryDate,
            string? str_MaxExpiryDate,
            string? str_MinManufacturingDate,
            string? str_MaxManufacturingDate,
            string? str_MinPrice,
            string? str_MaxPrice)
        {
            List<Product> queryProducts;
            ProductDA.GetDALFunction().ReadData(out queryProducts);

            if (name != null)
            {
                queryProducts = queryProducts.Where(p => p.Name.Contains(name)).ToList();
            }

            if (str_CategoryID != null)
            {
                int categoryID = -1;
                if (Int32.TryParse(str_CategoryID, out categoryID))
                {
                    queryProducts = queryProducts.Where(p => p.CategoryID == categoryID).ToList();
                }
            }

            if (manufacturer != null)
            {
                queryProducts = queryProducts.Where(p => p.Manufacturer.Contains(manufacturer)).ToList();
            }

            if (str_MinExpiryDate != null)
            {
                DateTime minExpiryDate;
                if (DateTime.TryParseExact(str_MinExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out minExpiryDate))
                {
                    queryProducts = queryProducts.Where(p => p.ExpiryDate >= minExpiryDate).ToList();
                }
            }

            if (str_MaxExpiryDate != null)
            {
                DateTime maxExpiryDate;
                if (DateTime.TryParseExact(str_MaxExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out maxExpiryDate))
                {
                    queryProducts = queryProducts.Where(p => p.ExpiryDate <= maxExpiryDate).ToList();
                }
            }

            if (str_MinManufacturingDate != null)
            {
                DateTime minManufacturingDate;
                if (DateTime.TryParseExact(str_MinManufacturingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out minManufacturingDate))
                {
                    queryProducts = queryProducts.Where(p => p.ManufacturingDate >= minManufacturingDate).ToList();
                }
            }

            if (str_MaxManufacturingDate != null)
            {
                DateTime maxManufacturingDate;
                if (DateTime.TryParseExact(str_MaxManufacturingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out maxManufacturingDate))
                {
                    queryProducts = queryProducts.Where(p => p.ManufacturingDate <= maxManufacturingDate).ToList();
                }
            }

            if (str_MinPrice != null)
            {
                decimal minPrice;
                if (Decimal.TryParse(str_MinPrice, out minPrice))
                {
                    queryProducts = queryProducts.Where(p => p.Price >= minPrice).ToList();
                }
            }

            if (str_MaxPrice != null)
            {
                decimal maxPrice;
                if (Decimal.TryParse(str_MaxPrice, out maxPrice))
                {
                    queryProducts = queryProducts.Where(p => p.Price <= maxPrice).ToList();
                }
            }

            return queryProducts;
        }

        public static bool CanDelete(int productID)
        {
            return !InvoiceBL.AnyProductMatchID(productID) && !ReceiptBL.AnyProductMatchID(productID);
        }

        public static int CountInInvoices(int productID)
        {
            int res = 0;

            List<Invoice> invoices;
            InvoiceDA.GetDALFunction().ReadData(out invoices);

            foreach (var invoice in invoices)
            {
                foreach (var record in invoice.BillRecords)
                {
                    if (record.ProductID == productID)
                    {
                        res += record.ProductCount;
                    }
                }
            }
            return res;
        }
        public static int CountInReceipts(int productID)
        {
            int res = 0;

            List<Receipt> receipts;
            ReceiptDA.GetDALFunction().ReadData(out receipts);

            foreach (var receipt in receipts)
            {
                foreach (var record in receipt.BillRecords)
                {
                    if (record.ProductID == productID)
                    {
                        res += record.ProductCount;
                    }
                }
            }
            return res;
        }

        public static List<Product> GetExpiredProducts(string str_ExpiryDate)
        {
            List<Product> queryProducts;
            ProductDA.GetDALFunction().ReadData(out queryProducts);

            DateTime expiryDate;
            if (str_ExpiryDate != null)
            {
                if (DateTime.TryParseExact(str_ExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out expiryDate))
                {
                    queryProducts = queryProducts.Where(p => p.ExpiryDate < expiryDate).ToList();
                }
            }
            return queryProducts;
        }
    }
}
