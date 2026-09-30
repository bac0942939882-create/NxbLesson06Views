using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nxb.Data;
using Nxb.Models;

namespace Nxb.Controllers;

public class CategoriesController(AppDbContext db) : Controller
{
    public async Task<IActionResult> Index() => View(await db.Categories.AsNoTracking().OrderBy(x => x.Id).ToListAsync());
    public async Task<IActionResult> Details(int id)
    {
        var entity = await db.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity == null ? NotFound() : View(entity);
    }
    public IActionResult Create() => View(new Category());
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Status")] Category model)
    {
        model.Name = model.Name?.Trim() ?? "";
        
        if (!ModelState.IsValid) return View(model);
        model.CreatedDate = DateTime.Now;
        db.Categories.Add(model);
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Không thể lưu. Kiểm tra dữ liệu và giá trị trùng."); return View(model); }
        TempData["Success"] = "Đã thêm mới.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id)
    {
        var entity = await db.Categories.FindAsync(id);
        return entity == null ? NotFound() : View(entity);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status")] Category model)
    {
        if (id != model.Id) return BadRequest();
        var entity = await db.Categories.FindAsync(id);
        if (entity == null) return NotFound();
        model.Name = model.Name?.Trim() ?? "";
        
        if (!ModelState.IsValid) return View(model);
        entity.Name = model.Name;
        entity.Status = model.Status;
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Không thể lưu. Kiểm tra dữ liệu và giá trị trùng."); return View(model); }
        TempData["Success"] = "Đã cập nhật.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await db.Categories.FindAsync(id);
        return entity == null ? NotFound() : View(entity);
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var entity = await db.Categories.FindAsync(id);
        if (entity == null) return NotFound();
        if (await db.Products.AnyAsync(p => p.CategoryId == id))
        {
            ModelState.AddModelError("", "Danh mục đang có sản phẩm. Hãy chuyển hoặc xóa sản phẩm trước.");
            return View(entity);
        }
        db.Categories.Remove(entity);
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Dữ liệu đang được bảng khác sử dụng, không thể xóa."); return View(entity); }
        TempData["Success"] = "Đã xóa.";
        return RedirectToAction(nameof(Index));
    }
}
