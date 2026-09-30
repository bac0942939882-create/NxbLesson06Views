using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nxb.Models;

[Table("Subjects")]
public class Subjects
{
    [Key] public int Id { get; set; }
    [Required(ErrorMessage = "Tên môn học không được để trống.")]
    [StringLength(100), Column(TypeName = "nvarchar(100)")]
    [Display(Name = "Tên môn học")] public string SubjectName { get; set; } = "";
    [ValidateNever] public ICollection<Marks> Marks { get; set; } = new List<Marks>();
}
