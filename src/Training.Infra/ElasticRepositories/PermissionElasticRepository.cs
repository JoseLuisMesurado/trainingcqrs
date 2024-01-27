using Nest;
using Training.Core;
using Training.NG.ElasticSearchHelper;

namespace Training.Infra;

public class PermissionElasticRepository : BaseElasticIndexRepository, IPermissionElasticRepository
{
    public PermissionElasticRepository(IElasticClient client, string indexName) : base(client, indexName)
    {
    }
}
