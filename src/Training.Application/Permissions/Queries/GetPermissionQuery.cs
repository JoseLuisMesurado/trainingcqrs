using MediatR;
using Microsoft.EntityFrameworkCore;
using Training.Core.Responses;
using Training.Core.SqlRepositories;
using Training.NG.HttpResponse;

namespace Training.Application.Permissions.Queries
{
    public record  GetPermissionQuery : IRequest<ApiResponse<ICollection<PermissionResponse>>>;

    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, ApiResponse<ICollection<PermissionResponse>>>
    {
        private readonly IPermissionRepository _permissionRepository;
        public GetPermissionQueryHandler(IPermissionRepository permissionRepository) { 
            _permissionRepository = permissionRepository;
        }
    
        public async Task<ApiResponse<ICollection<PermissionResponse>>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            var permimissionList = await _permissionRepository.GetAllWithInclude(r => r.Include(d=>d.PermissionType));
            var toReturn = permimissionList.AsQueryable().Select(PermissionSelectExpression.DTO).ToList();
            return new ApiResponse<ICollection<PermissionResponse>> { Response = toReturn };
        }
    }
}
