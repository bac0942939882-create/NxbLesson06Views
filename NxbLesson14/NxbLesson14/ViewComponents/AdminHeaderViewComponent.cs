using Microsoft.AspNetCore.Mvc;
namespace NxbLesson14.ViewComponents;
public class AdminHeaderViewComponent : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}
