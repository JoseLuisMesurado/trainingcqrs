using System.Linq.Expressions;
using MediatR;
using Training.Core;
using Training.Core.Entities;
using Training.Core.SqlRepositories;

namespace Training.Application;

public record GetEmployeePermissionsQuery : IRequest<ICollection<EmployeePermissionsReponse>>
{
    public Guid EmployeeId { get; set; }
}

public class GetEmployeePermissionsQueryHander : IRequestHandler<GetEmployeePermissionsQuery, ICollection<EmployeePermissionsReponse>>
{
    private readonly IPermissionRepository _permissionRepository;

    public GetEmployeePermissionsQueryHander(IPermissionRepository permissionRepository) => _permissionRepository = permissionRepository;

    public async Task<ICollection<EmployeePermissionsReponse>> Handle(GetEmployeePermissionsQuery request, CancellationToken cancellationToken)
    {
        var permimissioTypeList = await _permissionRepository.Get<EmployeePermissionsReponse>(x=>x.EmployeeId.Equals(request.EmployeeId),
         GetPermissionTypeSelect);

        return permimissioTypeList;
    }
    private static readonly Expression<Func<Permission<Guid>, EmployeePermissionsReponse>> GetPermissionTypeSelect = p => new EmployeePermissionsReponse
    {
        Id = p.Id,
        EmployeeId = p.EmployeeId,
        GrantedDate = p.GrantedDate,
        ExpirationDate = p.ExpirationDate,
    };

}
