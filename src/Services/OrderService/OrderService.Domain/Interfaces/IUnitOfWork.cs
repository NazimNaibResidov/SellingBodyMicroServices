using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Savesavechangesasync(CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> SetEntityasync(CancellationToken cancellationToken = default(CancellationToken));
    }
}