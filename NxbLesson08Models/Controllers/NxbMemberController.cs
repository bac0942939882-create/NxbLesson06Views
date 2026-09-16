using Microsoft.AspNetCore.Mvc;
using NxbLesson08Models.Models;

namespace NxbLesson08Models.Controllers
{
    public class NxbMemberController : Controller
    {
        // Mock data - NxbMember
        private static List<NxbMember> _members = new List<NxbMember>()
        {
            new NxbMember
            {
                NxbMemberId = Guid.NewGuid().ToString(),
                NxbUserName = "tranvanb",
                NxbPassword = "Password123!",
                NxbFullName = "Trần Văn B",
                NxbEmail = "tranvanb@nxb.com"
            },
            new NxbMember
            {
                NxbMemberId = Guid.NewGuid().ToString(),
                NxbUserName = "lethic",
                NxbPassword = "Password456!",
                NxbFullName = "Lê Thị C",
                NxbEmail = "lethic@nxb.com"
            },
            new NxbMember
            {
                NxbMemberId = Guid.NewGuid().ToString(),
                NxbUserName = "phamdungd",
                NxbPassword = "Password789!",
                NxbFullName = "Phạm Dũng D",
                NxbEmail = "phamdungd@nxb.com"
            }
        };

        // GET: Danh sách thành viên
        public IActionResult Index()
        {
            return View(_members);
        }
    }
}
