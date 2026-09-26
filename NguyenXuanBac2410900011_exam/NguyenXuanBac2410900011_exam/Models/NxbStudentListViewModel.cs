namespace NguyenXuanBac2410900011_exam.Models;
public class NxbStudentListViewModel
{
    public IReadOnlyList<NxbStudent> Students { get; init; } = [];
    public string? Search { get; init; }
    public bool? Active { get; init; }
    public int Page { get; init; }
    public int PageCount { get; init; }
    public int TotalMatches { get; init; }
    public int TotalStudents { get; init; }
    public int ActiveStudents { get; init; }
}
