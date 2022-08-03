using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces.Repostory;
using OrderService.Domain.Interfaces;
using OrderService.Domain.SeedWork;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepsotory<T> where T : BaseEntity
    {
        private readonly OrderDbContext context;

        public GenericRepository(OrderDbContext context)
        {
            this.context = context??throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork unitOfWork { get; }

        public async virtual Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual async Task<List<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                
                query = query.Include(includeProperty);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            

            if (orderBy != null)
            {
                query= orderBy(query);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<List<T>> Get(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            return await Get(filter, null, includes);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
            
        }

        public virtual async Task<T> GetByIdAsyc(Guid id, params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var includeProperty in include)
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id);

        }

        public virtual async Task<T> GetSingleAsyc(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var includeProperty in include)
            {
                query = query.Include(includeProperty);
            }
            return await query.Where(expression).SingleOrDefaultAsync();
        }

        public virtual T Update(T entity)
        {
            context.Set<T>().Update(entity);
            return entity;
        }
    }
}
