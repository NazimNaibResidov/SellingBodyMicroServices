using AutoMapper;
using MediatR;
using OrderService.Application.Interfaces.Repostory;
using OrderService.Application.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Queries
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDatialViewModel>
    {
        private IOrderRepsotory orderRepsotory;
        private readonly IMapper mapper;

        public GetOrderDetailsQueryHandler(IOrderRepsotory orderRepsotory, IMapper mapper)
        {
            this.orderRepsotory = orderRepsotory ?? throw new NotImplementedException(nameof(orderRepsotory));
            this.mapper = mapper;
        }

        public async Task<OrderDatialViewModel> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var order = await orderRepsotory.GetByIdAsync(request.OrderId, i =>);
            var result = mapper.Map<OrderDatialViewModel>(order);
            return result;
        }
    }
}