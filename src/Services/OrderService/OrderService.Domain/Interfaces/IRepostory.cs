namespace OrderService.Domain.Interfaces
{
    public interface IRepository<T>
    {
        IUnitOfWork unitOfWork { get; }
    }
}