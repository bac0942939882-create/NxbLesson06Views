using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nxb.Models;

[Table("StdClass")]
public class StdClass
{
    [Key] public int Id { get; set; }
    [Required(ErrorMessage = "Tên lớp không được để trống.")]
    [StringLength(100), Column(TypeName = "nvarchar(100)")]
    [Display(Name = "Tên lớp")] public string ClassName { get; set; } = "";
    [ValidateNever] public ICollection<Student> Students { get; set; } = new List<Student>();
}
