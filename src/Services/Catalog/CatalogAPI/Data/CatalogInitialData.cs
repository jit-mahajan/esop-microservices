using Marten.Schema;

namespace CatalogAPI.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {

            using var session = store.LightweightSession();

            if(await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetInitialProducts());

            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetInitialProducts() => new List<Product>() 
        {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Description = "Description for Product 1",
                    Price = 10.0m,
                    Category = new List<string> { "Category A", "Category B" }
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Description = "Description for Product 2",
                    Price = 20.0m,
                    Category = new List<string> { "Category A", "Category B" }
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 3",
                    Description = "Description for Product 3",
                    Price = 30.0m,
                    Category =  new List<string> { "Category A", "Category B" }
                }
            };
    }
}
