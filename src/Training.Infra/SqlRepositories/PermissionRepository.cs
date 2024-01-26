using Training.Core.Entities;
using Training.Core.SqlRepositories;
using Training.Infra.Contexts;
using Training.NG.EFCommon.Repositories;

namespace Training.Infra.SqlRepositories
{
    public class PermissionRepository: Repository<Permission<Guid>, TrainigContext, Guid>, IPermissionRepository
    {
        public PermissionRepository(TrainigContext context) : base(context)
        {
        }
       
    }
}
