using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.API.Endpoints
{

    public record GerOrderByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrderByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByNameQuery(orderName));

                var response = result.Adapt<GerOrderByNameResponse>();

                return Results.Ok(response);

            })
                 .WithName("GetOrderByName")
                .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                 .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Orders By Name")
                .WithDescription("Get Orders By Name");
        }
    }
}
