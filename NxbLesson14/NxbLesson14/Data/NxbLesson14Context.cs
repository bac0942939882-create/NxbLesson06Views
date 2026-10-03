using Microsoft.EntityFrameworkCore;
using NxbLesson14.Models;
namespace NxbLesson14.Data;

public class NxbLesson14Context(DbContextOptions<NxbLesson14Context> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Banner> Banners => Set<Banner>();
    public DbSet<Blog> Blogs => Set<Blog>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var sqlServer = Database.IsSqlServer();
        modelBuilder.Entity<Category>(e =>
        {
            e.ToTable("Category");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.HasIndex(x => x.Name).IsUnique();
            e.Property(x => x.Status).HasDefaultValue((byte)1).HasSentinel((byte)255);
            e.Property(x => x.Image).HasMaxLength(100).IsUnicode(false);
            e.Property(x => x.Description).HasMaxLength(350);
            e.Property(x => x.CreatedDate).HasColumnType("date")
                .HasDefaultValueSql(sqlServer ? "CONVERT(date, GETDATE())" : "date('now')");
        });
        modelBuilder.Entity<Product>(e =>
        {
            e.ToTable("Product");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.HasIndex(x => x.Name).IsUnique();
            e.Property(x => x.Status).HasDefaultValue((byte)1).HasSentinel((byte)255);
            e.Property(x => x.Image).HasMaxLength(100).IsUnicode(false);
            e.Property(x => x.Description).HasMaxLength(350);
            e.Property(x => x.CreatedDate).HasColumnType("date")
                .HasDefaultValueSql(sqlServer ? "CONVERT(date, GETDATE())" : "date('now')");
            e.Property(x => x.Price).HasColumnType("float");
            e.Property(x => x.SalePrice).HasColumnName("salePrice").HasColumnType("float").HasDefaultValue(0.0);
            e.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Banner>(e =>
        {
            e.ToTable("Banner");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.HasIndex(x => x.Name).IsUnique();
            e.Property(x => x.Status).HasDefaultValue((byte)1).HasSentinel((byte)255);
            e.Property(x => x.Image).HasMaxLength(100).IsUnicode(false);
            e.Property(x => x.Description).HasMaxLength(350);
            e.Property(x => x.Prioty).HasDefaultValue(0);
        });
        modelBuilder.Entity<Blog>(e =>
        {
            e.ToTable("Blog");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.HasIndex(x => x.Name).IsUnique();
            e.Property(x => x.Status).HasDefaultValue((byte)1).HasSentinel((byte)255);
            e.Property(x => x.Image).HasMaxLength(100).IsUnicode(false);
            e.Property(x => x.Description).HasMaxLength(350);
            e.Property(x => x.CreatedDate).HasColumnType("date")
                .HasDefaultValueSql(sqlServer ? "CONVERT(date, GETDATE())" : "date('now')");
        });
    }
}
