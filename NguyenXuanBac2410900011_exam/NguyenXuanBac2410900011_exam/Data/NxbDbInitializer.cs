using Microsoft.EntityFrameworkCore;
using NguyenXuanBac2410900011_exam.Models;

namespace NguyenXuanBac2410900011_exam.Data;

public static class NxbDbInitializer
{
    public static async Task InitializeAsync(NxbDbContext context)
    {
        // Chỉ tạo và thêm dữ liệu khi CSDL chưa có bảng; không ghi đè dữ liệu đã có.
        if (!await context.Database.EnsureCreatedAsync()) return;

        // Các hồ sơ sau đều là dữ liệu minh họa, không phải thông tin thật.
        context.NxbStudents.AddRange(
            new NxbStudent { NxbName = "Trần Minh Anh", NxbGender = "Nữ", NxbBirthDay = new(2005, 3, 15), NxbEmail = "minhanh@example.com", NxbPhone = "0900000001" },
            new NxbStudent { NxbName = "Lê Hoàng Nam", NxbGender = "Nam", NxbBirthDay = new(2004, 8, 22), NxbEmail = "hoangnam@example.com", NxbPhone = "0900000002" },
            new NxbStudent { NxbName = "Phạm Thu Hà", NxbGender = "Nữ", NxbBirthDay = new(2005, 11, 6), NxbEmail = "thuha@example.com", NxbPhone = "0900000003" },
            new NxbStudent { NxbName = "Đỗ Hải Đăng", NxbGender = "Nam", NxbBirthDay = new(2004, 1, 18), NxbEmail = "haidang@example.com", NxbPhone = "0900000004", NxbActive = false },
            new NxbStudent { NxbName = "Vũ Ngọc Linh", NxbGender = "Khác", NxbBirthDay = new(2005, 6, 9), NxbEmail = "ngoclinh@example.com", NxbPhone = "0900000005" },
            new NxbStudent { NxbName = "Bùi Quang Huy", NxbGender = "Nam", NxbBirthDay = new(2004, 12, 2), NxbEmail = "quanghuy@example.com", NxbPhone = "0900000006", NxbActive = false });
        context.NxbEmployees.Add(new NxbEmployee
        {
            NxbName = "Nhân viên minh họa", NxbGender = "Nam", NxbBirthDay = new(1995, 1, 1),
            NxbEmail = "employee@example.com", NxbPhone = "0900000099"
        });
        await context.SaveChangesAsync();
    }
}
