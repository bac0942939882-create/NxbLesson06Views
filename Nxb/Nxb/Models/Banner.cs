using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nxb.Models;

[Table("Banner")]
public class Banner
{
    [Key] public int Id { get; set; }
    [Required, StringLength(150), Column(TypeName = "nvarchar(150)")]
    [Display(Name = "Tên banner")] public string Name { get; set; } = "";
    [Required, StringLength(150), Column(TypeName = "varchar(150)")]
    [Display(Name = "Ảnh")] public string Image { get; set; } = "";
    [StringLength(1000), Column(TypeName = "nvarchar(1000)")]
    [Display(Name = "Mô tả")] public string? Description { get; set; }
    [Display(Name = "Ngày tạo")] public DateTime CreatedDate { get; set; }
    [Range(0, 1), Column(TypeName = "tinyint")]
    [Display(Name = "Trạng thái")] public byte Status { get; set; } = 1;
}
