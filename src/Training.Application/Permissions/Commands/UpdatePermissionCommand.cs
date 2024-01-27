using MediatR;
using Training.Core;
using Training.Core.SqlRepositories;
using Training.NG.HttpResponse;

namespace Training.Application.Permissions.Commands
{
    public record UpdatePermissionCommand : IRequest
    {
        public Guid Id { get;  set; }
        public Guid EmployeeId { get;  set; }
        public short PermissionTypeId { get;  set; }
        public DateTime ExpirationDate { get; set; }
    }

    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionElasticRepository _permissionElasticRepository;
        public UpdatePermissionCommandHandler( IPermissionRepository permissionRepository,
                                                IPermissionElasticRepository permissionElasticRepository) { 
            _permissionRepository = permissionRepository;
            _permissionElasticRepository= permissionElasticRepository;
        }

        public async Task Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var toUpdate = await _permissionRepository.FindBy(request.Id) ?? throw new NotFoundEntityException {
                                                                                Detail ="Permission not found"
                                                                            };
            
            toUpdate.GrantedDate = DateTime.UtcNow;
            toUpdate.EmployeeId= request.EmployeeId;
            toUpdate.ExpirationDate = request.ExpirationDate;
            toUpdate.PermissionTypeId = request.PermissionTypeId;
            
            await _permissionRepository.Update(toUpdate);
            await _permissionElasticRepository.AddOrUpdate(toUpdate);
            // //var current = await _permissionRepository.GetById<PermissionResponse>(toUpdate.Id, PermissionSelectExpression.DTO);
            // var current = await _permissionRepository.GetById(toUpdate.Id, r => r.PermissionType);
            
        }
    }
}
