using CourseManagement.Domain.Entities;
using System.Linq.Expressions;

namespace CourseManagement.Domain.Repositories
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity : class,IEntity<Tkey> where Tkey:IComparable
    {
        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(Tkey id);

        Task<IList<TEntity>> GetPaginateList(int pageNo = 1, int pageSize = 10,Expression<Func<TEntity, bool>> filter = null,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,params Expression<Func<TEntity, object>>[] includes);
    }
}
