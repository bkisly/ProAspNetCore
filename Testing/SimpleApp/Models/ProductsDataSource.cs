namespace SimpleApp.Models
{
    public class ProductsDataSource : IProductsDataSource
    {
        public IEnumerable<Product> Products => new[]
        {
            new Product { Name = "Kayak", Price = 275M },
            new Product { Name = "Lifejacket", Price = 48.95M }
        };
    }
}
