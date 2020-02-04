using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingBasket.Default
{
    public class Basket : IBasket
    {
        private readonly List<Product> products;

        public Basket()
        {
            products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            products.Add(product);
        }

        public decimal GetTotal()
        {
            return products.Sum(p => p.Price);
        }
    }
}
