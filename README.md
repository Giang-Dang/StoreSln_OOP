# Đồ án LTHDT
DEADLINE: 6:00 PM, 19/11/2022

## ĐỀ:

#### Viết phần mềm quản lý cửa hàng với các chức năng sau:</h3>
-	Thêm, xóa, sửa và tìm kiếm các mặt hàng (mã, tên hàng, hạn dùng, công ty sản xuất, năm sản xuất, loại hàng).
-	Thêm, xóa, sửa và tìm kiếm các loại hàng.
-	Thêm, xóa, sửa và tìm kiếm các hóa đơn bán hàng
-	Thêm, xóa, sửa và tìm kiếm các hóa đơn nhập hàng
-	Thống kê số hàng còn lại trong kho theo thể loại
-	Thống kê số hàng cũ đã hết hạn sử dụng

#### Chú ý:
-	Làm với giao diện web
-	Các dữ liệu phải được lưu trữ trên tập tin
-	Phải tổ chức theo mô hình 3 lớp.
-	Không nộp chung mã nguồn với môn KTLT được.
-	File doc tự đánh giá

## CÔNG NGHỆ SỬ DỤNG:
- Razor Pages trong ASP.NET Core 6
- Mô hình 3 lớp
- OOP
- Design Pattern: Singleton, Abtract Factory

## CÁC TÍNH NĂNG
### Mặt hàng
-	Thêm các mặt hàng.
-	Sửa các thuộc tính của mặt hàng trừ mã hàng.
-	Xóa các mặt hàng: Kiểm tra mặt hàng nếu đã tồn tại trong hóa đơn thì không cho xóa và hiện thông báo.
-	Tìm kiếm mặt hàng kết hợp tùy chọn nhiều thuộc tính (, tên hàng, hạn dùng, công ty sản xuất, năm sản xuất, loại hàng).
### Loại hàng
-	Thêm loại hàng
-	Xóa loại hàng: Kiểm tra trước khi xóa nếu có sản phẩm thuộc loại hàng thì không cho xóa và hiện thông báo.
-	Tìm kiếm mặt hàng theo tên. 
### Hóa đơn nhập hàng
-	Thêm hóa đơn nhập hàng:
•	Tự gán thời gian tạo bằng thời gian hiện tại.
•	Nút thêm từng mục (gồm tên mặt hàng và số lượng). Sau khi thêm, cập nhật lại danh sách mục ở hóa đơn.
-	Sửa hóa đơn nhập hàng:
•	Ngày tạo
•	Mục lục của hóa đơn (thêm, xóa). Kiểm tra xóa mục lục có làm số lượng tồn kho của các mặt hàng bị âm không. Nếu có hiện thông báo và không cho xóa. Nếu không thì xóa mục lục.
-	Xóa hóa đơn nhập hàng:
•	Kiểm tra xem việc xóa hóa đơn có làm số lượng tồn kho của các mặt hàng bị âm không. Nếu có hiện thông báo và không cho xóa. Nếu không thì xóa hóa đơn.
-	Tìm kiếm hóa đơn theo ngày tạo.
-	Hiện thành tiền của từng mục và tổng thành tiền của hóa đơn.
### Hóa đơn bán hàng
-	Thêm hóa đơn bán hàng:
•	Tự gán thời gian tạo bằng thời gian hiện tại.
•	Nút thêm từng mục (gồm tên mặt hàng và số lượng). Sau khi thêm, cập nhật lại danh sách mục ở hóa đơn.
•	Khi thêm 1 mục. Kiểm tra xem có đủ hàng tồn không. Nếu đủ thì tạo mục mới. Nếu không hiện thông báo và không cho tạo.
-	Sửa hóa đơn bán hàng:
•	Sửa thời gian tạo đơn
•	Sửa mục lục. Có kiểm tra như lúc thêm.
-	Xóa hóa đơn nhập hàng.
-	Tìm kiếm hóa đơn theo ngày tạo
-	Hiện thành tiền của từng mục và tổng thành tiền của hóa đơn.
### Thống kê
-	Thống kê số hàng còn lại trong kho theo thể loại.
-	Thống kê số hàng cũ đã hết hạn sử dụng:
•	Mặc định là ngày hiện tại.
•	Có thể chỉnh lại ngày.

## OOP
-	Áp dụng tính đóng gói, kế thừa, đa hình và trừu tượng vào các class ở folder Entities và DAL. Các class Entities có implement interface IContainsID.
-	Trong folder DAL có Generic Class DALBase<T> where T : IContainsID. Class này chứa các hàm SaveData(), ReadData(), Add(), RemoveAtID(), Update().
-	Singleton áp dụng vào các class ProductDA, CategoryDA, InvoiceDA, ReceiptDA để tạo 1 instance từ class DALBase<T> (T tương ứng với Product cho ProductDA, Category cho CategoryDA,…).

## SCREENSHOTS:
![Screenshot_OOP_01.png](/Screenshots/Screenshot_OOP_01.png)
![Screenshot_OOP_02.png](/Screenshots/Screenshot_OOP_02.png)
![Screenshot_OOP_03.png](/Screenshots/Screenshot_OOP_03.png)
![Screenshot_OOP_04.png](/Screenshots/Screenshot_OOP_04.png)
![Screenshot_OOP_05.png](/Screenshots/Screenshot_OOP_05.png)
![Screenshot_OOP_06.png](/Screenshots/Screenshot_OOP_06.png)
