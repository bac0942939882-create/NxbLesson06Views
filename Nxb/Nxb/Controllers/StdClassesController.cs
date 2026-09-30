using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nxb.Data;
using Nxb.Models;

namespace Nxb.Controllers;

public class StdClassesController(StudentDbContext db) : Controller
{
    public async Task<IActionResult> Index() => View(await db.StdClasses.AsNoTracking().OrderBy(x => x.Id).ToListAsync());
    public async Task<IActionResult> Details(int id)
    {
        var entity = await db.StdClasses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity == null ? NotFound() : View(entity);
    }
    public IActionResult Create() => View(new StdClass());
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ClassName")] StdClass model)
    {
        model.ClassName = model.ClassName?.Trim() ?? "";
        
        if (!ModelState.IsValid) return View(model);
        
        db.StdClasses.Add(model);
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Không thể lưu. Kiểm tra dữ liệu và giá trị trùng."); return View(model); }
        TempData["Success"] = "Đã thêm mới.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id)
    {
        var entity = await db.StdClasses.FindAsync(id);
        return entity == null ? NotFound() : View(entity);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ClassName")] StdClass model)
    {
        if (id != model.Id) return BadRequest();
        var entity = await db.StdClasses.FindAsync(id);
        if (entity == null) return NotFound();
        model.ClassName = model.ClassName?.Trim() ?? "";
        
        if (!ModelState.IsValid) return View(model);
        entity.ClassName = model.ClassName;
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Không thể lưu. Kiểm tra dữ liệu và giá trị trùng."); return View(model); }
        TempData["Success"] = "Đã cập nhật.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await db.StdClasses.FindAsync(id);
        return entity == null ? NotFound() : View(entity);
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var entity = await db.StdClasses.FindAsync(id);
        if (entity == null) return NotFound();
        if (await db.Students.AnyAsync(s => s.ClassId == id))
        {
            ModelState.AddModelError("", "Lớp đang có sinh viên. Hãy chuyển hoặc xóa sinh viên trước.");
            return View(entity);
        }
        db.StdClasses.Remove(entity);
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Dữ liệu đang được bảng khác sử dụng, không thể xóa."); return View(entity); }
        TempData["Success"] = "Đã xóa.";
        return RedirectToAction(nameof(Index));
    }
}
