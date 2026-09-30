using Microsoft.EntityFrameworkCore;
using Nxb.Models;

namespace Nxb.Data;

public class StudentDbContext(DbContextOptions<StudentDbContext> options) : DbContext(options)
{
    public DbSet<StdClass> StdClasses => Set<StdClass>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Subjects> Subjects => Set<Subjects>();
    public DbSet<Marks> Marks => Set<Marks>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasIndex(s => s.StudentEmail).IsUnique();
        modelBuilder.Entity<Student>().HasIndex(s => s.StudentPhone).IsUnique();
        modelBuilder.Entity<Subjects>().HasIndex(s => s.SubjectName).IsUnique();
        modelBuilder.Entity<Student>().HasOne(s => s.Class).WithMany(c => c.Students)
            .HasForeignKey(s => s.ClassId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Marks>().HasKey(m => new { m.SubjectId, m.StudentId });
        modelBuilder.Entity<Marks>().HasOne(m => m.Student).WithMany(s => s.Marks)
            .HasForeignKey(m => m.StudentId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Marks>().HasOne(m => m.Subject).WithMany(s => s.Marks)
            .HasForeignKey(m => m.SubjectId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Marks>().ToTable(t => t.HasCheckConstraint("CK_Marks_Score", "[Score] >= 0 AND [Score] <= 10"));
        modelBuilder.Entity<StdClass>().HasData(
            new StdClass { Id = 1, ClassName = "K24CNT2" },
            new StdClass { Id = 2, ClassName = "K24CNT3" });
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, StudentName = "Nguyễn An", StudentEmail = "an@example.com", StudentPhone = "0900000001", StudentAddress = "Hà Nội", StudentAvatar = "demo.png", StudentBirthday = new DateTime(2006, 3, 10), ClassId = 1 },
            new Student { Id = 2, StudentName = "Trần Bình", StudentEmail = "binh@example.com", StudentPhone = "0900000002", StudentAddress = "Hải Phòng", StudentAvatar = "demo.png", StudentBirthday = new DateTime(2006, 5, 20), ClassId = 1 },
            new Student { Id = 3, StudentName = "Lê Chi", StudentEmail = "chi@example.com", StudentPhone = "0900000003", StudentAddress = "Đà Nẵng", StudentAvatar = "demo.png", StudentBirthday = new DateTime(2006, 8, 15), ClassId = 2 },
            new Student { Id = 4, StudentName = "Phạm Dũng", StudentEmail = "dung@example.com", StudentPhone = "0900000004", StudentAddress = "Hà Nội", StudentAvatar = "demo.png", StudentBirthday = new DateTime(2006, 11, 2), ClassId = 2 });
        modelBuilder.Entity<Subjects>().HasData(
            new Subjects { Id = 1, SubjectName = "Lập trình ASP.NET Core" },
            new Subjects { Id = 2, SubjectName = "Cơ sở dữ liệu" },
            new Subjects { Id = 3, SubjectName = "Tiếng Anh" });
        modelBuilder.Entity<Marks>().HasData(
            new Marks { SubjectId = 1, StudentId = 1, Score = 8.5 },
            new Marks { SubjectId = 2, StudentId = 1, Score = 9 },
            new Marks { SubjectId = 1, StudentId = 2, Score = 7.5 },
            new Marks { SubjectId = 3, StudentId = 2, Score = 8 },
            new Marks { SubjectId = 2, StudentId = 3, Score = 8 },
            new Marks { SubjectId = 3, StudentId = 4, Score = 7 });
    }
}
