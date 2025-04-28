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

namespace Application.Services.Orders.Commands
{
    public class CreateOrderCommand : IRequest<OrderResponse>
    {
        public OrderRequest Order { get; }

        public CreateOrderCommand(OrderRequest order)
        {
            Order = order;
        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Order> _orderRepository;
        private readonly ProductGrpcService.ProductGrpcServiceClient _grpcClient;

        public CreateOrderCommandHandler(IMapper mapper,
            IEfRepository<Order> orderRepository,
            ProductGrpcService.ProductGrpcServiceClient grpcClient)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _grpcClient = grpcClient;
        }

        public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order();

            foreach (var productRequest in request.Order.Products)
            {
                var productResponse = await _grpcClient.GetProductAsync(new GetProductRequest
                {
                    Guid = productRequest.ProductId.ToString()
                });

                var product = _mapper.Map<Product>(productResponse);

                product.Quantity = productRequest.Quantity;

                order.Products.Add(product);

                var updateResponse = await _grpcClient.UpdateStockAsync(new UpdateStockRequest 
                { 
                    Guid = product.ProductId.ToString(),
                    Quantity = productResponse.Quantity - product.Quantity
                });
            }

            await _orderRepository.AddAsync(order);

            return _mapper.Map<OrderResponse>(order);
        }
    }
}
