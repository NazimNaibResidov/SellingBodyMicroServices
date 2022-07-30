using MediatR;
using OrderService.Application.ViewModels;
using System;

namespace OrderService.Application.Features.Queries
{
    public class GetOrderDetailsQuery : IRequest<OrderDatialViewModel>
    {
        public GetOrderDetailsQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}