using BasketService.Api.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasketService.Api.Interfaces
{
    public interface IBasketRepsotory
    {
        IEnumerable<string> GetUsers();

        Task<CusomterBasket> GetBasketAsync(string cusomterId);

        Task<CusomterBasket> UpdateBasketAsync(CusomterBasket cusomterBasket);

        Task<bool> DeleteBasketAsync(string id);
    }
}