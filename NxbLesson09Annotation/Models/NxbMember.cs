namespace NxbLesson09Annotation.Models;
public class NxbMember
{
    public int NxbMemberId { get; set; }
    public string NxbMemberName { get; set; } = "";
    public string NxbPassword { get; set; } = "";
    public string NxbEmail { get; set; } = "";
    public string NxbPhoneNumber { get; set; } = "";
    public string NxbFullName { get; set; } = "";
    public DateTime NxbBirthday { get; set; }
}
