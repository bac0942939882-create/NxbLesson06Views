using System.ComponentModel.DataAnnotations;

namespace NxbLesson10EFDbFirst.Models;

// Model sinh theo cấu trúc bảng NxbMember trong ví dụ Database First.
public partial class NxbMember
{
    public long Id { get; set; }

    [StringLength(20)]
    public string? NxbUserName { get; set; }

    [StringLength(50)]
    public string? NxbPassword { get; set; }

    [StringLength(50)]
    public string? NxbFullName { get; set; }

    [StringLength(50), EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    public string? NxbEmail { get; set; }

    [StringLength(12), Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
    public string? NxbPhone { get; set; }

    public bool? NxbStatus { get; set; }
}
