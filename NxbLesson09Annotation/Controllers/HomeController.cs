using Microsoft.AspNetCore.Mvc;
namespace NxbLesson09Annotation.Controllers;
public class HomeController : Controller
{
    public IActionResult Index() => View();
    public IActionResult NxbAbout() => View();
    public IActionResult Error() => View();
}
