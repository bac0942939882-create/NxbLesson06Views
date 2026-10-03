# Kết quả kiểm tra NxbLesson14

- Build Release bằng .NET SDK 8.0.413: thành công, không có lỗi hoặc cảnh báo biên dịch.
- Thực hiện 186 kiểm tra HTTP, tài nguyên giao diện, CRUD và validation: tất cả đạt.
- Kiểm tra chạy ứng dụng dùng Entity Framework Core với SQLite; SQL Server LocalDB được cấu hình sẵn cho Visual Studio trên Windows.
- Script SQL Server được sinh trực tiếp từ model EF Core; đã kiểm tra kiểu dữ liệu, unique index và khóa ngoại trong script. Chưa chạy trực tiếp với SQL Server vì môi trường kiểm tra không có SQL Server.
- Đã kiểm tra tất cả đường dẫn ảnh, CSS/JS được các trang chính tải và các tài nguyên font được CSS tham chiếu.
- Chưa thực hiện kiểm tra hình ảnh và tương tác JavaScript trực tiếp trong trình duyệt do trình duyệt kiểm tra không khởi chạy được trong môi trường này.

## Nhóm thao tác đã đạt

| Nhóm | Kết quả |
| --- | --- |
| Trang chủ Bistup, trang thông tin, layout demo | HTTP 200; TagHelpers render thành URL đúng |
| AdminLTE Dashboard và menu NavLeft | Layout và ViewComponent render đúng |
| Danh sách, Create, Edit, Details, Delete của 4 thực thể | Chạy và lưu dữ liệu thành công |
| Tên trùng tiếng Việt, khác hoa/thường và khoảng trắng | Bị từ chối ở cả 4 thực thể |
| Tên trống, tên quá 100, mô tả quá 350, Status ngoài 0/1 | Form hiện lỗi và không lưu |
| Giá âm, giá khuyến mại vượt giá bán, CategoryId không tồn tại | Bị từ chối |
| Status = 0 | Được lưu đúng; Product/Blog ẩn không truy cập ở trang người dùng |
| Xóa Category đang có Product | Bị chặn, danh mục được giữ lại |
| GET trang xác nhận xóa | Không làm thay đổi dữ liệu |
| POST không có anti-forgery token | HTTP 400 |
| ID không tồn tại sau khi xóa | HTTP 404 |
| Tài nguyên CSS / JavaScript / ảnh / font | File và đường dẫn đầy đủ |

Dữ liệu, công cụ và script kiểm tra tạm thời không được đưa vào ZIP. Các file hướng dẫn và mã nguồn đều dùng tên NxbLesson14.
