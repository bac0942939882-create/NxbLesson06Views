using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nxb.Models;

[Table("Product")]
public class Product
{
    [Key] public int Id { get; set; }
    [Required, StringLength(150), Column(TypeName = "nvarchar(150)")]
    [Display(Name = "Tên sản phẩm")] public string Name { get; set; } = "";
    [Required, StringLength(150), Column(TypeName = "varchar(150)")]
    [Display(Name = "Ảnh")] public string Image { get; set; } = "";
    [Range(0, float.MaxValue), Display(Name = "Giá")] public float Price { get; set; }
    [Range(0, float.MaxValue), Display(Name = "Giá giảm")] public float SalePrice { get; set; }
    [Range(0, 1), Column(TypeName = "tinyint")]
    [Display(Name = "Trạng thái")] public byte Status { get; set; } = 1;
    [StringLength(1000), Column(TypeName = "ntext")]
    [Display(Name = "Mô tả")] public string? Descriptions { get; set; }
    [Range(1, int.MaxValue), Display(Name = "Danh mục")] public int CategoryId { get; set; }
    [Display(Name = "Ngày tạo")] public DateTime CreatedDate { get; set; }
    [ValidateNever] public Category Category { get; set; } = null!;
}
