using Microsoft.AspNetCore.Mvc;
namespace NxbLesson14.ViewComponents;
public class AdminFooterViewComponent : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}
