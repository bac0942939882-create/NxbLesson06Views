using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nxb.Data;
using Nxb.Models;
using Nxb.ViewModels;

namespace Nxb.Controllers;

public class MarksController(StudentDbContext db) : Controller
{
    private IQueryable<Marks> Query() => db.Marks.Include(x => x.Student).Include(x => x.Subject);
    public async Task<IActionResult> Index() => View(await Query().AsNoTracking()
        .OrderBy(x => x.StudentId).ThenBy(x => x.SubjectId).ToListAsync());
    public async Task<IActionResult> Details(int subjectId, int studentId)
    {
        var entity = await Query().AsNoTracking().FirstOrDefaultAsync(x => x.SubjectId == subjectId && x.StudentId == studentId);
        return entity == null ? NotFound() : View(entity);
    }
    public async Task<IActionResult> Create()
    {
        await PrepareAsync(new MarksForm());
        return View(new MarksForm());
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MarksForm form)
    {
        await ValidateReferencesAsync(form);
        if (await db.Marks.AnyAsync(x => x.SubjectId == form.SubjectId && x.StudentId == form.StudentId))
            ModelState.AddModelError("", "Sinh viên đã có điểm môn này. Hãy dùng chức năng sửa.");
        if (!ModelState.IsValid) { await PrepareAsync(form); return View(form); }
        db.Marks.Add(new Marks { SubjectId = form.SubjectId, StudentId = form.StudentId, Score = form.Score!.Value });
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Không thể lưu điểm. Kiểm tra điểm trùng và dữ liệu liên quan."); await PrepareAsync(form); return View(form); }
        TempData["Success"] = "Đã thêm điểm.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int subjectId, int studentId)
    {
        var entity = await db.Marks.FindAsync(subjectId, studentId);
        if (entity == null) return NotFound();
        var form = new MarksForm { SubjectId = subjectId, StudentId = studentId, Score = entity.Score };
        await PrepareAsync(form);
        return View(form);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int originalSubjectId, int originalStudentId, MarksForm form)
    {
        if (originalSubjectId != form.SubjectId || originalStudentId != form.StudentId) return BadRequest();
        var entity = await db.Marks.FindAsync(originalSubjectId, originalStudentId);
        if (entity == null) return NotFound();
        await ValidateReferencesAsync(form);
        if (!ModelState.IsValid) { await PrepareAsync(form); return View(form); }
        entity.Score = form.Score!.Value;
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Không thể cập nhật điểm."); await PrepareAsync(form); return View(form); }
        TempData["Success"] = "Đã cập nhật điểm.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int subjectId, int studentId)
    {
        var entity = await Query().AsNoTracking().FirstOrDefaultAsync(x => x.SubjectId == subjectId && x.StudentId == studentId);
        return entity == null ? NotFound() : View(entity);
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int subjectId, int studentId)
    {
        var entity = await db.Marks.FindAsync(subjectId, studentId);
        if (entity == null) return NotFound();
        db.Marks.Remove(entity);
        await db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa điểm.";
        return RedirectToAction(nameof(Index));
    }
    private async Task ValidateReferencesAsync(MarksForm form)
    {
        if (!await db.Students.AnyAsync(x => x.Id == form.StudentId)) ModelState.AddModelError(nameof(form.StudentId), "Sinh viên không tồn tại.");
        if (!await db.Subjects.AnyAsync(x => x.Id == form.SubjectId)) ModelState.AddModelError(nameof(form.SubjectId), "Môn học không tồn tại.");
    }
    private async Task PrepareAsync(MarksForm form)
    {
        ViewData["StudentId"] = new SelectList(await db.Students.AsNoTracking().OrderBy(x => x.StudentName)
            .Select(x => new { x.Id, Name = x.StudentName + " (#" + x.Id + ")" }).ToListAsync(), "Id", "Name", form.StudentId);
        ViewData["SubjectId"] = new SelectList(await db.Subjects.AsNoTracking().OrderBy(x => x.SubjectName).ToListAsync(), "Id", "SubjectName", form.SubjectId);
    }
}
