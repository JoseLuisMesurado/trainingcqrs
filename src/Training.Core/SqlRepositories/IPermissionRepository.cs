
using Training.Core.Entities;
using Training.NG.EFCommon.Repositories;
namespace Training.Core.SqlRepositories
{
    public interface IPermissionRepository : IRepository<Permission<Guid>, Guid>
    {
        
    }
}
