using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nxb.Data;
using Nxb.Models;
using Nxb.ViewModels;
using Nxb.Services;

namespace Nxb.Controllers;

public class ProductsController(AppDbContext db, ImageUploadService images) : Controller
{
    public async Task<IActionResult> Index() => View(await db.Products.Include(x => x.Category).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync());
    public async Task<IActionResult> Details(int id)
    {
        var entity = await db.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity == null ? NotFound() : View(entity);
    }
    public async Task<IActionResult> Create()
    {
        await PrepareAsync(0);
        return View(new ProductForm());
    }
    [HttpPost, ValidateAntiForgeryToken, RequestSizeLimit(6 * 1024 * 1024)]
    public async Task<IActionResult> Create(ProductForm form)
    {
        form.Id = 0;
        return await SaveFormAsync(form, new Product(), true);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var entity = await db.Products.FindAsync(id);
        if (entity == null) return NotFound();
        var form = new ProductForm { Id = entity.Id, Name = entity.Name, Price = entity.Price, SalePrice = entity.SalePrice, Descriptions = entity.Descriptions, CategoryId = entity.CategoryId, Status = entity.Status, CurrentImage = entity.Image };
        await PrepareAsync(form.CategoryId);
        return View(form);
    }
    [HttpPost, ValidateAntiForgeryToken, RequestSizeLimit(6 * 1024 * 1024)]
    public async Task<IActionResult> Edit(int id, ProductForm form)
    {
        if (id != form.Id) return BadRequest();
        var entity = await db.Products.FindAsync(id);
        if (entity == null) return NotFound();
        return await SaveFormAsync(form, entity, false);
    }
    private async Task<IActionResult> SaveFormAsync(ProductForm form, Product entity, bool isNew)
    {
        string view = isNew ? "Create" : "Edit";
        form.CurrentImage = entity.Image;
        form.Name = form.Name?.Trim() ?? "";
        if (!await db.Categories.AnyAsync(x => x.Id == form.CategoryId))
            ModelState.AddModelError(nameof(form.CategoryId), "Danh mục không tồn tại.");
        if (isNew && (form.ImageFile == null || form.ImageFile.Length == 0))
            ModelState.AddModelError(nameof(form.ImageFile), "Chọn ảnh khi thêm mới.");
        await PrepareAsync(form.CategoryId);
        if (!ModelState.IsValid) return View(view, form);
        string? uploaded = null;
        string? previousImage = entity.Image;
        try
        {
            if (form.ImageFile != null)
                uploaded = await images.SaveAsync(form.ImageFile, "Product");
        }
        catch (InvalidDataException ex)
        {
            ModelState.AddModelError(nameof(form.ImageFile), ex.Message);
            return View(view, form);
        }
        entity.Name = form.Name;
        entity.Price = form.Price!.Value;
        entity.SalePrice = form.SalePrice!.Value;
        entity.Descriptions = form.Descriptions;
        entity.CategoryId = form.CategoryId;
        entity.Status = form.Status;
        entity.Image = uploaded ?? entity.Image;
        if (isNew)
        {
            entity.CreatedDate = DateTime.Now;
            db.Products.Add(entity);
        }
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException)
        {
            images.Delete(uploaded, "Product");
            ModelState.AddModelError("", "Không thể lưu. Kiểm tra giá trị trùng và các dữ liệu liên quan.");
            return View(view, form);
        }
        if (uploaded != null) images.Delete(previousImage, "Product");
        TempData["Success"] = isNew ? "Đã thêm mới." : "Đã cập nhật.";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await db.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity == null ? NotFound() : View(entity);
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var entity = await db.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return NotFound();
        
        db.Products.Remove(entity);
        try { await db.SaveChangesAsync(); }
        catch (DbUpdateException) { ModelState.AddModelError("", "Dữ liệu đang được sử dụng, không thể xóa."); return View(entity); }
        images.Delete(entity.Image, "Product");
        TempData["Success"] = "Đã xóa.";
        return RedirectToAction(nameof(Index));
    }
    private async Task PrepareAsync(int selected)
    {
        ViewData["CategoryId"] = new SelectList(await db.Categories.AsNoTracking().OrderBy(x => x.Name).ToListAsync(), "Id", "Name", selected);
    }
}
