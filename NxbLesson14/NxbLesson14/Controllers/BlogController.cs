using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NxbLesson14.Data;
namespace NxbLesson14.Controllers;
public class BlogController(NxbLesson14Context db) : Controller
{
    public async Task<IActionResult> Index() => View(await db.Blogs.AsNoTracking().Where(x => x.Status == 1)
        .OrderByDescending(x => x.CreatedDate).ToListAsync());
    public async Task<IActionResult> Details(int id)
    {
        var model = await db.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Status == 1);
        if (model == null) return NotFound();
        return View(model);
    }
}
