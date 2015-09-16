using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extensionMethodsDemo
{
    public static class MyExtensionMethods
    {
        /// <summary>
        /// This extension method operates on ShoppingCart objects only
        /// </summary>
        /// <param name="cartParm"></param>
        /// <returns>A Decimal value representing aggregate of product.Price for all products in the shoppingcart</returns>
        public static decimal TotalPrices(this ShoppingCart cartParm)
        {
            decimal total = 0;
            foreach (Product prod in cartParm.Products)
            {
                total += prod.Price;
            }
            return total;
        }

        /// <summary>
        /// This extension method operates on anything implimenting IEnumerable T (type of Product)
        /// </summary>
        /// <param name="productEnum"></param>
        /// <returns>A Decimal value representing aggregate of product.Price for all products in the shoppingcart</returns>
        public static decimal TotalPrices(this IEnumerable<Product> productEnum)
        {
            decimal total = 0;
            foreach (Product prod in productEnum)
            {
                total += prod.Price;
            }
            return total;
        }

        /// <summary>
        /// This filtering extension method operates on anything implimenting IEnumerable T (type of Product) and accepts a filter parameter
        /// </summary>
        /// <param name="productEnum"></param>
        /// <param name="categoryParm"></param>
        /// <returns>returns a IEnumerable of products</returns>
        public static IEnumerable<Product> FilterOnlyByCategory(this IEnumerable<Product> productEnum, string categoryParm)
        {
            foreach (Product prod in productEnum)
            {
                if (prod.Category == categoryParm)
                {
                    yield return prod;
                }
            }
        }

        /// <summary>
        /// This filtering extension method operates on anything implimenting IEnumerable T (type of Product) 
        /// and accepts a lamda Func filter parameter
        /// </summary>
        /// <param name="productEnum"></param>
        /// <param name="selectorParm"></param>
        /// <returns></returns>
        public static IEnumerable<Product> Filter(this IEnumerable<Product> productEnum,
            Func<Product, bool> selectorParm)
        {
            foreach (Product prod in productEnum)
            {
                if (selectorParm(prod))
                {
                    yield return prod;
                }
            }
        } 

    }
}
