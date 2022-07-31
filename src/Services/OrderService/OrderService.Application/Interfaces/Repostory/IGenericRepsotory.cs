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
       

        Task<List<T>> GetAll(bool noTracking = true);

        Task<List<T>> GetList(Expression<Func<T, bool>> peridicate, bool noTracking = true, IOrderedQueryable<T> order = null, params Expression<Func<T, object>>[] includes);

        Task<T> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<T, object>>[] includes);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> peridicate, bool noTracking = true, params Expression<Func<T, object>>[] includes);

        IQueryable<T> Get(Expression<Func<T, bool>> peridicate, bool noTracking = true, params Expression<Func<T, object>>[] includes);
    }
}