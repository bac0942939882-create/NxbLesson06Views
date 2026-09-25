using System.Globalization;
using Microsoft.AspNetCore.Localization;
using NxbLesson09Annotation.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ProductStore>();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var vi = CultureInfo.GetCultureInfo("vi-VN");
    options.DefaultRequestCulture = new RequestCulture(vi);
    options.SupportedCultures = [vi];
    options.SupportedUICultures = [vi];
});
var app = builder.Build();
app.UseRequestLocalization();
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
