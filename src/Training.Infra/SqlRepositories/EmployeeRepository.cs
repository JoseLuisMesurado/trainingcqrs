using Training.Core;
using Training.Infra.Contexts;
using Training.NG.EFCommon.Repositories;

namespace Training.Infra;

public class EmployeeRepository : Repository<Employee<Guid>, TrainigContext, Guid>, IEmployeeRepository
{
    public EmployeeRepository(TrainigContext context) : base(context)
    {
    }
}
