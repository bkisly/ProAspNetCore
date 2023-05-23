namespace SimpleApp.Models
{
    public interface IProductsDataSource
    {
        public IEnumerable<Product> Products { get; }
    }
}
