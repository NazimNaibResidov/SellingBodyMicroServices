using OrderService.Domain.Interfaces;
using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderService.Application.Interfaces.Repostory
{
    public interface IGenericRepsotory<T> : IRepository<T> where T : BaseEntity
    {
        Task<int> AddAsync(T entity);

        int Add(T entity);

        int Add(IEnumerable<T> entity);

        Task<int> AddAsync(IEnumerable<T> entity);

        Task<int> UpdateAsync(T entity);

        int Update(T entity);

        Task<int> DeleteAsync(T entity);

        int Delete(T entity);

        Task<int> DeleteAsync(Guid Id);

        int Delete(Guid id);

        bool DeleteRange(Expression<Func<T, bool>> peridicate);

        Task<bool> DeleteRangeAsync(Expression<Func<T, bool>> peridicate);

        bool AddOrUpdate(T entity);

        Task<bool> AddOrUpdateAsync(T entity);

        IQueryable<T> AsQueryable();

        Task<List<T>> GetAll(bool noTracking = true);

        Task<List<T>> GetList(Expression<Func<T, bool>> peridicate, bool noTracking = true, IOrderedQueryable<T> order = null, params Expression<Func<T, object>>[] includes);

        Task<T> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<T, object>>[] includes);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> peridicate, bool noTracking = true, params Expression<Func<T, object>>[] includes);

        IQueryable<T> Get(Expression<Func<T, bool>> peridicate, bool noTracking = true, params Expression<Func<T, object>>[] includes);
    }
}