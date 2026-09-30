using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nxb.Data;
using Nxb.ViewModels;

namespace Nxb.Controllers;

public class HomeController(AppDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            Banners = await db.Banners.AsNoTracking().Where(x => x.Status == 1).OrderBy(x => x.Id).ToListAsync(),
            Products = await db.Products.AsNoTracking().Include(x => x.Category)
                .Where(x => x.Status == 1 && x.Category.Status == 1).OrderBy(x => x.Id).Take(4).ToListAsync()
        };
        return View(model);
    }
    // Bài tự làm 2: dữ liệu lấy từ database, hiển thị thành các cột.
    public async Task<IActionResult> Product() => View(await db.Products.AsNoTracking()
        .Include(x => x.Category).Where(x => x.Status == 1 && x.Category.Status == 1).OrderBy(x => x.Id).ToListAsync());
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View();
}
