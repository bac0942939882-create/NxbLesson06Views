using System.ComponentModel.DataAnnotations;
namespace NxbLesson09Annotation.Models;
public class NxbMemberRegister
{
    public int NxbMemberId { get; set; }
    [Display(Name = "Tên đăng nhập")]
    [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải có từ 3 đến 20 ký tự")]
    public string NxbUserName { get; set; } = "";
    [Display(Name = "Mật khẩu")]
    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [DataType(DataType.Password)]
    public string NxbPassword { get; set; } = "";
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    public string NxbEmail { get; set; } = "";
    [Display(Name = "Số điện thoại")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải gồm 10 chữ số và bắt đầu bằng 0")]
    public string? NxbPhoneNumber { get; set; }
    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Họ và tên không được để trống")]
    public string NxbFullName { get; set; } = "";
    [Display(Name = "Ngày sinh")]
    [DataType(DataType.Date)]
    public DateTime NxbBirthday { get; set; } = DateTime.Today;
}
