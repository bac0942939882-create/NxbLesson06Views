using System.Text.Json;
using NxbLesson09Annotation.Models;
namespace NxbLesson09Annotation.Services;
public class ProductStore
{
    private readonly object _gate = new();
    private readonly string _path;
    private List<Product> _products;
    public IReadOnlyList<Category> Categories { get; } = [
        new() { Id = 1, Name = "Máy tính xách tay" },
        new() { Id = 2, Name = "Linh kiện máy tính" },
        new() { Id = 3, Name = "Thiết bị ngoại vi" }
    ];
    public ProductStore(IWebHostEnvironment environment)
    {
        _path = Path.Combine(environment.ContentRootPath, "Data", "products.json");
        Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
        _products = File.Exists(_path) ? JsonSerializer.Deserialize<List<Product>>(File.ReadAllText(_path)) ?? [] : [];
    }
    public List<Product> All() { lock (_gate) return _products.Select(Clone).ToList(); }
    public Product? Find(int id) { lock (_gate) return _products.Where(x => x.Id == id).Select(Clone).FirstOrDefault(); }
    public void Add(Product item)
    {
        lock (_gate) { item.Id = _products.Count == 0 ? 1 : _products.Max(x => x.Id) + 1; _products.Add(Clone(item)); Save(); }
    }
    public bool Update(Product item)
    {
        lock (_gate)
        {
            int index = _products.FindIndex(x => x.Id == item.Id);
            if (index < 0) return false;
            _products[index] = Clone(item); Save(); return true;
        }
    }
    public bool Delete(int id)
    {
        lock (_gate)
        {
            int count = _products.RemoveAll(x => x.Id == id);
            if (count > 0) Save(); return count > 0;
        }
    }
    private void Save()
    {
        var temp = _path + ".tmp";
        File.WriteAllText(temp, JsonSerializer.Serialize(_products, new JsonSerializerOptions { WriteIndented = true }));
        File.Move(temp, _path, true);
    }
    private static Product Clone(Product x) => new() { Id = x.Id, Name = x.Name, Image = x.Image,
        Price = x.Price, SalePrice = x.SalePrice, Description = x.Description, CategoryId = x.CategoryId };
}
