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
    }
}