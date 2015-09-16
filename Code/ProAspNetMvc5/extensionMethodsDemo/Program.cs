using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extensionMethodsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beginning...");

            Product demoProduct = Product.GetDemoProduct();
            Console.WriteLine("Demo Product");
            Console.WriteLine(string.Format("Category: {0}", demoProduct.Category));
            Console.WriteLine("---");

            Console.WriteLine("Demo Extension Method for ShoppingCart");
            ShoppingCart cart = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kayak", Price = 277M},
                    new Product {Name = "Lifejacket", Price = 48.95M},
                    new Product {Name = "Soccer Ball", Price = 19.50M},
                    new Product {Name = "Corner flag", Price = 34.95M},
                }
            };
            decimal cartTotal = cart.TotalPrices();
            Console.WriteLine(string.Format("cart.TotalPrices(): ${0}", cartTotal));
            Console.WriteLine("---");



            Console.WriteLine("Demo Extension Method to an Interface for IEnumerable<Product>");
            IEnumerable<Product> products = new ShoppingCartForProducts
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kayak", Category = "Watersports", Price = 277M},
                    new Product {Name = "Lifejacket",  Category = "Watersports", Price = 48.95M},
                    new Product {Name = "Soccer Ball",  Category = "Soccer", Price = 19.50M},
                    new Product {Name = "Corner flag",  Category = "Soccer", Price = 34.95M},
                }
            };
            // also test out an array of products (which should impliment ienumerable
            Product[] productArray =
            {
                new Product {Name = "Kayak", Price = 277M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer Ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M},
            };

            Console.WriteLine(string.Format("IEnumerable<product>.TotalPrices(): {0},\n\r Product[].TotalPrices(): ${1}", products.TotalPrices(), productArray.TotalPrices()));
            Console.WriteLine("---");


            Console.WriteLine("Demo Filtering Extension Methods (combined with previous)");
            decimal filteredTotal = 0;
            foreach (var prod in products.FilterOnlyByCategory("Watersports"))
            {
                filteredTotal += prod.Price;
            }
            Console.WriteLine(string.Format("Price of products.FilterByCategory(\"Watersports\").TotalPrices():\n\r  ${0}", products.FilterOnlyByCategory("Watersports").TotalPrices()));
            Console.WriteLine(string.Format("Price of products.FilterByCategory(\"Soccer\").TotalPrices():\n\r  ${0}", products.FilterOnlyByCategory("Soccer").TotalPrices()));
            Console.WriteLine(string.Format("Price of products.FilterByCategory(\"Made Up Category\").TotalPrices():\n\r  ${0}", products.FilterOnlyByCategory("Made Up Category").TotalPrices()));
            Console.WriteLine("---");



            Console.WriteLine("Demo Product Filtering Using Anon Func<> delegate with Anon Method ");

            // Func<parmType,returnType> replaces a class TYPE, because it defines what will be any anonymous method that accpets and parmType and returns a returnType

            // using an anonymous method that must return bool
            Func<Product, bool> categoryDelegateFilter = delegate(Product prod)
            {
                return prod.Category == "Soccer";
            };
            decimal delegateFilteredTotal = 0;
            foreach (var prod in products.Filter(categoryDelegateFilter))
            {
                delegateFilteredTotal += prod.Price;
            }
            Console.WriteLine(string.Format("Total: {0}", delegateFilteredTotal));
            Console.WriteLine("---");


            Console.WriteLine("Demo Product Filtering Using Lamda Func delegate");

            // could also use lamda, since you can tecnically pass in a parameter
            // the => means 'as a passed parameter'
            Func<Product, bool> categoryLamdaFilter = something => something.Category == "Watersports";

            decimal lamdaFilteredTotal = 0;
            foreach (var prod in products.Filter(categoryLamdaFilter))
            {
                lamdaFilteredTotal += prod.Price;
            }

            Console.WriteLine(string.Format("Total: {0}", lamdaFilteredTotal));
            Console.WriteLine("---");


            Console.ReadKey();
        }
    }
}
