using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NxbLesson14.Data;
using NxbLesson14.Models;
namespace NxbLesson14.Areas.Admin.Controllers;

[Area("Admin")]
public class BannerController(NxbLesson14Context db) : Controller
{
    public async Task<IActionResult> Index(string? search)
    {
        var query = db.Banners.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(x => x.Name.Contains(search.Trim()));
        ViewBag.Search = search;
        return View(await query.OrderByDescending(x => x.Id).ToListAsync());
    }
    public async Task<IActionResult> Details(int id)
    {
        var model = await db.Banners.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (model == null) return NotFound();
        return View(model);
    }
    public async Task<IActionResult> Create()
    {
        
        await Task.CompletedTask;
        return View(new Banner());
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Status,Image,Description,Prioty")] Banner model)
    {
        await ValidateAsync(model);
        if (ModelState.IsValid)
        {
            db.Banners.Add(model);
            try
            {
                await db.SaveChangesAsync();
                TempData["Success"] = "Đã thêm mới thành công.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Không thể lưu dữ liệu. Kiểm tra tên trùng và dữ liệu liên quan.");
            }
        }
        
        return View(model);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var model = await db.Banners.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (model == null) return NotFound();
        
        return View(model);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,Image,Description,Prioty")] Banner model)
    {
        if (id != model.Id) return BadRequest();
        var current = await db.Banners.FindAsync(id);
        if (current == null) return NotFound();
        await ValidateAsync(model);
        if (!ModelState.IsValid)
        {
            
            return View(model);
        }
        current.Name = model.Name;
        current.Status = model.Status;
        current.Image = model.Image;
        current.Description = model.Description;
        current.Prioty = model.Prioty;
        try
        {
            await db.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật thành công.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await db.Banners.AnyAsync(x => x.Id == id)) return NotFound();
            ModelState.AddModelError("", "Dữ liệu vừa thay đổi. Vui lòng tải lại trang.");
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Không thể lưu dữ liệu. Kiểm tra tên trùng và dữ liệu liên quan.");
        }
        
        return View(model);
    }
    public async Task<IActionResult> Delete(int id)
    {
        var model = await db.Banners.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (model == null) return NotFound();
        return View(model);
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var model = await db.Banners.FirstOrDefaultAsync(x => x.Id == id);
        if (model == null) return NotFound();

        db.Banners.Remove(model);
        try
        {
            await db.SaveChangesAsync();
            TempData["Success"] = "Đã xóa thành công.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Không thể xóa vì có dữ liệu liên quan hoặc dữ liệu vừa thay đổi.");
            return View("Delete", model);
        }
    }
    private async Task ValidateAsync(Banner model)
    {
        model.Name = (model.Name ?? "").Trim();
        if (string.IsNullOrWhiteSpace(model.Name))
            ModelState.AddModelError(nameof(model.Name), "Tên không được để trống.");
        // So sánh tên Unicode ở C# để cả SQL Server và SQLite xử lý tiếng Việt giống nhau.
        var otherNames = await db.Banners.AsNoTracking().Where(x => x.Id != model.Id)
            .Select(x => x.Name).ToListAsync();
        if (otherNames.Any(name => string.Equals(name, model.Name, StringComparison.OrdinalIgnoreCase)))
            ModelState.AddModelError(nameof(model.Name), "Tên đã tồn tại, vui lòng chọn tên khác.");

    }

}
