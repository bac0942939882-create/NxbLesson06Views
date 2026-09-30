using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nxb.Data;
using Nxb.Models;
using Nxb.ViewModels;
using Nxb.Services;

namespace Nxb.Controllers;

public class StudentsController(StudentDbContext db, ImageUploadService images) : Controller
{
    public async Task<IActionResult> Index() => View(await db.Students.Include(x => x.Class).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync());
    public async Task<IActionResult> Details(int id)
    {
        var entity = await db.Students.Include(x => x.Class).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity == null ? NotFound() : View(entity);
    }
    public async Task<IActionResult> Create()
    {
        await PrepareAsync(0);
        return View(new StudentForm());
    }
    [HttpPost, ValidateAntiForgeryToken, RequestSizeLimit(6 * 1024 * 1024)]
    public async Task<IActionResult> Create(StudentForm form)
    {
        form.Id = 0;
        return await SaveFormAsync(form, new Student(), true);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var entity = await db.Students.FindAsync(id);
        if (entity == null) return NotFound();
        var form = new StudentForm { Id = entity.Id, StudentName = entity.StudentName, StudentEmail = entity.StudentEmail, StudentPhone = entity.StudentPhone, StudentAddress = entity.StudentAddress, StudentBirthday = entity.StudentBirthday, ClassId = entity.ClassId, CurrentImage = entity.StudentAvatar };
        await PrepareAsync(form.ClassId);
        return View(form);
    }
    [HttpPost, ValidateAntiForgeryToken, RequestSizeLimit(6 * 1024 * 1024)]
    public async Task<IActionResult> Edit(int id, StudentForm form)
    {
        if (id != form.Id) return BadRequest();
        var entity = await db.Students.FindAsync(id);
        if (entity == null) return NotFound();
        return await SaveFormAsync(form, entity, false);
    }
    private async Task<IActionResult> SaveFormAsync(StudentForm form, Student entity, bool isNew)
    {
        string view = isNew ? "Create" : "Edit";
        form.CurrentImage = entity.StudentAvatar;
        form.StudentName = form.StudentName?.Trim() ?? "";
        form.StudentEmail = form.StudentEmail?.Trim() ?? "";
        form.StudentPhone = form.StudentPhone?.Trim() ?? "";
        form.StudentAddress = form.StudentAddress?.Trim() ?? "";
        form.StudentEmail = form.StudentEmail.ToLowerInvariant();
        if (!await db.StdClasses.AnyAsync(x => x.Id == form.ClassId))
            ModelState.AddModelError(nameof(form.ClassId), "Lớp không tồn tại.");
        if (await db.Students.AnyAsync(x => x.StudentEmail == form.StudentEmail && x.Id != form.Id))
            ModelState.AddModelError(nameof(form.StudentEmail), "Email đã được sinh viên khác sử dụng.");
        if (await db.Students.AnyAsync(x => x.StudentPhone == form.StudentPhone && x.Id != form.Id))
            ModelState.AddModelError(nameof(form.StudentPhone), "Số điện thoại đã được sinh viên khác sử dụng.");
        if (isNew && (form.ImageFile == null || form.ImageFile.Length == 0))
            ModelState.AddModelError(nameof(form.ImageFile), "Chọn ảnh khi thêm mới.");
        await PrepareAsync(form.ClassId);
        if (!ModelState.IsValid) return View(view, form);
        string? uploaded = null;
        string? previousImage = entity.StudentAvatar;
        try
        {
            if (form.ImageFile != null)
                uploaded = await images.SaveAsync(form.ImageFile, "Avatar");
        }
        catch (InvalidDataException ex)
        {
            ModelState.AddModelError(nameof(form.ImageFile), ex.Message);
            return View(view, form);
        }
        entity.StudentName = form.StudentName;
        entity.StudentEmail = form.StudentEmail;
        entity.StudentPhone = form.StudentPhone;
        entity.StudentAddress = form.StudentAddress;
        entity.StudentBirthday = form.StudentBirthday!.Value;
        entity.ClassId = form.ClassId;
        entity.StudentAvatar = uploaded ?? entity.StudentAvatar;
        if (isNew)
        {
            
            db.Students.Add(entity);
        }
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException)
        {
            images.Delete(uploaded, "Avatar");
            ModelState.AddModelError("", "Không thể lưu. Kiểm tra giá trị trùng và các dữ liệu liên quan.");
            return View(view, form);
        }
        if (uploaded != null) images.Delete(previousImage, "Avatar");
        TempData["Success"] = isNew ? "Đã thêm mới." : "Đã cập nhật.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await db.Students.Include(x => x.Class).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity == null ? NotFound() : View(entity);
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var entity = await db.Students.Include(x => x.Class).FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();
        if (await db.Marks.AnyAsync(x => x.StudentId == id))
        {
            ModelState.AddModelError("", "Sinh viên đang có điểm. Hãy xóa các điểm liên quan trước.");
            return View(entity);
        }
        db.Students.Remove(entity);
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Dữ liệu đang được sử dụng, không thể xóa."); return View(entity); }
        images.Delete(entity.StudentAvatar, "Avatar");
        TempData["Success"] = "Đã xóa.";
        return RedirectToAction(nameof(Index));
    }
    private async Task PrepareAsync(int selected)
    {
        ViewData["ClassId"] = new SelectList(await db.StdClasses.AsNoTracking().OrderBy(x => x.ClassName).ToListAsync(), "Id", "ClassName", selected);
    }
}
