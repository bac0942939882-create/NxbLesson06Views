using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nxb.Models;

[Table("Student")]
public class Student
{
    [Key] public int Id { get; set; }
    [Required, StringLength(100), Column(TypeName = "nvarchar(100)")]
    [Display(Name = "Họ tên")] public string StudentName { get; set; } = "";
    [Required, EmailAddress, StringLength(100), Column(TypeName = "nvarchar(100)")]
    [Display(Name = "Email")] public string StudentEmail { get; set; } = "";
    [Required, Phone, StringLength(50, MinimumLength = 9), Column(TypeName = "nvarchar(50)")]
    [Display(Name = "Số điện thoại")] public string StudentPhone { get; set; } = "";
    [Required, StringLength(150), Column(TypeName = "nvarchar(150)")]
    [Display(Name = "Địa chỉ")] public string StudentAddress { get; set; } = "";
    [Required, StringLength(100), Column(TypeName = "nvarchar(100)")]
    [Display(Name = "Ảnh đại diện")] public string StudentAvatar { get; set; } = "";
    [Required, Column(TypeName = "date"), DataType(DataType.Date)]
    [Display(Name = "Ngày sinh")] public DateTime StudentBirthday { get; set; }
    [Range(1, int.MaxValue), Display(Name = "Lớp")] public int ClassId { get; set; }
    [ValidateNever] public StdClass Class { get; set; } = null!;
    [ValidateNever] public ICollection<Marks> Marks { get; set; } = new List<Marks>();
}
