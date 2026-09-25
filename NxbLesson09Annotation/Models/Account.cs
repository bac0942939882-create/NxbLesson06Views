using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace NxbLesson09Annotation.Models;
public class Account
{
    [Key] public int Id { get; set; }
    [Display(Name = "Họ và tên"), Required(ErrorMessage = "Họ tên không được để trống")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Họ tên phải có từ 6 đến 20 ký tự")]
    public string FullName { get; set; } = "";
    [Display(Name = "Địa chỉ email"), Required(ErrorMessage = "Địa chỉ email không được để trống")]
    [EmailAddress(ErrorMessage = "Địa chỉ email không đúng định dạng")]
    public string Email { get; set; } = "";
    [Display(Name = "Số điện thoại"), Required(ErrorMessage = "Số điện thoại không được để trống")]
    [DataType(DataType.PhoneNumber)]
    [Remote(action: "VerifyPhone", controller: "Account", ErrorMessage = "Số điện thoại không đúng định dạng hoặc đã được sử dụng")]
    public string Phone { get; set; } = "";
    [Display(Name = "Địa chỉ thường trú"), Required(ErrorMessage = "Địa chỉ không được để trống")]
    [StringLength(35, ErrorMessage = "Địa chỉ không vượt quá 35 ký tự")]
    public string Address { get; set; } = "";
    [Display(Name = "Ảnh đại diện")] public string? Avatar { get; set; }
    [Display(Name = "Ngày sinh"), Required(ErrorMessage = "Ngày sinh không được để trống")]
    [DataType(DataType.Date)] public DateTime Birthday { get; set; } = DateTime.Today;
    [Display(Name = "Giới tính"), Required(ErrorMessage = "Vui lòng chọn giới tính")]
    public string Gender { get; set; } = "";
    [Display(Name = "Mật khẩu"), Required(ErrorMessage = "Mật khẩu không được để trống")]
    [DataType(DataType.Password)] public string Password { get; set; } = "";
    [Display(Name = "Link Facebook cá nhân"), Required(ErrorMessage = "Link Facebook không được để trống"), Url(ErrorMessage = "Đường dẫn Facebook không hợp lệ")]
    public string? Facebook { get; set; }
}
