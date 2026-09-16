using Microsoft.AspNetCore.Mvc;
using NxbLesson7.Models.DataModels;

namespace NxbLesson7.Controllers;

public class NxbMemberController : Controller
{
    // Mock data - tương tự phần minh họa trong video/slide.
    private static readonly List<NxbMember> _members = new()
    {
        new NxbMember
        {
            NxbMemberId = "Nxb001",
            NxbUserName = "nxbuser",
            NxbPassword = "password123",
            NxbFullName = "Nguyễn Xuân Bắc",
            NxbEmail = "nguyenxuanbac@gmail.com"
        },
        new NxbMember
        {
            NxbMemberId = "Nxb002",
            NxbUserName = "student01",
            NxbPassword = "123123",
            NxbFullName = "Nguyễn Văn An",
            NxbEmail = "nguyenvanan@gmail.com"
        },
        new NxbMember
        {
            NxbMemberId = "Nxb003",
            NxbUserName = "student02",
            NxbPassword = "456456",
            NxbFullName = "Trần Minh Anh",
            NxbEmail = "tranminhanh@gmail.com"
        },
        new NxbMember
        {
            NxbMemberId = "Nxb004",
            NxbUserName = "student03",
            NxbPassword = "789789",
            NxbFullName = "Lê Quốc Bảo",
            NxbEmail = "lequocbao@gmail.com"
        },
        new NxbMember
        {
            NxbMemberId = "Nxb005",
            NxbUserName = "student04",
            NxbPassword = "abcabc",
            NxbFullName = "Phạm Hoàng Nam",
            NxbEmail = "phamhoangnam@gmail.com"
        }
    };

    public IActionResult Index()
    {
        return View(_members);
    }

    // Truyền một object sang View bằng ViewBag - đúng ý phần Model -> View.
    public IActionResult GetMember()
    {
        var member = new NxbMember
        {
            NxbMemberId = Guid.NewGuid().ToString(),
            NxbUserName = "nxbuser",
            NxbPassword = "password123",
            NxbFullName = "Nguyễn Xuân Bắc",
            NxbEmail = "nguyenxuanbac@gmail.com"
        };

        ViewData["Title"] = "Thông tin member";
        ViewBag.Member = member;
        return View();
    }

    // Truyền List<NxbMember> sang View bằng ViewBag.
    public IActionResult Members()
    {
        ViewData["Title"] = "Danh sách thành viên";
        ViewBag.Members = _members;
        return View();
    }

    // Strongly Typed View.
    public IActionResult Detail(string id = "Nxb001")
    {
        var member = _members.FirstOrDefault(x => x.NxbMemberId == id) ?? _members[0];
        return View(member);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = "Thêm mới thành viên";
        return View(new NxbMember());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(NxbMember member)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Thêm mới thành viên";
            return View(member);
        }

        _members.Add(member);
        return RedirectToAction(nameof(Index));
    }
}
