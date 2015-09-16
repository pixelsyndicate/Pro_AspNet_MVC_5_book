using System.Collections;
using System.Collections.Generic;

namespace extensionMethodsDemo
{
    public class ShoppingCart
    {
        public List<Product> Products { get; set; }
    }

    public class ShoppingCartForProducts : IEnumerable<Product>
    {
        // has-a
        public List<Product> Products { get; set; }

        #region MUST IMPLEMENT FOR IENUMERABLE<T>
        public IEnumerator<Product> GetEnumerator()
        {
            return Products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}