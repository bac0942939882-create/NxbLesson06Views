using Microsoft.AspNetCore.Mvc;
using NxbLesson09Annotation.Models;
namespace NxbLesson09Annotation.Controllers;
public class NxbMemberController : Controller
{
    private static readonly List<NxbMember> Members = [];
    private static readonly object Gate = new();
    public IActionResult Index() { lock (Gate) return View(Members.ToList()); }
    public IActionResult Create() => View(new NxbMemberRegister());
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(NxbMemberRegister model)
    {
        if (!ModelState.IsValid) return View(model);
        lock (Gate)
        {
            if (Members.Any(m => m.NxbMemberName.Equals(model.NxbUserName, StringComparison.OrdinalIgnoreCase)))
            { ModelState.AddModelError(nameof(model.NxbUserName), "Tên đăng nhập đã tồn tại"); return View(model); }
            Members.Add(new NxbMember { NxbMemberId = Members.Count == 0 ? 1 : Members.Max(m => m.NxbMemberId) + 1,
                NxbMemberName = model.NxbUserName, NxbPassword = model.NxbPassword,
                NxbEmail = model.NxbEmail, NxbPhoneNumber = model.NxbPhoneNumber ?? "",
                NxbFullName = model.NxbFullName, NxbBirthday = model.NxbBirthday });
        }
        TempData["Message"] = "Đăng ký thành viên thành công";
        return RedirectToAction(nameof(Index));
    }
}
