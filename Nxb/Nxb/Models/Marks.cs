using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nxb.Models;

[Table("Marks")]
public class Marks
{
    [Range(1, int.MaxValue), Display(Name = "Môn học")] public int SubjectId { get; set; }
    [Range(1, int.MaxValue), Display(Name = "Sinh viên")] public int StudentId { get; set; }
    [Range(0d, 10d, ErrorMessage = "Điểm phải nằm trong khoảng từ 0 đến 10.")]
    [Column(TypeName = "float"), Display(Name = "Điểm")] public double Score { get; set; }
    [ValidateNever] public Subjects Subject { get; set; } = null!;
    [ValidateNever] public Student Student { get; set; } = null!;
}
