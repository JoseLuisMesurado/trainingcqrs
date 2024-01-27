using MediatR;
using Training.Core;
using Training.NG.HttpResponse;

namespace Training.Application;

public class UpdateEmployeeCommand : IRequest
{
    public Guid EmployeeId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}

public class UpdateEmployeeCommandHandler: IRequestHandler<UpdateEmployeeCommand>{

    private readonly IEmployeeRepository _employeeRepository;
    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository) {
        _employeeRepository= employeeRepository;
    }

    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var toUpdate = await _employeeRepository.FindBy(request.EmployeeId) ?? throw new NotFoundEntityException {
            Detail ="Employee not found"
        };

        toUpdate.Email=request.Email;
        toUpdate.FirstName= request.FirstName;
        toUpdate.LastName=request.LastName;

        await _employeeRepository.Update(toUpdate);
        
    }
}
