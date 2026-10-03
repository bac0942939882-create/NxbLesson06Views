using NxbLesson14.Models;
namespace NxbLesson14.Data;
public static class DbInitializer
{
    public static async Task InitializeAsync(NxbLesson14Context db)
    {
        var created = await db.Database.EnsureCreatedAsync();
        if (!created) return;
        var categories = new[]
        {
            new Category { Name = "Đồ uống", Description = "Cà phê, trà và nước trái cây", Image = "/images/demo/348x261.png" },
            new Category { Name = "Đồ ăn", Description = "Bánh và món ăn nhẹ", Image = "/images/demo/348x261.png" },
            new Category { Name = "Phụ kiện", Description = "Các sản phẩm tiện ích", Image = "/images/demo/348x261.png" }
        };
        db.Categories.AddRange(categories);
        await db.SaveChangesAsync();
        db.Products.AddRange(
            new Product { Name = "Cà phê sữa", Price = 30000, SalePrice = 25000, CategoryId = categories[0].Id, Image = "/images/demo/348x261.png", Description = "Cà phê sữa đậm vị." },
            new Product { Name = "Trà đào", Price = 35000, CategoryId = categories[0].Id, Image = "/images/demo/348x261.png" },
            new Product { Name = "Bánh ngọt", Price = 28000, CategoryId = categories[1].Id, Image = "/images/demo/348x261.png" },
            new Product { Name = "Bình giữ nhiệt", Price = 120000, CategoryId = categories[2].Id, Image = "/images/demo/348x261.png" });
        db.Banners.AddRange(
            new Banner { Name = "Banner trang chủ", Prioty = 0, Image = "/images/demo/backgrounds/01.png", Description = "Banner đầu trang." },
            new Banner { Name = "Ưu đãi cuối tuần", Prioty = 1, Image = "/images/demo/348x261.png" });
        db.Blogs.AddRange(
            new Blog { Name = "Chào mừng đến với NxbLesson14", Description = "Bài viết mẫu để kiểm tra các chức năng quản lý nội dung.", Image = "/images/demo/348x261.png" },
            new Blog { Name = "Khám phá sản phẩm mới", Description = "Thông tin về các sản phẩm mới trong danh mục.", Image = "/images/demo/348x261.png" });
        await db.SaveChangesAsync();
    }
}
