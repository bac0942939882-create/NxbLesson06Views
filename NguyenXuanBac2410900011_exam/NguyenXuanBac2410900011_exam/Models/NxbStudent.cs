using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NguyenXuanBac2410900011_exam.Validation;

namespace NguyenXuanBac2410900011_exam.Models;

[Table("NxbStudent")]
public class NxbStudent
{
    [Key]
    [Display(Name = "Mã bản ghi")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Họ và tên cần từ 2 đến 100 ký tự.")]
    [Display(Name = "Họ và tên")]
    public string NxbName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
    [StringLength(10)]
    [RegularExpression("^(Nam|Nữ|Khác)$", ErrorMessage = "Giới tính phải là Nam, Nữ hoặc Khác.")]
    [Display(Name = "Giới tính")]
    public string NxbGender { get; set; } = "Nam";

    [Required(ErrorMessage = "Vui lòng nhập ngày sinh.")]
    [Column(TypeName = "date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
    [NotInFuture]
    [Display(Name = "Ngày sinh")]
    public DateTime NxbBirthDay { get; set; } = DateTime.Today.AddYears(-18);

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [StringLength(254, ErrorMessage = "Email không được vượt quá 254 ký tự.")]
    [EmailAddress(ErrorMessage = "Địa chỉ email chưa đúng định dạng.")]
    [Display(Name = "Email")]
    public string NxbEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [StringLength(20)]
    [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Số điện thoại gồm 10 chữ số và bắt đầu bằng 0.")]
    [Display(Name = "Số điện thoại")]
    public string NxbPhone { get; set; } = string.Empty;

    [Display(Name = "Đang hoạt động")]
    public bool NxbActive { get; set; } = true;
}
