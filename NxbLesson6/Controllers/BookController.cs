using Microsoft.AspNetCore.Mvc;
using NxbLesson6.Models;

namespace NxbLesson6.Controllers;

public class BookController : Controller
{
    protected Book book = new();

    public IActionResult Index()
    {
        ViewBag.authors = book.Authors;
        ViewBag.genres = book.Genres;
        var books = book.GetBookList();
        return View(books);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.authors = book.Authors;
        ViewBag.genres = book.Genres;
        return View(new Book());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Book model)
    {
        ViewBag.authors = book.Authors;
        ViewBag.genres = book.Genres;
        if (!ModelState.IsValid) return View(model);
        TempData["Message"] = $"Đã nhận thông tin sách: {model.Title}";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        ViewBag.authors = book.Authors;
        ViewBag.genres = book.Genres;
        var model = book.GetBookById(id);
        if (model == null) return NotFound();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Book model)
    {
        ViewBag.authors = book.Authors;
        ViewBag.genres = book.Genres;
        if (!ModelState.IsValid) return View(model);
        TempData["Message"] = $"Đã cập nhật sách: {model.Title}";
        return RedirectToAction(nameof(Index));
    }

    public PartialViewResult PopularBook()
    {
        var books = book.GetBookList();
        return PartialView(books);
    }
}
