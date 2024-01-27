using MediatR;
using Microsoft.EntityFrameworkCore;
using Training.Core;
using Training.Core.Responses;
using Training.Core.SqlRepositories;

namespace Training.Application.Permissions.Queries
{
    public record  GetPermissionsQuery : IRequest<ICollection<PermissionResponse>>;

    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, ICollection<PermissionResponse>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionElasticRepository _permissionElasticRepository;
        public GetPermissionsQueryHandler(IPermissionRepository permissionRepository,
                                        IPermissionElasticRepository permissionElasticRepository) { 
            _permissionRepository = permissionRepository;
            _permissionElasticRepository=permissionElasticRepository;
        }
    
        public async Task<ICollection<PermissionResponse>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permimissionList = await _permissionRepository.GetAllWithInclude(r => r.Include(d=>d.PermissionType));
            await _permissionElasticRepository.AddOrUpdateBulk(permimissionList);
            var toReturn = permimissionList.AsQueryable().Select(PermissionSelectExpression.DTO).ToList();
            return  toReturn ;
        }
    }
}
