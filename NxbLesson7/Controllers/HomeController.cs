using Microsoft.AspNetCore.Mvc;

namespace NxbLesson7.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
