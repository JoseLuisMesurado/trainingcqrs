using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Training.NG.EFCommon.BaseEntities;

namespace Training.NG.EFCommon.Repositories
{
    public interface IRepository<TEntity, M> : IBaseRepository<TEntity,M> where TEntity : class, IBaseEntity
    {
        Task<TEntity> GetById(M id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes);
        Task<T> GetById<T>(M id, Expression<Func<TEntity, T>> selectExpression);
        Task<ICollection<TEntity>> GetAllWithInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes);
    }
}
