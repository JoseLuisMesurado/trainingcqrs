using FluentValidation;

namespace Training.Application.Permissions.Commands
{
    public sealed class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionCommandValidator()
        {
            RuleFor(v => v.Id).NotNull().NotEmpty();
            RuleFor(v => v.EmployeeId).NotNull().NotEmpty();
            RuleFor(v => v.PermissionTypeId).NotNull().NotEmpty();
            RuleFor(v => v.ExpirationDate).NotNull().NotEmpty();
        }
    }
}
