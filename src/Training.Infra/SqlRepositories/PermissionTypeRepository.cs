using Training.Core.Entities;
using Training.Core.SqlRepositories;
using Training.Infra.Contexts;
using Training.NG.EFCommon.Repositories;

namespace Training.Infra.SqlRepositories
{
    public class PermissionTypeRepository : Repository<PermissionType<short>, TrainigContext, short>, IPermissionTypeRepository
    {
        public PermissionTypeRepository(TrainigContext context) : base(context)
        {
        }
    }
}
