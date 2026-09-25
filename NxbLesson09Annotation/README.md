# NxbLesson09Annotation

Bài thực hành ASP.NET Core MVC .NET 8 theo video Lesson 09, sau đó làm tiếp PDF "Bài thực hành 05 - Data Validate & Annotation", bao gồm bài tập tự làm cuối tài liệu.

## Mở và chạy trên Visual Studio 2022

1. Cài workload **ASP.NET and web development** và **.NET 8 SDK**.
2. Mở `NxbLesson09Annotation.sln` bằng Visual Studio 2022.
3. Chọn profile `https` hoặc `http`, bấm **F5**. Không cần SQL Server, NuGet bên ngoài hay migration.
4. Vào trang chủ để mở lần lượt **Thông tin sinh viên**, **Thành viên**, **Account**, **Sản phẩm**.

Phần video: `Home/NxbAbout`, model `NxbMember`, form `NxbMember/Create` dùng Data Annotation và `ModelState`. Phần PDF: `Account/Create` có các thuộc tính và xác thực từ tài liệu, `Account/VerifyPhone` hỗ trợ kiểm tra khi nhập; trang cuối: `Product` có danh mục dạng danh sách chọn, CRUD, ảnh lưu tại `wwwroot/products`, dữ liệu sản phẩm trong `Data/products.json`. Danh mục mẫu ở `Services/ProductStore.cs`.

Thông tin sinh viên: Nguyễn Xuân Bắc, K24CNT2, 2410900011, bac0942939882@gmail.com. Chuỗi số 2410900011 trong video được dùng làm mã sinh viên và người dùng cũng yêu cầu dùng làm số liên hệ trong trang thông tin; biểu mẫu Account chấp nhận 10 chữ số, có thể ngăn bằng dấu chấm hoặc dấu gạch ngang theo biểu thức ở PDF.

Lưu ý: phần Member và Account dùng danh sách trong bộ nhớ để minh họa theo bài giảng, nên dữ liệu sẽ mất khi khởi động lại; Product được lưu thành JSON. Đây là project bài tập thực hành, không phải hệ thống tài khoản đăng nhập; mật khẩu Member chỉ dùng để minh họa và không được đưa lên trang danh sách.
