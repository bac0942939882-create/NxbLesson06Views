using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nxb.ViewModels;

public class ProductForm : IValidatableObject
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nhập tên sản phẩm."), StringLength(150)]
    [Display(Name = "Tên sản phẩm")] public string Name { get; set; } = "";
    [Required(ErrorMessage = "Nhập giá sản phẩm."), Range(0, float.MaxValue)]
    [Display(Name = "Giá")] public float? Price { get; set; }
    [Required(ErrorMessage = "Nhập giá giảm (0 nếu không giảm)."), Range(0, float.MaxValue)]
    [Display(Name = "Giá giảm")] public float? SalePrice { get; set; }
    [StringLength(1000), Display(Name = "Mô tả")] public string? Descriptions { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Chọn danh mục.")]
    [Display(Name = "Danh mục")] public int CategoryId { get; set; }
    [Range(0, 1), Display(Name = "Trạng thái")] public byte Status { get; set; } = 1;
    [Display(Name = "Ảnh sản phẩm")] public IFormFile? ImageFile { get; set; }
    [BindNever, ValidateNever] public string? CurrentImage { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (Price.HasValue && !float.IsFinite(Price.Value))
            yield return new ValidationResult("Giá không hợp lệ.", new[] { nameof(Price) });
        if (SalePrice.HasValue && !float.IsFinite(SalePrice.Value))
            yield return new ValidationResult("Giá giảm không hợp lệ.", new[] { nameof(SalePrice) });
        if (Price.HasValue && SalePrice > Price)
            yield return new ValidationResult("Giá giảm không được lớn hơn giá gốc.", new[] { nameof(SalePrice) });
    }
}

public class BannerForm
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nhập tên banner."), StringLength(150)]
    [Display(Name = "Tên banner")] public string Name { get; set; } = "";
    [StringLength(1000), Display(Name = "Mô tả")] public string? Description { get; set; }
    [Range(0, 1), Display(Name = "Trạng thái")] public byte Status { get; set; } = 1;
    [Display(Name = "Ảnh banner")] public IFormFile? ImageFile { get; set; }
    [BindNever, ValidateNever] public string? CurrentImage { get; set; }
}

public class StudentForm : IValidatableObject
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nhập họ tên."), StringLength(100)]
    [Display(Name = "Họ tên")] public string StudentName { get; set; } = "";
    [Required(ErrorMessage = "Nhập email."), EmailAddress(ErrorMessage = "Email không đúng định dạng."), StringLength(100)]
    [Display(Name = "Email")] public string StudentEmail { get; set; } = "";
    [Required(ErrorMessage = "Nhập số điện thoại."), Phone(ErrorMessage = "Số điện thoại không hợp lệ."), StringLength(50, MinimumLength = 9)]
    [Display(Name = "Số điện thoại")] public string StudentPhone { get; set; } = "";
    [Required(ErrorMessage = "Nhập địa chỉ."), StringLength(150)]
    [Display(Name = "Địa chỉ")] public string StudentAddress { get; set; } = "";
    [Required(ErrorMessage = "Chọn ngày sinh."), DataType(DataType.Date)]
    [Display(Name = "Ngày sinh")] public DateTime? StudentBirthday { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Chọn lớp.")]
    [Display(Name = "Lớp")] public int ClassId { get; set; }
    [Display(Name = "Ảnh đại diện")] public IFormFile? ImageFile { get; set; }
    [BindNever, ValidateNever] public string? CurrentImage { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (StudentBirthday.HasValue && (StudentBirthday.Value.Date > DateTime.Today || StudentBirthday.Value.Year < 1900))
            yield return new ValidationResult("Ngày sinh phải từ năm 1900 đến hôm nay.", new[] { nameof(StudentBirthday) });
    }
}

public class MarksForm : IValidatableObject
{
    [Range(1, int.MaxValue, ErrorMessage = "Chọn môn học."), Display(Name = "Môn học")] public int SubjectId { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Chọn sinh viên."), Display(Name = "Sinh viên")] public int StudentId { get; set; }
    [Required(ErrorMessage = "Nhập điểm."), Range(0d, 10d, ErrorMessage = "Điểm phải từ 0 đến 10.")]
    [Display(Name = "Điểm")] public double? Score { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (Score.HasValue && !double.IsFinite(Score.Value))
            yield return new ValidationResult("Điểm không hợp lệ.", new[] { nameof(Score) });
    }
}
