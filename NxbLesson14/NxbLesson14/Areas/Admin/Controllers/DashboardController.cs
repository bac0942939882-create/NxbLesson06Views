using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NxbLesson14.Data;
using NxbLesson14.Models;
namespace NxbLesson14.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController(NxbLesson14Context db) : Controller
{
    public async Task<IActionResult> Index() => View(new DashboardViewModel
    {
        CategoryCount = await db.Categories.CountAsync(),
        ProductCount = await db.Products.CountAsync(),
        BlogCount = await db.Blogs.CountAsync(),
        BannerCount = await db.Banners.CountAsync()
    });
}
