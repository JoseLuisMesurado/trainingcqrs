using FluentValidation;

namespace Training.Application.Permissions.Commands
{
    public sealed class AddPermissionCommandValidation : AbstractValidator<AddPermissionCommand>
    {
        public AddPermissionCommandValidation()
        {
            RuleFor(v => v.PermissionTypeId).NotNull().NotEmpty();
            RuleFor(v => v.EmployeeId).NotNull().NotEmpty();
            RuleFor(v=>v.GrantedExpirationDate).NotNull().NotEmpty();
        }
    }
}
