# NxbLesson7 - ASP.NET Core MVC .NET 8 LTS

Dự án mô phỏng nội dung bài Lesson 07: Models, đưa dữ liệu Model -> View, ViewBag/ViewData, Strong Typing, Binding Data và tạo View theo kiểu Scaffolding trong Visual Studio.

## Quy ước đã thay tên
- `Tvc` -> `Nxb`
- `Trịnh Văn Chung` -> `Nguyễn Xuân Bắc`
- Tên project: `NxbLesson7`

## Chạy bằng Visual Studio
1. Mở `NxbLesson7.sln` bằng Visual Studio 2022.
2. Chọn SDK .NET 8.0 (LTS).
3. Đặt `NxbLesson7` làm Startup Project.
4. Nhấn `F5` hoặc `Ctrl + F5`.
5. Các URL chính:
   - `/Home/Index`
   - `/NxbMember/GetMember` - object + ViewBag
   - `/NxbMember/Members` - List + ViewBag
   - `/NxbMember/Detail` - Strongly Typed View
   - `/NxbMember/Index` - danh sách
   - `/NxbMember/Create` - Binding Data + Validation + POST

## Ghi chú
Project dùng mock data trong bộ nhớ để bám sát cách minh họa trong video, không thêm Entity Framework/SQL ngoài nội dung bài.
