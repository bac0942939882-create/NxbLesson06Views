using Microsoft.AspNetCore.Mvc;
using NxbLesson08Models.Models;
using System.Diagnostics;

namespace NxbLesson08Models.Controllers
{
    public class NxbHomeController : Controller
    {
        private readonly ILogger<NxbHomeController> _logger;

        public NxbHomeController(ILogger<NxbHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult NxbIndex()
        {
            return View();
        }

        public IActionResult NxbPrivacy()
        {
            return View();
        }
        public IActionResult NxbAbout()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
