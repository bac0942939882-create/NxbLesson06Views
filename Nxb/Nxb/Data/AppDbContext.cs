using Microsoft.EntityFrameworkCore;
using Nxb.Models;

namespace Nxb.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Banner> Banners => Set<Banner>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Product>().ToTable(t =>
        {
            t.HasCheckConstraint("CK_Product_Price", "[Price] >= 0 AND [SalePrice] >= 0 AND [SalePrice] <= [Price]");
            t.HasCheckConstraint("CK_Product_Status", "[Status] IN (0,1)");
        });
        modelBuilder.Entity<Category>().ToTable(t => t.HasCheckConstraint("CK_Category_Status", "[Status] IN (0,1)"));
        modelBuilder.Entity<Banner>().ToTable(t => t.HasCheckConstraint("CK_Banner_Status", "[Status] IN (0,1)"));
        var created = new DateTime(2026, 9, 30, 8, 0, 0);
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Túi xách", Status = 1, CreatedDate = created },
            new Category { Id = 2, Name = "Balo", Status = 1, CreatedDate = created },
            new Category { Id = 3, Name = "Phụ kiện", Status = 1, CreatedDate = created });
        var names = new[] { "Túi xách đen", "Túi xách đỏ", "Túi xách xanh", "Túi xách nâu", "Balo đen", "Balo xanh", "Ví nhỏ", "Túi mini" };
        modelBuilder.Entity<Product>().HasData(names.Select((name, index) => new Product
        {
            Id = index + 1, Name = name, Image = $"demo-{index + 1}.png",
            Price = 500000 + index * 50000, SalePrice = 450000 + index * 50000,
            Status = 1, Descriptions = "Sản phẩm mẫu dùng để thực hành thêm, xem, sửa và xóa.",
            CategoryId = index < 4 ? 1 : index < 6 ? 2 : 3, CreatedDate = created
        }));
        modelBuilder.Entity<Banner>().HasData(
            new Banner { Id = 1, Name = "Bộ sưu tập mới", Image = "demo-1.png", Description = "Khám phá sản phẩm mới tại Nxb", Status = 1, CreatedDate = created },
            new Banner { Id = 2, Name = "Ưu đãi hôm nay", Image = "demo-2.png", Description = "Các mẫu túi và balo dành cho bạn", Status = 1, CreatedDate = created },
            new Banner { Id = 3, Name = "Banner đang ẩn", Image = "demo-1.png", Description = "Bật trạng thái để hiển thị trên trang chủ", Status = 0, CreatedDate = created });
    }
}
