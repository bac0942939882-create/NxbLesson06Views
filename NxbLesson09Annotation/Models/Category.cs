using System.ComponentModel.DataAnnotations;
namespace NxbLesson09Annotation.Models;
public class Category
{
    public int Id { get; set; }
    [Display(Name = "Tên danh mục"), Required(ErrorMessage = "Tên danh mục không được để trống")]
    [StringLength(150, MinimumLength = 6, ErrorMessage = "Tên danh mục phải có từ 6 đến 150 ký tự")]
    public string Name { get; set; } = "";
}
