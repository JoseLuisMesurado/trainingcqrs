using Nest;

namespace Training.NG.ElasticSearchHelper
{
    public  interface IBaseElasticIndexRepository
    {
        Task<bool> AddOrUpdateBulk<T>(IEnumerable<T> documents) where T : class;
        Task<bool> AddOrUpdate<T>(T document) where T : class;
        Task<T> Get<T>(string key) where T : class;
        Task<List<T>?> GetAll<T>() where T : class;
        Task<List<T>?> Query<T>(QueryContainer predicate) where T : class;
        Task<bool> Remove<T>(string key) where T : class;
        Task<long> RemoveAll<T>() where T : class;
    }
}
