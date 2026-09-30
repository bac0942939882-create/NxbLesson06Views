# Nguyễn Xuân Bắc — 2410900011

Bài thực hành **ASP.NET Core Web App (Model-View-Controller)**, **.NET 8.0**, **Microsoft SQL Server**.

- Solution và project: `NguyenXuanBac2410900011_exam`.
- Tiền tố lớp, bảng, trường: `Nxb`.
- Trang giới thiệu: `/Home/NxbAbout`.
- Danh sách và CRUD: `/NxbStudents`.
- File nộp SQL: **Nxb Employee_2410900011_Db.sql**.

## Mở và chạy trong Visual Studio

1. Mở **NguyenXuanBac2410900011_exam.sln** ở ngay thư mục này. Không mở nhầm solution bài Lesson06 ở thư mục cha.
2. Máy cần Visual Studio có workload **ASP.NET and web development**, .NET 8 SDK và SQL Server Express. Dự án được tạo từ MVC Template, không phải Razor Pages hoặc Web API.
3. Đợi Visual Studio khôi phục các gói NuGet.
4. Kết nối mặc định trong `NguyenXuanBac2410900011_exam/appsettings.json`:

   ```json
   "NxbConnection": "Server=.\\SQLEXPRESS;Database=NxbEmployee_2410900011_Db;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
   ```

5. Trong SQL Server Management Studio, kết nối `.\SQLEXPRESS` bằng Windows Authentication, mở **Nxb Employee_2410900011_Db.sql** và bấm **Execute / F5**. Script tạo CSDL, hai bảng và dữ liệu mẫu; có thể chạy lại mà không xóa dữ liệu.
6. Trong Visual Studio, chọn profile **http**, nhấn **Ctrl+F5**. Địa chỉ mặc định: **http://localhost:5075**.
7. Chọn menu **Thông tin sinh viên** hoặc **Danh sách sinh viên**.

Khi chạy ở môi trường Development, ứng dụng cũng có thể tự tạo CSDL và dữ liệu mẫu nếu chưa có bảng, nhờ `Database:InitializeOnStartup=true`. Vì vậy bước chạy SQL có thể bỏ qua khi chỉ muốn chạy thử, nhưng file SQL vẫn là file cần nộp. Cách này dùng `EnsureCreated`, không yêu cầu chạy `Update-Database`.

Nếu dùng SQL Server instance khác, sửa phần `Server` cho đúng máy. Với LocalDB, dùng `Server=(localdb)\\MSSQLLocalDB` trong JSON (hai dấu gạch chéo trong file JSON). Không đưa mật khẩu SQL thật lên GitHub; dùng Windows Authentication như cấu hình mặc định.

## Đối chiếu yêu cầu đề

| Yêu cầu | Cách thực hiện |
|---|---|
| CSDL Employee | Bảng `dbo.NxbEmployee`, model `NxbEmployee` |
| Template MVC, .NET 8 | Project SDK Web, `TargetFramework=net8.0`, Controllers / Models / Views |
| Home / HvtAbout | Đổi tiền tố thành `HomeController.NxbAbout()` |
| Thông tin sinh viên | Nguyễn Xuân Bắc — 2410900011 |
| CRUD Student bằng scaffolding | `NxbStudentsController`, 5 view Index / Details / Create / Edit / Delete |
| Menu trong Layout | `Views/Shared/_Layout.cshtml` |
| Bootstrap và CSS | Bootstrap cục bộ + `wwwroot/css/site.css` |

**Lưu ý tên bảng:** đề ghi Employee ở mục 1 nhưng Student ở mục 4. Bài tạo **hai bảng độc lập cùng cấu trúc**: `NxbEmployee` đáp ứng phần SQL, `NxbStudent` phục vụ CRUD sinh viên. Không đổi tên một bảng để làm mất yêu cầu còn lại.

Các hồ sơ có sẵn là **dữ liệu minh họa**, không phải thông tin cá nhân thật. Trang giới thiệu chỉ ghi thông tin sinh viên đã được cung cấp.

## Cấu trúc bảng

Cả `NxbEmployee` và `NxbStudent` đều có các cột sau:

| Cột | Kiểu SQL Server | Ý nghĩa |
|---|---|---|
| Id | INT IDENTITY(1,1), PRIMARY KEY | Mã tự tăng |
| NxbName | NVARCHAR(100), NOT NULL | Họ tên, hỗ trợ tiếng Việt |
| NxbGender | NVARCHAR(10), NOT NULL | Nam / Nữ / Khác, có CHECK constraint |
| NxbBirthDay | DATE, NOT NULL | Ngày sinh |
| NxbEmail | NVARCHAR(254), NOT NULL | Email |
| NxbPhone | NVARCHAR(20), NOT NULL | Giữ được số 0 đầu điện thoại |
| NxbActive | BIT, NOT NULL | 1: hoạt động; 0: ngừng hoạt động |

Biểu mẫu kiểm tra trường bắt buộc, họ tên 2–100 ký tự, email, số điện thoại 10 chữ số bắt đầu bằng 0 và ngày sinh không nằm trong tương lai. Kiểm tra dữ liệu được thực hiện tại máy chủ; POST có chống giả mạo yêu cầu (anti-forgery). Xóa cần qua trang xác nhận.

## Scaffolding đã sử dụng

Bộ controller và 5 view CRUD được sinh bằng công cụ scaffolding chính thức, sau đó chỉnh giao diện tiếng Việt và bổ sung tìm kiếm, lọc trạng thái, phân trang, thông báo kết quả.

Lệnh ban đầu (chạy trong thư mục chứa file .csproj):

```powershell
dotnet tool restore
dotnet aspnet-codegenerator controller -name NxbStudentsController -m NxbStudent -dc NxbDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
```

Trong Visual Studio, thao tác tương đương:

1. Nhấp phải **Controllers → Add → New Scaffolded Item**.
2. Chọn **MVC Controller with views, using Entity Framework**.
3. Model class: **NxbStudent**.
4. Data context: **NxbDbContext**.
5. Controller name: **NxbStudentsController**.

Bài đã có kết quả scaffolding, không cần chạy lại. Nếu sinh lại và ghi đè, giao diện tùy chỉnh có thể mất.

Các gói: EF Core SQL Server / Design / Tools **8.0.31**, Web CodeGeneration Design **8.0.23**. `global.json` giữ SDK ở dòng 8.0.4xx.

Tham khảo: [Microsoft — tạo model và scaffolding MVC](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-8.0&tabs=visual-studio).

## Đưa bài lên GitHub

Bản làm việc này nằm trong repository **NxbLesson06Views**, nhánh **master**, theo dõi **origin/master**:

[https://github.com/bac0942939882-create/NxbLesson06Views](https://github.com/bac0942939882-create/NxbLesson06Views)

Mở Terminal tại thư mục **NxbLesson06Views** (thư mục cha của bài thi), rồi chạy:

```powershell
git add .
git commit -m "Them bai thi ASP.NET Core MVC .NET 8 - Nguyen Xuan Bac 2410900011"
git push
```

Các bài cũ trong repository được giữ nguyên. Bài thi mới nằm trong thư mục `NguyenXuanBac2410900011_exam`. Bài chưa được commit hoặc push tự động, để bạn tự thực hiện ba lệnh trên. `.gitignore` loại bỏ `bin/`, `obj/`, `.vs/` và file cá nhân của Visual Studio.

Nếu Git yêu cầu đăng nhập khi push, đăng nhập tài khoản GitHub có quyền ghi repository này. Nếu Git báo remote có cập nhật mới, sau khi commit chạy `git pull --rebase` rồi `git push`; không dùng force push.

File ZIP đi kèm chỉ chứa bài thi, không có thư mục `.git`. Nếu dùng ZIP ở máy khác, hãy clone repository trước rồi chép thư mục bài thi vào repository đó; không chạy `git init` bên trong thư mục bài thi.

## Kiểm tra bài

- Chạy file SQL: kết quả ban đầu là 1 nhân viên và 6 sinh viên.
- Mở trang giới thiệu: đúng tên và mã sinh viên.
- Thêm một sinh viên, xem chi tiết, sửa thông tin, đổi trạng thái.
- Thử để trống họ tên, email sai, điện thoại sai, ngày sinh tương lai: không lưu dữ liệu sai.
- Tìm kiếm / lọc trạng thái, thử từ khóa không có kết quả.
- Mở trang xóa và chọn Hủy; sau đó mở lại và xác nhận xóa hồ sơ thử.
- Thu nhỏ cửa sổ để kiểm tra giao diện.
