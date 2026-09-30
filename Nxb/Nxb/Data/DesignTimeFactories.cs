using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Nxb.Data;

// EF dùng factory khi Add-Migration/Update-Database, không chạy phần khởi động web.
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
        return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(config.GetConnectionString("AppConnection")).Options);
    }
}

public class StudentDbContextFactory : IDesignTimeDbContextFactory<StudentDbContext>
{
    public StudentDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
        return new StudentDbContext(new DbContextOptionsBuilder<StudentDbContext>()
            .UseSqlServer(config.GetConnectionString("StudentConnection")).Options);
    }
}
