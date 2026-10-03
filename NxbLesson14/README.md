# NxbLesson14

Project thực hành theo PDF “Tìm hiểu về Layout”. Tên project, solution và namespace đều là **NxbLesson14**.

## Mở và chạy bằng Visual Studio

1. Giải nén toàn bộ ZIP vào một thư mục trên máy. Mở `NxbLesson14.sln`.
2. Dùng Visual Studio 2022 có workload **ASP.NET and web development** và SDK **.NET 8**. Chờ NuGet khôi phục các package.
3. Cấu hình mặc định dùng **SQL Server Express LocalDB**. Cài thành phần **SQL Server Express LocalDB** trong Visual Studio Installer nếu máy chưa có.
4. Chọn project `NxbLesson14` làm **Set as Startup Project**, chọn profile `http`, nhấn **Ctrl+F5**.
5. Mở `http://localhost:5140/`. Vào quản trị ở `http://localhost:5140/Admin`.

Ở lần chạy đầu, ứng dụng tự tạo `NxbLesson14Db`, bốn bảng và dữ liệu mẫu. Không cần chạy lệnh migration hoặc file SQL trước. Dữ liệu CRUD được lưu vào CSDL, giữ lại khi tắt ứng dụng.

Mở **HUONG_DAN_VISUAL_STUDIO.html** để xem hướng dẫn từng bước, tên template và các file cần tạo.

## Template đã chọn

| Thành phần | Template / kiểu item |
| --- | --- |
| Project | ASP.NET Core Web App (Model-View-Controller), C#, .NET 8 |
| Area | MVC Area, tên Admin |
| Controller cơ bản | MVC Controller - Empty |
| View | Razor View - Empty / Empty (without model) |
| Layout | Razor Layout hoặc Razor View để tạo file .cshtml |
| CRUD | MVC Controller with views, using Entity Framework |
| Giao diện người dùng | Bistup của OS Templates |
| Giao diện quản trị | AdminLTE 3.2.0, Bootstrap 4, Font Awesome |

## Các phần đã làm

| Trang PDF | Nội dung | File chính |
| --- | --- | --- |
| 1–8 | Layout, ViewImports, ViewStart, Css/Scripts section | `Views/Shared/_Layout.cshtml`, `_BootstrapMain.cshtml`, `Views/_ViewImports.cshtml`, `Views/_ViewStart.cshtml`, `Views/Home/LayoutDemo.cshtml` |
| 9–16 | Admin Area, Dashboard và Category, cấu hình route | `Areas/Admin/Controllers/`, `Areas/Admin/Views/`, `Program.cs` |
| 16–21 | Ghép template Bistup thành layout người dùng | `Views/Shared/_main.cshtml`, `_Header.cshtml`, `_Footer.cshtml`, `Views/Home/Index.cshtml`, `wwwroot/css`, `wwwroot/js`, `wwwroot/images` |
| 21–23 | Ghép AdminLTE 3, tách component menu | `Areas/Admin/Views/Shared/Admin.cshtml`, `ViewComponents/`, `Areas/Admin/Views/Shared/Components/`, `wwwroot/dist`, `wwwroot/plugins` |
| 23–25 | CRUD Category, Product, Banner, Blog | `Models/`, `Data/`, bốn admin controller và bốn thư mục view CRUD |

Tài liệu có một số vị trí ghi “Admins”; cấu trúc cuối của tài liệu dùng “Admin”. Project thống nhất **Admin**, truy cập bằng `/Admin`.

`_BootstrapMain.cshtml` giữ lại phần minh họa trước khi thay bằng HTML template. Xem ở `/Home/LayoutDemo`. Layout mặc định cuối cùng là `_main.cshtml` (Bistup).

## Đường dẫn để kiểm tra

| Trang | URL |
| --- | --- |
| Trang chủ Bistup | `/` |
| Giới thiệu và liên hệ | `/Home/About`, `/Home/Contact` |
| Minh họa layout và section | `/Home/LayoutDemo` |
| Sản phẩm / bài viết hiển thị | `/Product`, `/Blog` |
| Dashboard AdminLTE | `/Admin` |
| CRUD danh mục | `/Admin/Category` |
| CRUD sản phẩm | `/Admin/Product` |
| CRUD banner | `/Admin/Banner` |
| CRUD bài viết | `/Admin/Blog` |

Mỗi trang quản trị có tìm kiếm, thêm, chi tiết, sửa và xóa. Trạng thái `1` là hiển thị, `0` là ẩn. Sản phẩm và bài viết bị ẩn không xuất hiện ở trang người dùng. Sản phẩm thuộc danh mục ẩn cũng không hiển thị.

## Dữ liệu và kiểm tra form

- `Id`: khóa chính int tự tăng.
- `Name`: bắt buộc, tối đa 100 ký tự, không trùng trong cùng bảng. Tên được bỏ khoảng trắng đầu/cuối; kiểm tra trùng không phân biệt hoa/thường.
- `Status`: tinyint, mặc định 1; form chọn 0 hoặc 1.
- `CreatedDate`: date, mặc định ngày hiện tại (Category, Product, Blog).
- `Image`: varchar(100), cho phép trống. Điền đường dẫn ảnh nội bộ, ví dụ `/images/demo/348x261.png`. Thêm ảnh khác vào `wwwroot/images` rồi nhập đường dẫn tương ứng.
- `Description`: nvarchar(350), cho phép trống.
- Product có `Price` float bắt buộc; `salePrice` float mặc định 0; `CategoryId` là khóa ngoại bắt buộc. Giá không âm, giá khuyến mại không vượt giá bán.
- Banner giữ nguyên tên cột **Prioty** như tài liệu, int mặc định 0.
- Không xóa danh mục đang có sản phẩm. Chuyển sản phẩm sang danh mục khác hoặc xóa sản phẩm trước.

Các form kiểm tra cả trên trình duyệt và ở controller. Tên có unique index tại CSDL; khóa ngoại Product → Category không xóa dây chuyền. Nút xóa mở trang xác nhận, thao tác POST có anti-forgery token.

## Nếu dùng SQL Server khác LocalDB

Sửa `ConnectionStrings:DefaultConnection` trong `NxbLesson14/appsettings.json`. Ví dụ dùng SQL Server Express:

```json
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=NxbLesson14Db;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

Giữ `DatabaseProvider` là `SqlServer`. Tài khoản Windows cần quyền tạo CSDL trong SQL Server đang dùng.

`Database/NxbLesson14.sql` là cấu trúc SQL Server sinh trực tiếp từ model EF Core, để đọc hoặc tạo bảng thủ công trong CSDL trống. Nếu tự tạo bảng bằng script trước khi chạy, ứng dụng không tự thêm bộ dữ liệu mẫu; có thể thêm dữ liệu qua các trang CRUD.

## Chạy nhanh khi chưa cài SQL Server

Trong `appsettings.json`, đổi duy nhất:

```json
"DatabaseProvider": "Sqlite"
```

Ứng dụng sẽ dùng file `NxbLesson14.db`, tự tạo ở lần chạy đầu. Đây là lựa chọn chạy thử tiện lợi; cấu hình và script SQL Server vẫn được giữ sẵn. SQLite và SQL Server là hai CSDL riêng, đổi provider không tự chuyển dữ liệu.

## Kiểm tra và mã nguồn

Chi tiết kết quả kiểm tra nằm trong `KET_QUA_KIEM_TRA.md`. Cấu hình mặc định SQL Server LocalDB dành cho Visual Studio trên Windows; kiểm tra chạy CRUD trong môi trường tạo file được thực hiện bằng SQLite.

ZIP có đầy đủ mã nguồn, `.sln`, `.csproj`, tài nguyên giao diện và hướng dẫn; không chứa `bin`, `obj`, `.vs`, CSDL kiểm tra hoặc Git repository. Có `.gitignore` để bạn tự đưa mã nguồn lên GitHub.

## Nguồn giao diện và tài liệu tham khảo

- Bistup: OS Templates. Bản mẫu được lấy từ kho mirror `https://github.com/sadafimtiaz/Bistup`; nội dung mẫu khớp tiêu đề “Metus purus pharetra sit” và các section trong PDF. Đã giữ credit và licence.
- AdminLTE 3.2.0: `https://github.com/ColorlibHQ/AdminLTE/tree/v3.2.0`. Chỉ đóng gói các thư viện thực sự được layout dùng.
- Giấy phép: thư mục `ThirdPartyLicenses` và các file licence kèm thư viện.
- Layout: `https://learn.microsoft.com/aspnet/core/mvc/views/layout?view=aspnetcore-8.0`.
- Area: `https://learn.microsoft.com/aspnet/core/mvc/controllers/areas?view=aspnetcore-8.0`.
