using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NguyenXuanBac2410900011_exam.Models;

namespace NguyenXuanBac2410900011_exam.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
    public IActionResult NxbAbout() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
