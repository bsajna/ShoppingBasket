using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket
{
    /// <summary>
    /// Interface for a shopping basket.
    /// </summary>
    public interface IBasket
    {
        /// <summary>
        /// Adds product to the shopping basket.
        /// </summary>
        /// <param name="product"></param>
        void AddProduct(Product product);

        /// <summary>
        /// Gets the current total price of all the products in the basket, including various discounts.
        /// </summary>
        /// <returns></returns>
        decimal GetTotal();

        // There could be various other methods here, like RemoveProduct, GetProducts for retreiving the list of products that are currently in the basket, GetDiscount for getting
        // just the value of a discount that applies to the current items in the basket, etc. 
        // These are not defined here, however, for the sake of simplicity of this code assignment
    }
}
