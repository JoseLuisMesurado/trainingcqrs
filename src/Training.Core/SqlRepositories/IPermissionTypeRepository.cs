using Training.Core.Entities;
using Training.NG.EFCommon.Repositories;

namespace Training.Core.SqlRepositories
{
    public interface IPermissionTypeRepository : IRepository<PermissionType<short>, short>
    {
    }
}
