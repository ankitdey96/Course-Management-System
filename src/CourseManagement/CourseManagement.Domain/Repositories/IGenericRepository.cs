﻿using CourseManagement.Domain.Entities;
using System.Linq.Expressions;

namespace CourseManagement.Domain.Repositories
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity : class,IEntity<Tkey> where Tkey:IComparable
    {
        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(Tkey Id);

        Task DeleteAsync(TEntity entity);

        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(Tkey id);

        Task<bool> IsDuplicate(Expression<Func<TEntity, bool>> filter = null);

        Task<IList<TEntity>> GetPaginateList(int pageNo = 1, int pageSize = 10, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> includes = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
    }
}
