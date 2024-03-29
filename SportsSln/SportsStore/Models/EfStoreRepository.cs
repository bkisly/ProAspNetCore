﻿namespace SportsStore.Models
{
    public class EfStoreRepository : IStoreRepository
    {
        private readonly StoreDbContext _context;

        public EfStoreRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;

        public void SaveProduct(Product p)
        {
            _context.SaveChanges();
        }

        public void CreateProduct(Product p)
        {
            _context.Add(p);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product p)
        {
            _context.Remove(p);
            _context.SaveChanges();
        }
    }
}
