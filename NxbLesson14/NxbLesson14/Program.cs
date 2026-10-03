using Microsoft.EntityFrameworkCore;
using NxbLesson14.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NxbLesson14Context>(options =>
{
    if (builder.Configuration["DatabaseProvider"] == "Sqlite")
        options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
    else
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Route của Area phải đứng trước route mặc định.
app.MapControllerRoute("areas", "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

// Tạo CSDL và dữ liệu mẫu một lần, ở lần chạy đầu tiên.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NxbLesson14Context>();
    await DbInitializer.InitializeAsync(db);
}
app.Run();
