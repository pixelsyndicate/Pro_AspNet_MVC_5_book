using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class LinqValueCalculator : IValueCalculator
    {
        // this would normally be added via dependency injection, but i skipped that in this demo.
        readonly IDiscountHelper _discounter = new DefaultDiscountHelper();
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return _discounter.ApplyDiscount(products.Sum(p => p.Price));
            // return products.Sum(p => p.Price);
        }


    }

    public interface IValueCalculator
    {
        decimal ValueProducts(IEnumerable<Product> products);
    }
}