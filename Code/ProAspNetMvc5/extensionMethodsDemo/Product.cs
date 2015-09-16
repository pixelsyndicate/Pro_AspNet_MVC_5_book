﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extensionMethodsDemo
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public static Product GetDemoProduct()
        {
            return new Product
            {
                ProductID = 100, Name = "Kayak", Description = "A boat for one person",
                Price = 275M, Category = "Watersports"
            };
        }

    }
}
