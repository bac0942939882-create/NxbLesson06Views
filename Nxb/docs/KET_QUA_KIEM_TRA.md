# Kết quả kiểm tra Nxb

Ngày: 30/09/2026.

## Build và migration

- Build Release với .NET SDK 8.0.425: thành công, 0 lỗi, 0 cảnh báo.
- Migration `InitialNxb` và `InitialStudentManager`: được sinh bằng EF Core Tools 8.0.31 từ model thực tế.
- Script SQL Server idempotent: sinh thành công cho cả hai context. Script bao gồm tạo bảng, khóa ngoại, unique index, check constraint, dữ liệu mẫu và lịch sử migration.
- Kiểm tra StudentManager có bảng StdClass, Student, Subjects, Marks; Marks dùng khóa chính ghép (SubjectId, StudentId). Các unique index email, SĐT, tên môn học đều có trong script.

## Kiểm tra tích hợp chức năng

132 điều kiện kiểm tra tự động đã qua, dùng HTTP test host của ASP.NET Core và SQLite trong hai database tạm. Cấu hình SQL Server của project giao cho người dùng được giữ nguyên.

Các tình huống chính:

- 35 trang GET: Index/Create/Edit/Details/Delete cho 7 nhóm dữ liệu.
- Thêm, sửa, xóa thành công cho danh mục, sản phẩm, banner, lớp, sinh viên, môn học và điểm.
- Trang chủ có đúng 2 banner đang hoạt động; banner bị ẩn không hiển thị. Trang sản phẩm có 8 thẻ sản phẩm mẫu.
- Chặn form POST thiếu antiforgery token; Id không tồn tại trả 404.
- Chặn tên bắt buộc bị trống, email/SĐT sinh viên trùng, tên môn trùng, cặp điểm trùng, điểm vượt 10, điểm trống, khóa ngoại không tồn tại, ngày sinh tương lai, giá trống và giá giảm lớn hơn giá gốc.
- Chặn xóa danh mục còn sản phẩm, lớp còn sinh viên, sinh viên/môn học còn điểm.
- Chặn ảnh giả định dạng, SVG, ảnh lớn hơn 5 MB.
- Upload ảnh có tên chứa đường dẫn được đổi thành tên an toàn trên server.
- Sửa không chọn ảnh giữ nguyên ảnh đang lưu. Thay ảnh xóa ảnh upload cũ; xóa bản ghi xóa ảnh upload liên quan.
- Ngày tạo không bị thay đổi bởi dữ liệu POST khi sửa.
- Sửa và xóa điểm tìm đúng cặp khóa SubjectId/StudentId.
- Không còn ảnh upload của bộ kiểm tra trong project đóng gói.

## Kiểm tra giao diện

- 7 trang được render bằng Chrome Headless: trang chủ, lưới sản phẩm, danh sách sản phẩm, danh sách sinh viên, form sản phẩm, form sinh viên và form điểm.
- Kiểm tra thêm trang chủ trên chiều rộng điện thoại 390 px.
- Không có lỗi JavaScript, không tràn chiều ngang của trang. CSS, JS và ảnh mẫu được nạp từ các tài nguyên có sẵn trong project.
- Đã xem ảnh chụp và điều chỉnh màu trạng thái cho tương thích với Bootstrap đi kèm.
- Ảnh minh họa được lưu trong thư mục `anh-minh-hoa`.

## Phạm vi xác nhận

Môi trường kiểm tra không cài SQL Server hoặc Windows LocalDB, vì vậy chưa chạy trực tiếp các script trên SQL Server. Chức năng web được kiểm tra bằng SQLite; cấu trúc SQL Server được đối chiếu từ model, migration và script đã sinh. Cần thực hiện bước chạy lần đầu trên SQL Server/LocalDB của máy người dùng theo README.

Các database tạm, công cụ kiểm tra và file build không nằm trong gói nộp. Gói nộp chứa mã nguồn, solution, migration, ảnh mẫu và script SQL.
