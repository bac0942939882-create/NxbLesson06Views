using System.ComponentModel.DataAnnotations;

namespace NguyenXuanBac2410900011_exam.Validation;

public sealed class NotInFutureAttribute : ValidationAttribute
{
    public NotInFutureAttribute() : base("Ngày sinh không được lớn hơn ngày hiện tại.") { }

    public override bool IsValid(object? value) =>
        value is not DateTime date || date.Date <= DateTime.Today;
}
