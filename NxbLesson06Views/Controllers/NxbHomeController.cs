using Microsoft.AspNetCore.Mvc;
using NxbLesson06Views.Models;

namespace NxbLesson06Views.Controllers;

public class NxbHomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Thông tin sinh viên";

        var student = new
        {
            studentId = "240001234",
            studentName = "Trinh Van Chung",
            studentClass = "K24CNT2",
            email = "chungtrinh@example.com"
        };

        return View(student);
    }

    public IActionResult About()
    {
        ViewData["Title"] = "Thông tin sinh viên";
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}
