using Microsoft.EntityFrameworkCore;
using NguyenXuanBac2410900011_exam.Models;

namespace NguyenXuanBac2410900011_exam.Data;

public class NxbDbContext(DbContextOptions<NxbDbContext> options) : DbContext(options)
{
    public DbSet<NxbStudent> NxbStudents => Set<NxbStudent>();
    public DbSet<NxbEmployee> NxbEmployees => Set<NxbEmployee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<NxbStudent>().ToTable("NxbStudent", table =>
            table.HasCheckConstraint("CK_NxbStudent_Gender", "[NxbGender] IN (N'Nam', N'Nữ', N'Khác')"));
        modelBuilder.Entity<NxbEmployee>().ToTable("NxbEmployee", table =>
            table.HasCheckConstraint("CK_NxbEmployee_Gender", "[NxbGender] IN (N'Nam', N'Nữ', N'Khác')"));
    }
}
