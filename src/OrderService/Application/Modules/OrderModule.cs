using Application.Services.Orders;
using Application.Services.Orders.Commands;
using Application.Services.Orders.Queries;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Modules
{
    public class OrderModule : CarterModule
    {
        public OrderModule() : base("/api/orders")
        {
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", async (Guid id, IMediator mediator) => 
            {
                var response = await mediator.Send(new GetOrderByIdQuery(id));

                return Results.Ok(response);
            })
                .WithName("GetOrderByIdAsync");

            app.MapPost("/", async (OrderRequest request, IMediator mediator) => 
            {
                var response = await mediator.Send(new CreateOrderCommand(request));

                return Results.CreatedAtRoute("GetOrderByIdAsync", new { id = response.Id }, response);
            });        
        }
    }
}
