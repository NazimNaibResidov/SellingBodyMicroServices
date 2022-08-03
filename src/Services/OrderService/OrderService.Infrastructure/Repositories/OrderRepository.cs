using OrderService.Domain.AggregateModel.OrderAggreage;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.Context;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IBuyerRepository
    {
        private readonly OrderDbContext context;

        public OrderRepository(OrderDbContext context):base(context)
        {
            this.context = context;
        }

        public override async Task<Order> GetByIdAsyc(Guid id, params Expression<Func<Order, object>>[] include)
        {
           var query=await base.GetByIdAsyc(id, include);
            if (query==null)
            {
                query=context.Orders.Local.FirstOrDefault(x => x.Id == id);
            }
            return query;
        }
    }
}
