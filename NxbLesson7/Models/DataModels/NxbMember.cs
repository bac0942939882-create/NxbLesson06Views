using System.ComponentModel.DataAnnotations;

namespace NxbLesson7.Models.DataModels;

public class NxbMember
{
    [Required(ErrorMessage = "The NxbMemberId field is required.")]
    public string NxbMemberId { get; set; } = string.Empty;

    [Required(ErrorMessage = "The NxbUserName field is required.")]
    public string NxbUserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "The NxbPassword field is required.")]
    public string NxbPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "The NxbFullName field is required.")]
    public string NxbFullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "The NxbEmail field is required.")]
    [EmailAddress(ErrorMessage = "The NxbEmail field is not a valid e-mail address.")]
    public string NxbEmail { get; set; } = string.Empty;
}
