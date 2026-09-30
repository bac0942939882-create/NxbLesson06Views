using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nxb.Data;
using Nxb.Models;

namespace Nxb.Controllers;

public class SubjectsController(StudentDbContext db) : Controller
{
    public async Task<IActionResult> Index() => View(await db.Subjects.AsNoTracking().OrderBy(x => x.Id).ToListAsync());
    public async Task<IActionResult> Details(int id)
    {
        var entity = await db.Subjects.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity == null ? NotFound() : View(entity);
    }
    public IActionResult Create() => View(new Subjects());
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("SubjectName")] Subjects model)
    {
        int id = 0;
        model.SubjectName = model.SubjectName?.Trim() ?? "";
        if (await db.Subjects.AnyAsync(x => x.SubjectName == model.SubjectName && x.Id != id))
            ModelState.AddModelError(nameof(model.SubjectName), "Tên môn học đã tồn tại.");
        if (!ModelState.IsValid) return View(model);
        
        db.Subjects.Add(model);
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Không thể lưu. Kiểm tra dữ liệu và giá trị trùng."); return View(model); }
        TempData["Success"] = "Đã thêm mới.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int id)
    {
        var entity = await db.Subjects.FindAsync(id);
        return entity == null ? NotFound() : View(entity);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectName")] Subjects model)
    {
        if (id != model.Id) return BadRequest();
        var entity = await db.Subjects.FindAsync(id);
        if (entity == null) return NotFound();
        model.SubjectName = model.SubjectName?.Trim() ?? "";
        if (await db.Subjects.AnyAsync(x => x.SubjectName == model.SubjectName && x.Id != id))
            ModelState.AddModelError(nameof(model.SubjectName), "Tên môn học đã tồn tại.");
        if (!ModelState.IsValid) return View(model);
        entity.SubjectName = model.SubjectName;
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Không thể lưu. Kiểm tra dữ liệu và giá trị trùng."); return View(model); }
        TempData["Success"] = "Đã cập nhật.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await db.Subjects.FindAsync(id);
        return entity == null ? NotFound() : View(entity);
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var entity = await db.Subjects.FindAsync(id);
        if (entity == null) return NotFound();
        if (await db.Marks.AnyAsync(m => m.SubjectId == id))
        {
            ModelState.AddModelError("", "Môn học đang có điểm. Hãy xóa điểm liên quan trước.");
            return View(entity);
        }
        db.Subjects.Remove(entity);
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Dữ liệu đang được bảng khác sử dụng, không thể xóa."); return View(entity); }
        TempData["Success"] = "Đã xóa.";
        return RedirectToAction(nameof(Index));
    }
}
