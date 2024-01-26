using MediatR;
using Training.Core.Entities;
using Training.Core.SqlRepositories;
using Training.NG.HttpResponse;

namespace Training.Application.Permissions.Commands
{
    public record AddPermissionCommand : IRequest<ApiResponse>
    {
        public Guid EmployeeId { get; set; }
        public short PermissionTypeId { get; set; }
        public DateTime GrantedExpirationDate { get; internal set; }
    }

    public class AddPermissionCommandHandler : IRequestHandler<AddPermissionCommand, ApiResponse>
    {
        private readonly IPermissionRepository _permissionRepository;
        public AddPermissionCommandHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
       
        public async Task<ApiResponse> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
        {
            var toAdd = new Permission<Guid>
            {
                GrantedDate = DateTime.UtcNow,
                EmployeeId=request.EmployeeId,
                GrantedExpirationDate= request.GrantedExpirationDate,
                PermissionTypeId = request.PermissionTypeId
            };
            try
            {
                await _permissionRepository.Add(toAdd);
                //var current = await _permissionRepository.GetById<PermissionResponse>(toAdd.Id, PermisionSelectExpresion.SelectExpression);
                var current = await _permissionRepository.GetById(toAdd.Id, r => r.PermissionType);//mapeo completo
                return new ApiResponse();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        

    }
}
