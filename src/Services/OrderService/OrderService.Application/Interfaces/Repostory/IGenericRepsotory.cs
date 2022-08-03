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

        Task<List<T>> GetAll();
        Task<List<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> Get(Expression<Func<T, bool>> predicate = null,params Expression<Func<T, object>>[] orderBy );
        Task<T> GetById(Guid id);
        Task<T> GetByIdAsyc(Guid id,params Expression<Func<T, object>>[] include);
        Task<T> GetSingleAsyc(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] include);
        Task<T> AddAsync(T entity);
        T Update(T entity);
       

    }
}