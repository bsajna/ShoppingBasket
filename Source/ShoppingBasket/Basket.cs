using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket
{
    /// <summary>
    /// Default implementation of shopping basket.
    /// </summary>
    public class Basket : IBasket
    {
        private readonly List<Product> products;
        private readonly IEnumerable<IDiscount> discounts;

        public Basket(IEnumerable<IDiscount> discounts)
        {
            if (discounts == null)
            {
                throw new ArgumentNullException(nameof(discounts));
            }

            products = new List<Product>();
            // Create a copy of passed discount list, it should be read-only, at least I would expect that behaviour
            this.discounts = new List<IDiscount>(discounts);
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (products.Contains(product))
            {
                throw new ArgumentException("Specified product instance is already in the basket", nameof(product));
            }

            products.Add(product);
        }

        // Calculate total: Sum all product prices and then subtract the sum of discounts from it.
        // For applying discounts idea is this: Consecutively apply all discounts specified when creating basket, but a set of items can only be affected by one discount.
        // Otherwise this problem occur: lets say we have two discounts, one is applied to two breads, the other is applied to three breads and we
        // have five breads in the basket - do we apply the first discount on 4 breads and the second on 3 already discounted breads or....? We'd end up with various possible discounted
        // prices and then we'd have to decide which price to pick? This could be solved, but it would introduce complexity, for this code sample we'll keep things relatively simple,
        // hence one discount per same set of items rule
        public decimal GetTotal()
        {
            Log.Information("Calculating shopping basket total...");
            Log.Information("Items in basket: {0}", products.GroupBy(p => p.Type).Select(x => $"{x.Count()} x {x.First().Name}"));
            // Sum the products...
            var productsSum = products.Sum(p => p.Price);
            IEnumerable<Product> productsForDiscount = products;
            var totalDiscount = 0m;
            // Apply each discount...
            foreach (var discount in discounts)
            {
                Log.Information("Applying discount '{0}'....", discount.Name);
                var (Products, DiscountAmount) = discount.GetDiscountAmount(productsForDiscount);

                if (DiscountAmount != 0m)
                {
                    Log.Information("{0} itmes in shopping basket result in price reduced by {1}", Products.Count(), DiscountAmount);
                }
                else
                {
                    Log.Information("Discount cannot be applied to items in the basket");
                }
                // Now remove all affected products from the list so we don't potentially apply more discounts on them
                productsForDiscount = productsForDiscount.Except(Products);
                totalDiscount += DiscountAmount;
            }

            var total = Math.Max(0m, productsSum - totalDiscount);

            Log.Information("Total price of the products in the basket: {0}", productsSum);
            Log.Information("Total discount amount: {0}", totalDiscount);
            Log.Information("Total after discount: {0}", total);

            return total;
        }
    }
}
