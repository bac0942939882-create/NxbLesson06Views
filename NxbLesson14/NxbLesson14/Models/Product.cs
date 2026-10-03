using System.ComponentModel.DataAnnotations;

namespace NxbLesson14.Models;

public class Product
{

    public int Id { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập tên.")]
    [StringLength(100, ErrorMessage = "Tên tối đa 100 ký tự.")]
    [Display(Name = "Tên")]
    public string Name { get; set; } = "";
    [Range(0, 1, ErrorMessage = "Trạng thái chỉ nhận 0 hoặc 1.")]
    [Display(Name = "Trạng thái")]
    public byte Status { get; set; } = 1;
    [StringLength(100, ErrorMessage = "Đường dẫn ảnh tối đa 100 ký tự.")]
    [RegularExpression(@"/?(?:images|dist/img)/[a-zA-Z0-9_./-]+\.(?:png|jpg|jpeg|gif|webp|svg)", ErrorMessage = "Nhập đường dẫn ảnh nội bộ, ví dụ /images/demo/348x261.png.")]
    [Display(Name = "Đường dẫn ảnh")]
    public string? Image { get; set; }
    [StringLength(350, ErrorMessage = "Mô tả tối đa 350 ký tự.")]
    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Ngày tạo")]
    public DateTime CreatedDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Vui lòng nhập giá.")]
    [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số không âm.")]
    [Display(Name = "Giá")]
    public double Price { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Giá khuyến mại phải là số không âm.")]
    [Display(Name = "Giá khuyến mại")]
    public double SalePrice { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục.")]
    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
