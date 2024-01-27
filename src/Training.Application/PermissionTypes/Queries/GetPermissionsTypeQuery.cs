using MediatR;
using System.Linq.Expressions;
using Training.Core.Entities;
using Training.Core.Responses;
using Training.Core.SqlRepositories;

namespace Training.Application.PermissionTypes.Queries
{
    public record GetPermissionTypesQuery : IRequest<ICollection<PermissionTypeResponse>>;

    public class GetPermissionTypeQueryHandler : IRequestHandler<GetPermissionTypesQuery, ICollection<PermissionTypeResponse>>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        public GetPermissionTypeQueryHandler(IPermissionTypeRepository permissionTypeRepository) => _permissionTypeRepository = permissionTypeRepository;

        public async Task<ICollection<PermissionTypeResponse>> Handle(GetPermissionTypesQuery request, CancellationToken cancellationToken)
        {
            var permimissioTypeList = await _permissionTypeRepository.GetAll<PermissionTypeResponse>(GetPermissionTypeSelect);

            return  permimissioTypeList;
        }

        private static readonly Expression<Func<PermissionType<short>, PermissionTypeResponse>> GetPermissionTypeSelect = p => new PermissionTypeResponse
        {
            Id = p.Id,
            Name = p.Name
        };
    }
}
