using Microsoft.AspNetCore.Mvc.Rendering;

namespace NxbLesson6.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public string Image { get; set; } = "";
    public decimal Price { get; set; }
    public int TotalPage { get; set; }
    public string Summary { get; set; } = "";

    public List<SelectListItem> Authors { get; } = new()
    {
        new SelectListItem { Value = "1", Text = "Nam cao" },
        new SelectListItem { Value = "2", Text = "Ngô Tất Tố" },
        new SelectListItem { Value = "3", Text = "Adamkhoom" },
        new SelectListItem { Value = "4", Text = "Thiền sư Thích Nhất Hạnh" }
    };

    public List<SelectListItem> Genres { get; } = new()
    {
        new SelectListItem { Value = "1", Text = "Truyện tranh" },
        new SelectListItem { Value = "2", Text = "Văn học đương đại" },
        new SelectListItem { Value = "3", Text = "Phật học phổ thông" },
        new SelectListItem { Value = "4", Text = "Truyện cười" }
    };

    public List<Book> GetBookList() => new()
    {
        new Book { Id = 1, Title = "Chí Phèo", AuthorId = 1, GenreId = 1, Image = "/images/products/b1.svg", Price = 500000, TotalPage = 250, Summary = "Tác phẩm nổi tiếng của Nam Cao." },
        new Book { Id = 2, Title = "Lão Hạc", AuthorId = 1, GenreId = 1, Image = "/images/products/b2.svg", Price = 700000, TotalPage = 400, Summary = "Truyện ngắn giàu giá trị nhân văn." },
        new Book { Id = 4, Title = "Conan Phiêu lưu ký", AuthorId = 1, GenreId = 1, Image = "/images/products/b3.svg", Price = 550000, TotalPage = 300, Summary = "Một ấn phẩm truyện tranh phiêu lưu." },
        new Book { Id = 6, Title = "Đường Xưa Mây Trắng", AuthorId = 4, GenreId = 3, Image = "/images/products/b4.svg", Price = 850000, TotalPage = 500, Summary = "Tác phẩm về cuộc đời Đức Phật." }
    };

    public Book? GetBookById(int id) => GetBookList().FirstOrDefault(b => b.Id == id);
}
