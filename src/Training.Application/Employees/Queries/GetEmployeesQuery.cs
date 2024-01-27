using System.Linq.Expressions;
using MediatR;
using Training.Core;

namespace Training.Application;

public record GetEmployeesQuery : IRequest<ICollection<EmployeeResponse>>;
public class GetEmployeesQueryHandler: IRequestHandler<GetEmployeesQuery, ICollection<EmployeeResponse>>
{
    private readonly IEmployeeRepository _employeeRepository;
    public  GetEmployeesQueryHandler(IEmployeeRepository employeeRepository){
        _employeeRepository=employeeRepository;
    }
    public async Task<ICollection<EmployeeResponse>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var permimissioTypeList = await _employeeRepository.GetAll<EmployeeResponse>(GetEmployessSelect);
        return permimissioTypeList;
    }

    private static readonly Expression<Func<Employee<Guid>, EmployeeResponse>> GetEmployessSelect = p => new EmployeeResponse
    {
        Id = p.Id,
        Email = p.Email,
        FirstName = p.FirstName,
        LastName = p.LastName,
        BirthDate = p.BirthDate,
    };   
}
