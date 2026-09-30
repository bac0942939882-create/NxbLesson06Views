using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenXuanBac2410900011_exam.Models;

// Bảng riêng đáp ứng mục 1 của đề; CRUD sinh viên dùng NxbStudent.
[Table("NxbEmployee")]
public class NxbEmployee
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string NxbName { get; set; } = string.Empty;
    [Required, StringLength(10)]
    public string NxbGender { get; set; } = "Nam";
    [Column(TypeName = "date")]
    public DateTime NxbBirthDay { get; set; }
    [Required, StringLength(254)]
    public string NxbEmail { get; set; } = string.Empty;
    [Required, StringLength(20)]
    public string NxbPhone { get; set; } = string.Empty;
    public bool NxbActive { get; set; } = true;
}
