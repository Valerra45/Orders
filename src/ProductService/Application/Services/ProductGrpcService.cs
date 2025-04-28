using Domain.Products;
using Grpc.Core;
using Infrastructure;
using ProductService.GrpcService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductGrpcService : ProductService.GrpcService.ProductGrpcService.ProductGrpcServiceBase
    {
        private readonly ApplicationDbContext _context;

        public ProductGrpcService(ApplicationDbContext context)
        {
            _context = context;
        }

        public override async Task<ProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            var product = await _context.Products.FindAsync(Guid.Parse(request.Guid));

            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));
            }

            return new ProductResponse
            {
                Guid = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                Quantity = product.Quantity
            };
        }

        public override async Task<ProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            var product = new Product();

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = (decimal)request.Price;
            product.Quantity = request.Quantity;

            await _context.Products.AddAsync(product);

            return new ProductResponse
            {
                Guid = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                Quantity = product.Quantity
            };
        }

        public override async Task<UpdateStockResponse> UpdateStock(UpdateStockRequest request, ServerCallContext context)
        {
            var product = await _context.Products.FindAsync(Guid.Parse(request.Guid));

            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));
            }

            product.Quantity = request.Quantity;
            product.Update = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Russian Standard Time");

            await _context.SaveChangesAsync();

            return new UpdateStockResponse
            {
                Guid = request.Guid,
                Quantity = request.Quantity
            };
        }
    }
}
