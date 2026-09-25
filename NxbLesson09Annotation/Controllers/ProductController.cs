using Microsoft.AspNetCore.Mvc;
using NxbLesson09Annotation.Models;
using NxbLesson09Annotation.Services;
namespace NxbLesson09Annotation.Controllers;
public class ProductController(ProductStore store, IWebHostEnvironment environment) : Controller
{
    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
    public IActionResult Index() { ViewBag.Categories = store.Categories; return View(store.All()); }
    public IActionResult Details(int id) { var product = store.Find(id); if (product is null) return NotFound(); ViewBag.Category = store.Categories.FirstOrDefault(x => x.Id == product.CategoryId)?.Name; return View(product); }
    public IActionResult Create() { ViewBag.Categories = store.Categories; return View(new ProductForm()); }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductForm model)
    {
        ValidateForm(model, false);
        if (!ModelState.IsValid) { ViewBag.Categories = store.Categories; return View(model); }
        var image = await SaveImage(model.ImageFile!);
        store.Add(ToProduct(model, image));
        TempData["Message"] = "Đã thêm sản phẩm thành công";
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Edit(int id)
    {
        var product = store.Find(id); if (product is null) return NotFound();
        ViewBag.Categories = store.Categories;
        return View(ToForm(product));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductForm model)
    {
        var old = store.Find(id); if (old is null) return NotFound();
        model.Id = id;
        ValidateForm(model, true);
        if (!ModelState.IsValid) { ViewBag.Categories = store.Categories; model.ExistingImage = old.Image; return View(model); }
        string image = model.ImageFile is { Length: > 0 } ? await SaveImage(model.ImageFile) : old.Image;
        store.Update(ToProduct(model, image));
        if (image != old.Image) DeleteImage(old.Image);
        TempData["Message"] = "Đã cập nhật sản phẩm";
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Delete(int id) { var product = store.Find(id); return product is null ? NotFound() : View(product); }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = store.Find(id); if (product is null) return NotFound();
        store.Delete(id); DeleteImage(product.Image);
        TempData["Message"] = "Đã xóa sản phẩm";
        return RedirectToAction(nameof(Index));
    }
    private void ValidateForm(ProductForm model, bool editing)
    {
        if (!store.Categories.Any(x => x.Id == model.CategoryId)) ModelState.AddModelError(nameof(model.CategoryId), "Danh mục đã chọn không tồn tại");
        if (!editing && model.ImageFile is null) ModelState.AddModelError(nameof(model.ImageFile), "Vui lòng chọn ảnh sản phẩm");
        if (model.ImageFile is not null)
        {
            if (model.ImageFile.Length == 0 || model.ImageFile.Length > 5 * 1024 * 1024)
                ModelState.AddModelError(nameof(model.ImageFile), "Ảnh phải có dung lượng từ 1 byte đến 5 MB");
            if (!AllowedExtensions.Contains(Path.GetExtension(model.ImageFile.FileName)) || !IsImageType(model.ImageFile.ContentType))
                ModelState.AddModelError(nameof(model.ImageFile), "Chỉ nhận ảnh JPG, PNG, WebP hoặc GIF");
        }
    }
    private static bool IsImageType(string contentType) => contentType is "image/jpeg" or "image/png" or "image/webp" or "image/gif";
    private async Task<string> SaveImage(IFormFile file)
    {
        var folder = Path.Combine(environment.WebRootPath, "products"); Directory.CreateDirectory(folder);
        var name = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName).ToLowerInvariant()}";
        await using var output = System.IO.File.Create(Path.Combine(folder, name));
        await file.CopyToAsync(output);
        return $"/products/{name}";
    }
    private void DeleteImage(string image)
    {
        var filename = Path.GetFileName(image);
        if (!string.IsNullOrWhiteSpace(filename) && image.StartsWith("/products/", StringComparison.Ordinal))
        { var path = Path.Combine(environment.WebRootPath, "products", filename); if (System.IO.File.Exists(path)) System.IO.File.Delete(path); }
    }
    private static Product ToProduct(ProductForm form, string image) => new() { Id = form.Id, Name = form.Name, Image = image,
        Price = form.Price, SalePrice = form.SalePrice, Description = form.Description, CategoryId = form.CategoryId };
    private static ProductForm ToForm(Product item) => new() { Id = item.Id, Name = item.Name, ExistingImage = item.Image,
        Price = item.Price, SalePrice = item.SalePrice, Description = item.Description, CategoryId = item.CategoryId };
}
