namespace SimpleApp.Models
{
    public class FakeProductsDataSource : IProductsDataSource
    {
        public IEnumerable<Product> Products { get; }

        public FakeProductsDataSource(IEnumerable<Product> products)
        {
            Products = products;
        }
    }
}
