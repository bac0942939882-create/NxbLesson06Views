using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NxbLesson14.Data;
namespace NxbLesson14.Controllers;
public class ProductController(NxbLesson14Context db) : Controller
{
    public async Task<IActionResult> Index() => View(await db.Products.AsNoTracking().Include(x => x.Category)
        .Where(x => x.Status == 1 && x.Category!.Status == 1).OrderBy(x => x.Name).ToListAsync());
    public async Task<IActionResult> Details(int id)
    {
        var model = await db.Products.AsNoTracking().Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id && x.Status == 1 && x.Category!.Status == 1);
        if (model == null) return NotFound();
        return View(model);
    }
}
