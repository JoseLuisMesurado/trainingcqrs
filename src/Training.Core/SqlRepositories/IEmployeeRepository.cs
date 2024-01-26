using Training.NG.EFCommon.Repositories;

namespace Training.Core;

public interface IEmployeeRepository : IRepository<Employee<Guid>, Guid>
{

}
