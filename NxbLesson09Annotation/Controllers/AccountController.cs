using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using NxbLesson09Annotation.Models;
namespace NxbLesson09Annotation.Controllers;
public class AccountController : Controller
{
    private static readonly List<Account> Accounts = [];
    private static readonly object Gate = new();
    public IActionResult Index() { lock (Gate) return View(Accounts.ToList()); }
    public IActionResult Create() => View(new Account());
    [AcceptVerbs("GET", "POST")]
    public IActionResult VerifyPhone(string phone)
    {
        bool valid = !string.IsNullOrWhiteSpace(phone) && Regex.IsMatch(phone, @"^\d{3}[-.]?\d{3}[-.]?\d{4}$");
        lock (Gate) valid &= !Accounts.Any(a => a.Phone == phone);
        return Json(valid ? true : "Số điện thoại phải có 10 chữ số (có thể ngăn bằng dấu chấm hoặc gạch ngang) và chưa được sử dụng");
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Account model)
    {
        if (!Regex.IsMatch(model.Phone ?? "", @"^\d{3}[-.]?\d{3}[-.]?\d{4}$"))
            ModelState.AddModelError(nameof(model.Phone), "Số điện thoại phải gồm 10 chữ số, có thể ngăn bằng dấu chấm hoặc gạch ngang");
        if (!ModelState.IsValid) return View(model);
        lock (Gate)
        {
            if (Accounts.Any(a => a.Phone == model.Phone))
            { ModelState.AddModelError(nameof(model.Phone), "Số điện thoại đã được sử dụng"); return View(model); }
            model.Id = Accounts.Count == 0 ? 1 : Accounts.Max(a => a.Id) + 1;
            Accounts.Add(model);
        }
        TempData["Message"] = "Đã thêm tài khoản thành công";
        return RedirectToAction(nameof(Index));
    }
}
