# NxbLesson10EFDbFirst

Bài Lesson 10: Entity Framework Core, phương pháp Database First, ASP.NET Core MVC, .NET 8.0. Dựa theo video: tạo bảng thành viên trong SQL Server, ánh xạ thành `NxbMember` và `NxbK24Cnt2Lesson10EfDbContext`, tạo controller cùng các trang Index, Details, Create, Edit, Delete.

## Chạy trong Visual Studio

1. Giải nén và mở `NxbLesson10EFDbFirst.sln` trong Visual Studio có workload **ASP.NET and web development** và .NET 8 SDK.
2. Mở SQL Server Management Studio, kết nối SQL Server, chạy toàn bộ file `NxbLesson10EFDbFirst/Database/NxbK24CNT2Lesson10EFDb.sql`. Script tạo database và một thành viên mẫu: Nguyễn Xuân Bắc, `bac0942939882@gmail.com`, `0336924130`. Chạy lại script sẽ không thêm trùng tài khoản `nxb`.
3. Mở `NxbLesson10EFDbFirst/appsettings.json` và sửa `Server=.\\SQLEXPRESS` thành tên **server/instance của máy bạn**. Ví dụ nếu SSMS kết nối tới `localhost` thì đặt `Server=localhost`; nếu dùng LocalDB thì đặt `Server=(localdb)\\MSSQLLocalDB`. Tên database giữ nguyên `NxbK24CNT2Lesson10EFDb`.
4. Chờ Visual Studio tải NuGet, nhấn **F5**. Vào menu **Danh sách thành viên** hoặc đường dẫn `/NxbMembers`. Nếu trình duyệt báo chứng chỉ phát triển, chạy `dotnet dev-certs https --trust` trên máy của bạn hoặc dùng profile `http`.

Các gói EF Core trong project là phiên bản 8.0.31, tương ứng .NET 8 trong video. Code đã có sẵn model và DbContext nên không cần chạy lại `Scaffold-DbContext` để mở project. Nếu muốn tự thực hành lệnh Database First trong Package Manager Console sau khi tạo SQL, sử dụng:

```powershell
Scaffold-DbContext "Name=ConnectionStrings:NxbK24CNT2Connection" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context NxbK24Cnt2Lesson10EfDbContext -Tables NxbMember -Force
```

Lưu ý: lệnh `-Force` có thể ghi đè model/DbContext. Bảng trong video có cột mật khẩu dạng văn bản; project này chỉ phục vụ bài thực hành, không dùng bảng đó làm hệ thống đăng nhập. Bản ghi mẫu để trống mật khẩu; trang danh sách và chi tiết không hiển thị mật khẩu. Dùng một hệ thống xác thực có băm mật khẩu khi làm ứng dụng thật.

Không cần đẩy lên GitHub để chạy bài: file `.sln`, source code và script SQL đều có trong ZIP. Không có thư mục `bin`, `obj`, `.vs` hay file khóa dự án.
