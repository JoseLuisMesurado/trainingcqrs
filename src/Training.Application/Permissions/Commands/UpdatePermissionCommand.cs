using MediatR;
using Training.Core.SqlRepositories;
using Training.NG.HttpResponse;

namespace Training.Application.Permissions.Commands
{
    public record UpdatePermissionCommand : IRequest<ApiResponse>
    {
        public Guid Id { get;  set; }
        public Guid EmployeeId { get;  set; }
        public short PermissionTypeId { get;  set; }
        public DateTime GrantedExpirationDate { get; internal set; }
    }

    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, ApiResponse>
    {
        private readonly IPermissionRepository _permissionRepository;
        public UpdatePermissionCommandHandler(
            IPermissionRepository permissionRepository) { 
            
            _permissionRepository = permissionRepository;
        }

        public async Task<ApiResponse> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var toUpdate = await _permissionRepository.FindBy(request.Id);
            if (toUpdate == null)
                return null;
            toUpdate.GrantedDate = DateTime.UtcNow;
            toUpdate.EmployeeId= request.EmployeeId;
            toUpdate.GrantedExpirationDate = request.GrantedExpirationDate;
            toUpdate.PermissionTypeId = request.PermissionTypeId;
            await _permissionRepository.Update(toUpdate);

            //var current = await _permissionRepository.GetById<PermissionResponse>(toUpdate.Id, PermissionSelectExpression.DTO);
            var current = await _permissionRepository.GetById(toUpdate.Id, r => r.PermissionType);

            return new ApiResponse();
        }
    }
}
