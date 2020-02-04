using ShoppingBasket.Default;
using System;
using System.Collections.Generic;
using Xunit;

namespace ShoppingBasket.Test
{
    public class BasketTest
    {
        /// <summary>
        /// Instance of the basket to test.
        /// </summary>
        private readonly IBasket basket = new Basket();

        /// <summary>
        /// Data to test the total price of the items in the basket when no discounts are applied.
        /// </summary>
        /// <returns></returns>
        public static List<object[]> GetTestProductsWithTheirPricesSum() => new List<object[]>()
        {
             // Empty basket, total should be zero
            new object[] { new Product[0], 0 },

             // Three products, total should sum of their prices
            new object[] { new[] { new Product(ProductType.Bread, "Bread", 1), new Product(ProductType.Butter, "Butter", 0.8m), new Product(ProductType.Milk, "Milk", 1.15m) }, 2.95m }
        };

        [Fact]
        public void AddProductThrowsArgNullExceptionWhenAddingNullProducts()
        {
            Assert.Throws<ArgumentNullException>(() => basket.AddProduct(null));
        }

        [Theory]
        [MemberData(nameof(GetTestProductsWithTheirPricesSum))]
        public void TotalIsSumOfProductPricesWhenNoDiscounts(IEnumerable<Product> products, decimal total)
        {
            foreach (var product in products)
            {
                basket.AddProduct(product);
            }

            Assert.Equal(total , basket.GetTotal());
        }
    }
}
