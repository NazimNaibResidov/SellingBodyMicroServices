using OrderService.Domain.AggregateModel.BuyerAggregate;
using System.Threading.Tasks;

namespace OrderService.Domain.Interfaces
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        Buyer Add(Buyer buyer);

        Buyer Update(Buyer buyer);

        Task<Buyer> FindAsync(string BuyerIdentityGuid);

        Task<Buyer> FindByIdAsync(string id);
    }
}