using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Training.NG.EFCommon.BaseEntities;

namespace Training.NG.EFCommon.Repositories
{
    public class Repository<TEntity, TContext,S> : BaseRepository<TEntity, TContext,S>, IRepository<TEntity,S>
       where TEntity : class, IEntityPK<S>
       where TContext : DbContext
    {
        private readonly TContext _context;

        public Repository(TContext context) : base(context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> GetById(S id
            , Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();
            if(includes != null)
               query = includes(query);

            return await query.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.DeletedDate == null);
        }

        public async Task<T> GetById<T>(S id, Expression<Func<TEntity, T>> selectExpression)
        {
            IQueryable<T> query = _context.Set<TEntity>().AsNoTracking().Where(x => x.Id.Equals(id) && x.DeletedDate == null).Select(selectExpression);
            return await query.FirstOrDefaultAsync<T>();
        }

        public async Task<ICollection<TEntity>> GetAllWithInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();
            if(includes != null)
               query = includes(query);

            return await query.ToListAsync<TEntity>();
        }
    }
}
