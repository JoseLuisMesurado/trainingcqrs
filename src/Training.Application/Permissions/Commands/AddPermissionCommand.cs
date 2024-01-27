using MediatR;
using Training.Core;
using Training.Core.Entities;
using Training.Core.SqlRepositories;

namespace Training.Application.Permissions.Commands
{
    public record AddPermissionCommand : IRequest<Permission<Guid>>
    {
        public Guid EmployeeId { get; set; }
        public short PermissionTypeId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

    public class AddPermissionCommandHandler : IRequestHandler<AddPermissionCommand, Permission<Guid>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionElasticRepository _permissionElasticRepository;
        public AddPermissionCommandHandler(IPermissionRepository permissionRepository,
                                            IPermissionElasticRepository permissionElasticRepository)
        {
            _permissionRepository = permissionRepository;
            _permissionElasticRepository= permissionElasticRepository;
        }
       
        public async Task<Permission<Guid>> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
        {
            var toAdd = new Permission<Guid>
            {
                GrantedDate = DateTime.UtcNow,
                EmployeeId=request.EmployeeId,
                ExpirationDate= request.ExpirationDate,
                PermissionTypeId = request.PermissionTypeId
            };
        
                await _permissionRepository.Add(toAdd);
                await _permissionElasticRepository.AddOrUpdate(toAdd);
                return toAdd;
            }

    }
}
