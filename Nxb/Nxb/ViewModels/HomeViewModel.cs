using Nxb.Models;
namespace Nxb.ViewModels;

public class HomeViewModel
{
    public List<Banner> Banners { get; set; } = new();
    public List<Product> Products { get; set; } = new();
}
