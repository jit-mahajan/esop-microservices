

namespace CatalogAPI.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandHandler (IDocumentSession session): ICommandHandler<CreateProductCommand, CreateProductResult>
    {

        public async Task<CreateProductResult> Handle(CreateProductCommand commad, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Name = commad.Name,
                Category = commad.Category,
                Description = commad.Description,
                ImageFile = commad.ImageFile,
                Price = commad.Price
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}

