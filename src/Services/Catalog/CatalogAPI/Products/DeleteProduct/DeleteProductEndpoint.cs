using CatalogAPI.Products.GetProductByCategory;

namespace CatalogAPI.Products.DeleteProduct
{
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{Id}", async(Guid Id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(Id));
                var respose = result.Adapt<DeleteProductResponse>();
                return Results.Ok(respose);
            })
            
                .WithName("DeleteProduct")
                .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product");

        }
    }
}
