using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nxb.Models;

[Table("Category")]
public class Category
{
    [Key] public int Id { get; set; }
    [Required(ErrorMessage = "Tên danh mục không được để trống.")]
    [StringLength(100), Column(TypeName = "nvarchar(100)")]
    [Display(Name = "Tên danh mục")] public string Name { get; set; } = "";
    [Range(0, 1), Column(TypeName = "tinyint")]
    [Display(Name = "Trạng thái")] public byte Status { get; set; } = 1;
    [Display(Name = "Ngày tạo")] public DateTime CreatedDate { get; set; }
    [ValidateNever] public ICollection<Product> Products { get; set; } = new List<Product>();
}
