using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{
    public record DeleteOrdreResponse(bool IsSuccess);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.Map("/order/{id}", async (Guid Id, ISender sender) =>

           {
               var result = await sender.Send(new DeleteOrderCommand(Id));

               var response = result.Adapt<DeleteOrdreResponse>();

               return Results.Ok(response);
           }
           )
                .WithName("DeleteOrder")
                .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Order")
                .WithDescription("Deltee Order");
        }
    }
}
