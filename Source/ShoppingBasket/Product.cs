using System;

namespace ShoppingBasket
{
    /// <summary>
    /// Represents a product that can be aded to a shopping cart.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Product type.
        /// </summary>
        public ProductType Type { get; private set; }
        /// <summary>
        /// Some descriptive name of the product.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; private set; }

        public Product(ProductType type, string name, decimal price)
        {
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price));
            }

            Type = type;
            Name = name;
            Price = price;
        }
    }
}
