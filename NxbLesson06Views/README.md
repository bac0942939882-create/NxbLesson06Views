# NxbLesson06Views - .NET 8 LTS

Project được dựng lại theo nội dung quan sát được trong video bài học về **Views / Partial View / View Component**.

## Thay đổi theo yêu cầu

Toàn bộ tên `Tvc`/`TvcLesson06Views` trong project đã được đổi thành `Nxb`/`NxbLesson06Views`.

Ví dụ:
- `TvcHomeController` -> `NxbHomeController`
- `TvcLesson06Views` -> `NxbLesson06Views`
- `_TvcHeaderPartialView` -> `_NxbHeaderPartialView`
- `_TvcFooter` -> `_NxbFooter`
- namespace `TvcLesson06Views.*` -> `NxbLesson06Views.*`

## Nội dung chính

- ASP.NET Core MVC trên .NET 8 LTS.
- `Views/NxbHome/Index.cshtml` nhận dữ liệu sinh viên.
- Partial View `_NxbHeaderPartialView.cshtml`.
- Partial View `_NxbFooter.cshtml`.
- `CategoryViewComponent` trả về danh sách Category.
- View của View Component tại:
  `Views/Shared/Components/Category/Default.cshtml`
- Có tham số `n` trong View Component để lọc Category theo `CategoryId`.

## Chạy project

Cần cài **.NET 8 SDK**.

```bash
dotnet restore
dotnet run
```

Sau đó mở địa chỉ do ASP.NET Core in ra, hoặc:

- http://localhost:5167
- https://localhost:7167

## Lưu ý

Video sử dụng giao diện Bootstrap; project này dùng Bootstrap 5.3.8 qua CDN để không phải đóng gói thư viện frontend vào file ZIP.
