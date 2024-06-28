using CourseManagement.Domain.Entities;
using CourseManagement.Domain.Repositories;
using CourseManagement.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CourseManagement.Infrastructure.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : class,
        IEntity<TKey>
        where TKey : IComparable
    {
        private readonly DbSet<TEntity> dbSet;
        private readonly ApplicationDbContext  _dbcontext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
            dbSet = _dbcontext.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                if(_dbcontext.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
            });
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            IQueryable<TEntity> query = dbSet;
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await dbSet.FindAsync(id);
        }

        public Task<IList<TEntity>> GetPaginateList(int pageNo = 1, int pageSize = 10, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                dbSet.Attach(entity);
                _dbcontext.Entry(entity).State = EntityState.Modified;
            });
        }

        public async Task<bool> IsDuplicate(Expression<Func<TEntity,bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            bool DuplicateExist = true;
            if(filter is not null)
            {
                DuplicateExist = await query.CountAsync(filter) > 0;
            }
            else
            {
                DuplicateExist = await query.CountAsync() > 0;
            }

            return DuplicateExist;

        }
    }
}
