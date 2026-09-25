using Microsoft.EntityFrameworkCore;

namespace NxbLesson10EFDbFirst.Models;

public partial class NxbK24Cnt2Lesson10EfDbContext : DbContext
{
    public NxbK24Cnt2Lesson10EfDbContext(DbContextOptions<NxbK24Cnt2Lesson10EfDbContext> options)
        : base(options) { }

    public virtual DbSet<NxbMember> NxbMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NxbMember>(entity =>
        {
            entity.ToTable("NxbMember");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.NxbUserName).HasMaxLength(20).IsUnicode(false);
            entity.Property(e => e.NxbPassword).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.NxbFullName).HasMaxLength(50);
            entity.Property(e => e.NxbEmail).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.NxbPhone).HasMaxLength(12).IsUnicode(false).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
