using System.Collections.Generic;
using System.Linq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EfProductRepository : IProductRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return _context.Products; }
        }

        public Product GetProduct(int productid)
        {
            return _context.Products.FirstOrDefault(p => p.ProductID == productid);
        }

        public void SaveProduct(Product product)
        {
            // if its new, add it.
            if (product.ProductID == 0)
                _context.Products.Add(product);
            else
            {
                // check to see if it really exists
                Product dbEntry = _context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            _context.SaveChanges();
        }
    }
}