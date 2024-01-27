using FluentValidation;

namespace Training.Application;

public sealed class UpdateEmployeeCommandValidation : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidation(){
        RuleFor(v => v.EmployeeId).NotNull().NotEmpty();
        RuleFor(v => v.Email).NotNull().NotEmpty();
        RuleFor(v=>v.FirstName).NotNull().NotEmpty();
        RuleFor(v=>v.LastName).NotNull().NotEmpty();
        RuleFor(v=>v.BirthDate).NotNull().NotEmpty();
    }
}
