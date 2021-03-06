﻿using System.Collections.Generic;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        Product GetProduct(int productId);

        void SaveProduct(Product product);

        Product DeleteProduct(int productId);
    }
}
