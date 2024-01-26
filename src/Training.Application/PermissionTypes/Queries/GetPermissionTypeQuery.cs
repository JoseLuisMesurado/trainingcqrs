using MediatR;
using System.Linq.Expressions;
using Training.Core.Entities;
using Training.Core.Responses;
using Training.Core.SqlRepositories;
using Training.NG.HttpResponse;

namespace Training.Application.PermissionTypes.Queries
{
    public record GetPermissionTypeQuery : IRequest<ApiResponse<ICollection<PermissionTypeResponse>>>;

    public class GetPermissionTypeQueryHandler : IRequestHandler<GetPermissionTypeQuery, ApiResponse<ICollection<PermissionTypeResponse>>>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        public GetPermissionTypeQueryHandler(IPermissionTypeRepository permissionTypeRepository) => _permissionTypeRepository = permissionTypeRepository;

        public async Task<ApiResponse<ICollection<PermissionTypeResponse>>> Handle(GetPermissionTypeQuery request, CancellationToken cancellationToken)
        {
            var permimissioTypeList = await _permissionTypeRepository.GetAll<PermissionTypeResponse>(GetPermissionTypeSelect);

            return new ApiResponse<ICollection<PermissionTypeResponse>> { Response = permimissioTypeList };
        }

        private static readonly Expression<Func<PermissionType<short>, PermissionTypeResponse>> GetPermissionTypeSelect = p => new PermissionTypeResponse
        {
            Id = p.Id,
            Name = p.Name
        };
    }
}
