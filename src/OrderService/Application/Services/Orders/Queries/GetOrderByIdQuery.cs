using AutoMapper;
using Domain.Abstractions;
using Domain.Orders;
using MediatR;
using OrderService.GrpcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderResponse>
    {
        public Guid Id { get; }

        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Order> _orderRepository;

        public GetOrderByIdQueryHandler(IMapper mapper,
            IEfRepository<Order> orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            return _mapper.Map<OrderResponse>(order);
        }
    }
}
