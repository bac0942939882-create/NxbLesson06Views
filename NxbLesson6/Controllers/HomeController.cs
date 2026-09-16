using Microsoft.AspNetCore.Mvc;
using NxbLesson6.Models;

namespace NxbLesson6.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var products = new List<Product>
        {
            new() { Id = 1, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Price = 1890000, Image = "/images/rice-cooker.svg" },
            new() { Id = 2, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Price = 1990000, Image = "/images/rice-cooker.svg" },
            new() { Id = 3, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", Price = 2090000, Image = "/images/rice-cooker.svg" }
        };
        return View(new HomeViewModel { LatestProducts = products });
    }

    public IActionResult About() => View();
    public IActionResult Contact() => View();
}
