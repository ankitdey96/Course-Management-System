using CourseManagement.Domain.Entities;
using CourseManagement.Domain.Repositories;
using CourseManagement.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

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

        public virtual async Task RemoveAsync(TKey id)
        {
            var entityToDelete = dbSet.Find(id);
            await DeleteAsync(entityToDelete);
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

        public async Task<IList<TEntity>> GetPaginateList(int pageNo = 1, int pageSize = 10,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> includes = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter is not null)
                query = query.Where(filter);
            

            if(includes is not null)
                query = includes(query);

            IList<TEntity> data;

            if (orderBy is not null)
               query = orderBy(query);

            var Result = query.Skip((pageNo-1) * pageSize).Take(pageSize);
            data = await Result.AsNoTracking().ToListAsync();


            return data;


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
