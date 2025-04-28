using Application.Services.Orders;
using AutoMapper;
using Domain.Orders;
using OrderService.GrpcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MapProfile
{
    public class OrderMapProfile : Profile
    {
        public OrderMapProfile() 
        {
            CreateMap<ProductResponse, Product>()
                .ForMember(dest => dest.Quantity, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Guid));

            CreateMap<ProductOrderResponse, Product>()
                .ReverseMap();

            CreateMap<OrderResponse, Order>()
                .ReverseMap();
        }
    }
}
