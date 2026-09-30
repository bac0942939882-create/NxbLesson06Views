# Nxb - LabGuide06: Entity Framework Core

Project ASP.NET Core Web App (Model-View-Controller), .NET 8, Entity Framework Core 8 và SQL Server. Solution, project và namespace đều có tên **Nxb**. Mã nguồn có thể sửa bình thường và có thể đưa lên GitHub.

## Mở nhanh bằng Visual Studio

1. Giải nén `Nxb.zip` vào một thư mục bình thường, ví dụ `D:\BaiTap\Nxb`. Không mở solution ngay trong file ZIP.
2. Dùng Visual Studio 2022 có workload **ASP.NET and web development** và .NET 8 SDK. Trong Visual Studio Installer, cài **SQL Server Express LocalDB** nếu chưa có.
3. Mở `Nxb.sln`. Chờ Visual Studio khôi phục NuGet. Nếu chưa tự khôi phục, bấm chuột phải solution -> **Restore NuGet Packages**.
4. Bấm **Ctrl + Shift + B** để build. Đặt project **Nxb** làm Startup Project nếu cần.
5. Chọn profile **Nxb**, bấm **Ctrl + F5**. Trang mở ở `http://localhost:5086`.
6. Với cấu hình LocalDB mặc định, lần chạy đầu sẽ tự tạo hai database **Nxb** và **StudentManager** bằng các migration có sẵn, đồng thời thêm dữ liệu mẫu. Những lần chạy sau giữ dữ liệu đã lưu.

## Dùng SQL Server/SQL Express thay cho LocalDB

Mở `Nxb/appsettings.json`, đổi phần `Server` của **cả hai** connection string thành đúng tên server đang dùng. Giữ tên database riêng biệt:

```json
"ConnectionStrings": {
  "AppConnection": "Server=.\\SQLEXPRESS;Database=Nxb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True",
  "StudentConnection": "Server=.\\SQLEXPRESS;Database=StudentManager;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
}
```

Nếu SSMS kết nối bằng tên khác, ví dụ `DESKTOP-ABC` hoặc `localhost`, dùng đúng tên đó. Dấu `\` trong file JSON phải viết thành `\\`. Nếu dùng SQL Server Authentication, thay `Trusted_Connection=True` bằng `User Id=...;Password=...`; điền tài khoản của máy bạn.

Tài khoản kết nối cần quyền tạo database trong lần chạy đầu. Nếu không có quyền đó, nhờ tài khoản có quyền chạy script SQL bên dưới trước, rồi chạy web bằng tài khoản có quyền đọc/ghi các bảng.

## Các file database kèm theo

| File | Nội dung |
| --- | --- |
| `Database/TaoTatCaDatabase.sql` | Tạo cả hai database, bảng, quan hệ, dữ liệu mẫu và lịch sử migration trong một lần chạy. |
| `Database/Nxb.sql` | Chỉ tạo database Nxb với Category, Product, Banner. |
| `Database/StudentManager.sql` | Chỉ tạo StudentManager với StdClass, Student, Subjects, Marks. |

Mở SSMS -> kết nối đúng SQL Server -> mở `TaoTatCaDatabase.sql` -> **Execute/F5**. Script tự chọn database phù hợp và không yêu cầu bật SQLCMD Mode. Script được sinh từ chính các migration và có kiểm tra lịch sử để không thêm dữ liệu mẫu lần hai khi chạy lại trên database do project này tạo.

Bạn có thể chọn **một trong hai cách**: chạy web để migration tự tạo database hoặc chạy script SQL trước. Cả hai cách tạo cùng cấu trúc và lịch sử migration. Không cần làm cả hai.

Không dùng script này để chuyển đổi một database cũ cùng tên nhưng có cấu trúc khác. Nếu máy đã có database `Nxb`/`StudentManager` của bài khác, đổi tên database trong connection string và dùng cách migration tự tạo một database mới.

## Đối chiếu với tất cả bài trong PDF

| Phần trong PDF | Kết quả đã làm | Nơi xem |
| --- | --- | --- |
| Thực hành bài 1, trang 1-4 | Project MVC, gói EF Core, EF SQL Server, Design và Tools cùng phiên bản 8.0.31. | `Nxb/Nxb.csproj` |
| Thực hành bài 2, trang 4-10 | Models Category/Product, thuộc tính Column, AppDbContext, connection string, Code First, migration, quan hệ 1-n. | `Models`, `Data`, `Migrations/App`, database Nxb |
| Thực hành bài 3, trang 10-20 | CRUD danh mục; giao diện tiếng Việt; ngày tạo gán trên server, không nhập trên form. | `/Categories` |
| Tự làm bài 1, trang 21-22 | CRUD sản phẩm; chọn danh mục; upload ảnh khi thêm/sửa; giữ ảnh nếu không chọn ảnh mới. | `/Products` |
| Tự làm bài 2, trang 22 | Action `Product` của HomeController lấy dữ liệu thật và hiển thị dạng lưới 4 cột trên màn hình lớn. | `/Home/Product` |
| Tự làm bài 3, trang 22 | CRUD Banner có Id, Name, Image, Description, CreatedDate, Status và upload ảnh. | `/Banners` |
| Tự làm bài 4, trang 22 | Banner trên trang chủ dạng carousel, nút trước/sau, dấu chọn slide. Chỉ hiển thị banner có Status=1. | `/` |
| Tự làm bài 5, trang 22-24 | StudentManager: đúng 4 bảng, kiểu dữ liệu, unique email/SĐT/tên môn, khóa chính ghép Marks, khóa ngoại. Có migration và script SQL. | `Data/StudentDbContext.cs`, `Migrations/Student`, database StudentManager |
| Tự làm bài 6, trang 24 | CRUD lớp, sinh viên, môn học, điểm; kiểm tra dữ liệu tại form và database. | `/StdClasses`, `/Students`, `/Subjects`, `/Marks` |

Tên project mẫu `NetCoreLAB6_EF` và tên database bán hàng mẫu `NetCoreCRUD` được đổi thành **Nxb** theo yêu cầu. Database **StudentManager** giữ đúng tên đề bài. Những ví dụ .NET 6/7 trong tài liệu được chuyển sang .NET 8.

## Dữ liệu mẫu và cách thử

- Nxb: 3 danh mục, 8 sản phẩm có ảnh minh họa, 3 banner (2 hiển thị, 1 tạm ẩn).
- StudentManager: 2 lớp, 4 sinh viên, 3 môn học, 6 bản ghi điểm. Thông tin sinh viên mẫu dùng email `example.com`.
- Menu **Quản lý bán hàng** chứa danh mục, sản phẩm, banner. Menu **Quản lý sinh viên** chứa lớp, sinh viên, môn học, điểm.
- Chọn **Thêm mới**, nhập các trường bắt buộc, chọn ảnh hợp lệ rồi lưu. Bản ghi mới xuất hiện trong danh sách và tồn tại sau khi khởi động lại web.
- Dùng **Chi tiết**, **Sửa**, **Xóa** ở mỗi dòng. Xóa có trang xác nhận trước khi ghi thay đổi.
- Ở form sửa sản phẩm/banner/sinh viên, không chọn ảnh mới thì giữ ảnh hiện tại. Thay ảnh thành công sẽ xóa ảnh upload cũ; xóa bản ghi sẽ xóa ảnh upload đi kèm. Các ảnh mẫu dùng chung được giữ lại.
- Khi sửa điểm, sinh viên và môn học được khóa; chỉ sửa điểm của cặp khóa đang chọn. Muốn chuyển điểm sang cặp khác, xóa điểm cũ rồi thêm bản ghi mới.

## Kiểm tra dữ liệu đã có

- Các trường chuỗi có giới hạn độ dài theo đề. Email, số điện thoại và ngày sinh được kiểm tra định dạng; ngày sinh không ở tương lai và từ năm 1900 trở đi.
- Email sinh viên, SĐT sinh viên và tên môn học có unique index. Không thêm hai điểm cho cùng một cặp sinh viên/môn học.
- Điểm từ 0 đến 10. Giá và giá giảm không âm; giá giảm không vượt giá gốc; 0 là không áp dụng giảm giá.
- Danh mục, lớp, sinh viên và môn học được kiểm tra tồn tại trước khi ghi khóa ngoại.
- Không xóa danh mục còn sản phẩm, lớp còn sinh viên, sinh viên/môn học còn điểm. Form hiển thị lý do để bạn xử lý các bản ghi liên quan trước.
- Ảnh JPG/JPEG, PNG, GIF, WebP tối đa 5 MB; kiểm tra chữ ký định dạng; đổi tên trên server để tránh trùng file và đường dẫn từ tên tải lên.
- Form POST có antiforgery token. Id/ngày tạo/ảnh đang giữ được lấy hoặc kiểm tra trên server.
- Ngày tạo được giữ nguyên khi sửa để phản ánh thời điểm thêm bản ghi.

## Lệnh Entity Framework trong Visual Studio

Tools -> NuGet Package Manager -> Package Manager Console, chọn Default project là **Nxb**.

Các migration ban đầu đã có, không cần Add-Migration lại để chạy:

```powershell
Update-Database -Context AppDbContext
Update-Database -Context StudentDbContext
```

Sau khi bạn thay đổi model và muốn tạo migration tiếp theo:

```powershell
Add-Migration CapNhatBanHang -Context AppDbContext -OutputDir Migrations/App
Update-Database -Context AppDbContext
Add-Migration CapNhatSinhVien -Context StudentDbContext -OutputDir Migrations/Student
Update-Database -Context StudentDbContext
```

Vì có hai DbContext, luôn ghi `-Context` để công cụ biết phải xử lý database nào.

## Minh họa DB-First (mục tiêu của tài liệu)

Các bài thực hành và tự làm trong PDF triển khai bằng Code First. Nếu muốn thử tạo model từ database đã được tạo ở trên, chạy lệnh sau trong Package Manager Console. Kết quả nằm ở thư mục riêng để không ghi đè model đang dùng:

```powershell
Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=Nxb;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DbFirstModels -ContextDir DbFirstModels -Context NxbDbFirstContext -NoOnConfiguring
```

Lệnh này là phần tham khảo để thực hành reverse engineering; web hiện tại dùng hai DbContext Code First đã có.

## Chạy bằng terminal nếu cần

Từ thư mục có `Nxb.sln`:

```powershell
dotnet restore Nxb.sln
dotnet build Nxb.sln
dotnet run --project Nxb/Nxb.csproj
```

Nếu thay đổi cấu trúc sau này, có thể dùng manifest công cụ EF có sẵn:

```powershell
dotnet tool restore
dotnet ef migrations add TenMigration --context AppDbContext --project Nxb
dotnet ef database update --context AppDbContext --project Nxb
```

## Lỗi thường gặp

| Hiện tượng | Cách xử lý |
| --- | --- |
| Không tìm thấy SDK/framework .NET 8 | Cài .NET 8 SDK và workload web trong Visual Studio Installer; khởi động lại Visual Studio. |
| Lỗi kết nối SQL Server/LocalDB | Kiểm tra server trong SSMS; sửa cả hai connection string. Với SQL Express, kiểm tra dịch vụ instance đang chạy. |
| LocalDB chưa cài | Cài SQL Server Express LocalDB hoặc đổi connection string sang SQL Server đang dùng. |
| Login failed | Kiểm tra Windows Authentication hoặc thông tin tài khoản SQL, cùng quyền truy cập hai database. |
| There is already an object named... | Database cùng tên đang có cấu trúc khác. Đổi tên database trong cấu hình để tạo database mới. |
| NuGet chưa tải được | Kiểm tra mạng và nguồn nuget.org, rồi Restore NuGet Packages. |
| Không xóa được dữ liệu | Đọc thông báo trên trang xác nhận và xử lý các bản ghi phụ thuộc trước. |

## Kết quả kiểm tra khi đóng gói

Xem `docs/KET_QUA_KIEM_TRA.md`. Ảnh giao diện trong `docs/anh-minh-hoa` giúp xem nhanh kết quả.

Tài liệu chính thức dùng để đối chiếu cách tổ chức migration:
https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/
https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying
