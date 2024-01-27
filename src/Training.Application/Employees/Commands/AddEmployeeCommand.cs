using MediatR;
using Training.Core;

namespace Training.Application;

public class AddEmployeeCommand : IRequest<Employee<Guid>>
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }

}
public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand,Employee<Guid>>
{
    private readonly IEmployeeRepository _employeeRepository;
    public AddEmployeeCommandHandler(IEmployeeRepository employeeRepository){
        _employeeRepository= employeeRepository;
    }
    public async Task<Employee<Guid>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var toAdd = new Employee<Guid>{
            Email=request.Email,
            FirstName=request.FirstName,
            LastName=request.LastName,
            BirthDate= request.BirthDate
        };
        await _employeeRepository.Add(toAdd);

        return toAdd;
    }
}

