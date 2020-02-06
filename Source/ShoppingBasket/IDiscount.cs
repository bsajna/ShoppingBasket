using System.Collections.Generic;

namespace ShoppingBasket
{
    /// <summary>
    /// Interface for a discount.
    /// </summary>
    public interface IDiscount
    {
        /// <summary>
        /// Some descriptive name of the discount.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns the total amount of discount that can be applied to the specified products, and the list of products that enabled the discount to be applied if it can be applied.
        /// </summary>
        /// <param name="products">Products for which to calculate the discount.</param>
        /// <returns></returns>
        (IEnumerable<Product> Products, decimal DiscountAmount) GetDiscountAmount(IEnumerable<Product> products);
    }
}
