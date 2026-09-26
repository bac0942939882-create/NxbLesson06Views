using Microsoft.EntityFrameworkCore;
using NguyenXuanBac2410900011_exam.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NxbDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NxbConnection")
        ?? throw new InvalidOperationException("Thiếu chuỗi kết nối NxbConnection.")));

var app = builder.Build();

if (app.Environment.IsDevelopment() && builder.Configuration.GetValue<bool>("Database:InitializeOnStartup"))
{
    await using var scope = app.Services.CreateAsyncScope();
    await NxbDbInitializer.InitializeAsync(scope.ServiceProvider.GetRequiredService<NxbDbContext>());
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
