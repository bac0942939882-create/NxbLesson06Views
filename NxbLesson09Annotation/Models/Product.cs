using System.ComponentModel.DataAnnotations;
namespace NxbLesson09Annotation.Models;
public class Product
{
    public int Id { get; set; }
    [Display(Name = "Tên sản phẩm"), Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [StringLength(150, MinimumLength = 6, ErrorMessage = "Tên sản phẩm phải có từ 6 đến 150 ký tự")]
    public string Name { get; set; } = "";
    [Display(Name = "Ảnh sản phẩm"), Required(ErrorMessage = "Vui lòng chọn ảnh sản phẩm")]
    public string Image { get; set; } = "";
    [Display(Name = "Giá chuẩn"), Range(100000, float.MaxValue, ErrorMessage = "Giá chuẩn phải từ 100.000 đồng")]
    [DataType(DataType.Text)] public float Price { get; set; }
    [Display(Name = "Giá khuyến mại"), Range(0, float.MaxValue, ErrorMessage = "Giá khuyến mại không được âm")]
    [DataType(DataType.Text)] public float SalePrice { get; set; }
    [Display(Name = "Mô tả"), Required(ErrorMessage = "Mô tả không được để trống")]
    [StringLength(1500, ErrorMessage = "Mô tả không được quá 1.500 ký tự")]
    public string Description { get; set; } = "";
    [Display(Name = "Danh mục"), Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục")]
    public int CategoryId { get; set; }
}
