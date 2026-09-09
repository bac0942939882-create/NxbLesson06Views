using Microsoft.AspNetCore.Mvc;
using NxbLesson06Views.Models;

namespace NxbLesson06Views.ViewComponents;

public class CategoryViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(int? n)
    {
        var categories = new List<Category>
        {
            new Category { CategoryId = 1, CategoryName = "Electronics", status = true },
            new Category { CategoryId = 2, CategoryName = "Books", status = true },
            new Category { CategoryId = 3, CategoryName = "Clothing", status = false },
            new Category { CategoryId = 4, CategoryName = "Home & Kitchen", status = true }
        };

        // Nếu truyền n thì lọc theo CategoryId; nếu không truyền thì hiển thị tất cả.
        if (n.HasValue)
        {
            categories = categories
                .Where(x => x.CategoryId == n.Value)
                .ToList();
        }

        return View(categories);
    }
}
