﻿using CourseManagement.Domain.Entities;
using System.Linq.Expressions;

namespace CourseManagement.Domain.Repositories
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity : class where Tkey:struct
    {
        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(Tkey Id);

        Task DeleteAsync(TEntity entity);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
            List<string> includes = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter, List<string> includes = null);

        Task<bool> IsDuplicate(Expression<Func<TEntity, bool>> filter = null);

        Task<IList<TEntity>> GetPaginateList(int pageNo = 1, int pageSize = 10, Expression<Func<TEntity, bool>> filter = null,
           List<string> includes = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
    }
}
