using Microsoft.AspNetCore.Mvc;
namespace NxbLesson14.ViewComponents;
public class NavLeftViewComponent : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}
