
using FluentValidation;

namespace Training.Application;

public sealed class AddEmployeeCommandValidation : AbstractValidator<AddEmployeeCommand>
{
    public AddEmployeeCommandValidation(){
        RuleFor(v => v.Email).NotNull().NotEmpty();
        RuleFor(v => v.FirstName).NotNull().NotEmpty();
        RuleFor(v=>v.LastName).NotNull().NotEmpty();
        RuleFor(v=>v.BirthDate).NotNull().NotEmpty();
    }
}
