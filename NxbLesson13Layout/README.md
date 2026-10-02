# NxbLesson13Layout

Project ASP.NET Core Web App (Model-View-Controller), .NET 8, dựng theo video Lesson 13 – Tìm hiểu về Layout.

## Mở bằng Visual Studio

1. Giải nén toàn bộ file ZIP vào một thư mục trên máy.
2. Mở `NxbLesson13Layout.sln` bằng Visual Studio 2022 có workload **ASP.NET and web development** và .NET 8 SDK.
3. Nếu cần, nhấn chuột phải project **NxbLesson13Layout** → **Set as Startup Project**.
4. Chọn profile **http**, nhấn **Ctrl + F5**. Trình duyệt mở `/Products/Index`.

Không cần SQL Server hay cấu hình database cho bài này. Bootstrap, jQuery và các tệp CSS/JS đã nằm trong `wwwroot/lib`, không phụ thuộc CDN khi chạy.

## Nội dung theo video

- `_Layout.cshtml`, `_ViewStart.cshtml` và `_ViewImports.cshtml` của MVC.
- Layout riêng `Views/Shared/_NxbLayoutHome.cshtml` sử dụng `RenderBody`.
- `ProductsController`: `Index`, `Search(string? keyword)`, `Hots`.
- Các view sản phẩm chỉ định layout riêng và hiển thị tiêu đề như bài minh họa.
- Menu, phần nội dung, footer, Bootstrap và `NxbLayoutHome.css`.
- `NxbLayoutHome.js`: hàm `message()`, ví dụ `alert` được chú thích và `console.log` như cuối video. Mở F12 → Console để xem.
- Tiền tố trong project, namespace, layout và tên CSS/JS đã đổi thành `Nxb`.

Đã bổ sung action/view `About` cho liên kết Giới thiệu có sẵn trên menu để tránh lỗi 404. CSS cho phép menu xuống dòng trên màn hình nhỏ. Các tên ghi công giảng viên được giữ theo video.

Đây là bài minh họa Layout. Video chưa xây dựng dữ liệu sản phẩm, tìm kiếm thực tế hay CRUD; các trang vẫn giữ nội dung đơn giản theo mẫu.

## Các địa chỉ kiểm tra

| Đường dẫn | Nội dung |
| --- | --- |
| `/Products/Index` | Danh sách sản phẩm, layout riêng |
| `/Products/Search?keyword=GPU` | Trang tìm kiếm; controller nhận keyword vào ViewData |
| `/Products/Hots` | Trang Hots, layout riêng |
| `/Products/About` | Trang Giới thiệu, layout riêng |
| `/Home/Index` | Trang Welcome, layout mặc định |
| `/Home/Privacy` | Trang Privacy, layout mặc định |

## Chạy bằng dòng lệnh

Tại thư mục chứa solution:

```bash
dotnet restore
dotnet build
dotnet run --project NxbLesson13Layout --launch-profile http
```

Sau đó mở `http://localhost:5130/Products/Index`.

## Phạm vi tài liệu

Đã dựng phần thực hành trong video được gửi. Link slide Google Drive trả lỗi 403 (Forbidden), nên không thực hiện phần bổ sung ngoài video, theo yêu cầu của bạn.

Có thể chỉnh sửa toàn bộ source và đưa lên repository GitHub của bạn. File ZIP không chứa repository `.git` của người khác, thư mục `.vs`, `bin` hay `obj`.
