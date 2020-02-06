using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Discounts
{
    public class ThreeMilksOneExtraMilkForFreeDiscount : IDiscount
    {
        /// <summary>
        /// Returns the total amount of discount that can be applied to the specified products, and the list of products that enabled the discount to be applied if it can be applied.
        /// For every group of products consisting of four milks, discount amount is incremented by the full price of one milk in the corresponding group.
        /// All such milk products are returned as well. 
        /// </summary>
        /// <param name="products">Products for which to calculate the discount.</param>
        /// <returns></returns>
        public (IEnumerable<Product> Products, decimal DiscountAmount) GetDiscountAmount(IEnumerable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            // Get all milks...
            var milks = products.Where(p => p.Type == ProductType.Milk);

            // Divide their count by 4 (int division) to see how many milks for free, multiply that by the price of one milk and we're done
            var freeMilks = milks.Count() / 4;

            return (milks.Take(freeMilks * 4), freeMilks != 0 ? freeMilks * milks.First().Price : 0m);
        }
    }
}
