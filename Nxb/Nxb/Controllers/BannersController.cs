using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nxb.Data;
using Nxb.Models;
using Nxb.ViewModels;
using Nxb.Services;

namespace Nxb.Controllers;

public class BannersController(AppDbContext db, ImageUploadService images) : Controller
{
    public async Task<IActionResult> Index() => View(await db.Banners.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync());
    public async Task<IActionResult> Details(int id)
    {
        var entity = await db.Banners.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity == null ? NotFound() : View(entity);
    }
    public IActionResult Create()
    {
        
        return View(new BannerForm());
    }
    [HttpPost, ValidateAntiForgeryToken, RequestSizeLimit(6 * 1024 * 1024)]
    public async Task<IActionResult> Create(BannerForm form)
    {
        form.Id = 0;
        return await SaveFormAsync(form, new Banner(), true);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var entity = await db.Banners.FindAsync(id);
        if (entity == null) return NotFound();
        var form = new BannerForm { Id = entity.Id, Name = entity.Name, Description = entity.Description, Status = entity.Status, CurrentImage = entity.Image };
        
        return View(form);
    }
    [HttpPost, ValidateAntiForgeryToken, RequestSizeLimit(6 * 1024 * 1024)]
    public async Task<IActionResult> Edit(int id, BannerForm form)
    {
        if (id != form.Id) return BadRequest();
        var entity = await db.Banners.FindAsync(id);
        if (entity == null) return NotFound();
        return await SaveFormAsync(form, entity, false);
    }
    private async Task<IActionResult> SaveFormAsync(BannerForm form, Banner entity, bool isNew)
    {
        string view = isNew ? "Create" : "Edit";
        form.CurrentImage = entity.Image;
        form.Name = form.Name?.Trim() ?? "";
        
        if (isNew && (form.ImageFile == null || form.ImageFile.Length == 0))
            ModelState.AddModelError(nameof(form.ImageFile), "Chọn ảnh khi thêm mới.");
        
        if (!ModelState.IsValid) return View(view, form);
        string? uploaded = null;
        string? previousImage = entity.Image;
        try
        {
            if (form.ImageFile != null)
                uploaded = await images.SaveAsync(form.ImageFile, "Banner");
        }
        catch (InvalidDataException ex)
        {
            ModelState.AddModelError(nameof(form.ImageFile), ex.Message);
            return View(view, form);
        }
        entity.Name = form.Name;
        entity.Description = form.Description;
        entity.Status = form.Status;
        entity.Image = uploaded ?? entity.Image;
        if (isNew)
        {
            entity.CreatedDate = DateTime.Now;
            db.Banners.Add(entity);
        }
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException)
        {
            images.Delete(uploaded, "Banner");
            ModelState.AddModelError("", "Không thể lưu. Kiểm tra giá trị trùng và các dữ liệu liên quan.");
            return View(view, form);
        }
        if (uploaded != null) images.Delete(previousImage, "Banner");
        TempData["Success"] = isNew ? "Đã thêm mới." : "Đã cập nhật.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await db.Banners.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity == null ? NotFound() : View(entity);
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var entity = await db.Banners.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();
        
        db.Banners.Remove(entity);
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Dữ liệu đang được sử dụng, không thể xóa."); return View(entity); }
        images.Delete(entity.Image, "Banner");
        TempData["Success"] = "Đã xóa.";
        return RedirectToAction(nameof(Index));
    }
    
}
