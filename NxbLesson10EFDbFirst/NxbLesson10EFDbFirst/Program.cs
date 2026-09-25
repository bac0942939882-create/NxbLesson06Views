using Microsoft.EntityFrameworkCore;
using NxbLesson10EFDbFirst.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("NxbK24CNT2Connection")
    ?? throw new InvalidOperationException("Thiếu ConnectionStrings:NxbK24CNT2Connection trong appsettings.json.");
builder.Services.AddDbContext<NxbK24Cnt2Lesson10EfDbContext>(options => options.UseSqlServer(connectionString));

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
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
