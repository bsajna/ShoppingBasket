using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShoppingBasket.Test
{
    public class BasketTest
    {
        /// <summary>
        /// Data to test the total price of the items in the basket when no discounts are applied.
        /// </summary>
        /// <returns></returns>
        public static List<object[]> GetTestProductsWithTheirPricesSum() => new List<object[]>()
        {
             // Empty basket, total should be zero
            new object[] { new Product[0], 0 },

             // Three products, total should be sum of their prices
            new object[] { new[] { ProductHelper.GetBread(), ProductHelper.GetButter(), ProductHelper.GetMilk() }, 2.95m }
        };

        /// <summary>
        /// Data to test the total price of the items in the basket when total discount of 2 is applied.
        /// </summary>
        /// <returns></returns>
        public static List<object[]> GetTestProductsWithTheirPricesSumDiscountedBy2() => new List<object[]>()
        {
             // Empty basket, total discounted should be zero
            new object[] { new Product[0], 0 },

             // One product, total discounted should not be less than zero
            new object[] { new[] { ProductHelper.GetButter() }, 0m },

             // Three products, total discounted should be sum of their prices minus two
            new object[] { new[] { ProductHelper.GetBread(), ProductHelper.GetButter(), ProductHelper.GetMilk() }, 0.95m }
        };

        /// <summary>
        /// Instance of the basket to test.
        /// </summary>
        private readonly IBasket basket = new Basket(new IDiscount[0]);

        [Fact]
        public void InstantiatingThrowsArgNullExceptionWhenPassingNullDiscounts()
        {
            Assert.Throws<ArgumentNullException>(() => new Basket(null));
        }

        [Fact]
        public void AddProductThrowsArgNullExceptionWhenAddingNullProducts()
        {
            Assert.Throws<ArgumentNullException>(() => basket.AddProduct(null));
        }

        [Fact]
        public void AddProductThrowsArgExceptionWhenAddingAlreadyAddedProductInstance()
        {
            var product = new Product(ProductType.Bread, "Bread", 1);

            basket.AddProduct(product);

            Assert.Throws<ArgumentException>(() => basket.AddProduct(product));
        }

        [Theory]
        [MemberData(nameof(GetTestProductsWithTheirPricesSum))]
        public void TotalIsSumOfProductPricesWhenNoDiscounts(IEnumerable<Product> products, decimal total)
        {
            foreach (var product in products)
            {
                basket.AddProduct(product);
            }

            Assert.Equal(total, basket.GetTotal());
        }

        [Theory]
        [MemberData(nameof(GetTestProductsWithTheirPricesSumDiscountedBy2))]
        public void TotalIsSumOfProductPricesReducedBySumOfDiscountAmountsAndNotLessThanZero(IEnumerable<Product> products, decimal total)
        {
            var mockDiscountOf2 = new Mock<IDiscount>();
            // Setup mock discount so their sum is 2...
            var mockDiscountOf05 = new Mock<IDiscount>();
            var mockDiscountOf15 = new Mock<IDiscount>();

            mockDiscountOf05.Setup(p => p.GetDiscountAmount(products)).Returns((new List<Product>(products), 0.5m));
            mockDiscountOf15.Setup(p => p.GetDiscountAmount(new Product[0])).Returns(() => (new Product[0], 1.5m));

            var basketWithDiscounts = new Basket(new[] { mockDiscountOf05.Object, mockDiscountOf15.Object });

            foreach (var product in products)
            {
                basketWithDiscounts.AddProduct(product);
            }

            Assert.Equal(total, basketWithDiscounts.GetTotal());
        }
    }
}
