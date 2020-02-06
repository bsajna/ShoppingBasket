using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Discounts
{
    /// <summary>
    /// Specifies discount that can be applied for a group of products consisting of two butters and one bread.
    /// </summary>
    public class TwoButtersBreadDiscount : IDiscount
    {
        public string Name => "Buy 2 butters and get one bread at 50% off";


        /// <summary>
        /// Returns the total amount of discount that can be applied to the specified products, and the list of products that enabled the discount to be applied if it can be applied.
        /// For every group of products consisting of two butters and one bread, discount amount is incremented by 50% of the price of bread in the corresponding group.
        /// All such butter and bread products are returned as well. 
        /// </summary>
        /// <param name="products">Products for which to calculate the discount.</param>
        /// <returns></returns>
        public (IEnumerable<Product> Products, decimal DiscountAmount) GetDiscountAmount(IEnumerable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            var amount = 0m;
            var affectedProducts = new List<Product>();

            // Get all breads...
            var breads = products.Where(p => p.Type == ProductType.Bread);
            // Get all butters... Make it array so we can index into it easily
            var butters = products.Where(p => p.Type == ProductType.Butter).ToArray();

            var i = 0;
            // Now for each bread...
            foreach (var bread in breads)
            {
                // check if we have two more butters...
                if (i + 1 < butters.Length)
                {
                    // and if we do, add 50% of bread price to the current amount
                    amount += bread.Price * 0.5m;
                    // also add the two butters and bread to the current affected products list
                    affectedProducts.Add(butters[i++]);
                    affectedProducts.Add(butters[i++]);
                    affectedProducts.Add(bread);
                }
            }
            // we're done here, return the results
            return (affectedProducts, amount);
        }
    }
}
