using OrderService.Domain.AggregateModel.BuyerAggregate;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.Context;

namespace OrderService.Infrastructure.Repositories
{
    public class BuyerRepository : GenericRepository<Buyer>, IBuyerRepository
    {
        public BuyerRepository(OrderDbContext context) : base(context)
        {
        }

      
    }
}
