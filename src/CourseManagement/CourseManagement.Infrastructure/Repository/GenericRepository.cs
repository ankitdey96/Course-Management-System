using CourseManagement.Domain.Entities;
using CourseManagement.Domain.Repositories;
using CourseManagement.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.Query;

namespace CourseManagement.Infrastructure.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : class
        where TKey : struct
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

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
            List<string> includes = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter is not null)
                query = query.Where(filter);
            if (includes != null)
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            if (orderBy is not null)
                query = orderBy(query);


            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter, List<string> includes = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter is not null)
                query = query.Where(filter);

            if (includes is not null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IList<TEntity>> GetPaginateList(int pageNo = 1, int pageSize = 10,
            Expression<Func<TEntity, bool>> filter = null,
            List<string> includes = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter is not null)
                query = query.Where(filter);
            

            if(includes is not null)
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }

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
