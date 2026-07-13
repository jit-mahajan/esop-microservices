

namespace BasketAPI.Basket.GetBasket
{
    //public record GetBasketRequest(string UserName);
    public record GetBasketRespose(ShoppingCart Cart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));
                var response = result.Adapt<GetBasketRespose>();
                return Results.Ok(response);

            })
                .WithName("GetProductById")
                .Produces<GetBasketRespose>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Product By Id.")
                .WithDescription("Get Product By Id.");

        }
    }
}
