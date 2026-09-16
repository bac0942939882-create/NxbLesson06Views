using Microsoft.AspNetCore.Mvc;
using NxbLesson6.Models;

namespace NxbLesson6.ViewComponents;

public class BookViewComponent : ViewComponent
{
    protected Book _book = new();

    public IViewComponentResult Invoke()
    {
        var books = _book.GetBookList();
        return View(books);
    }
}
