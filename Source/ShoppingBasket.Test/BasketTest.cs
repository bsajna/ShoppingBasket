using ShoppingBasket.Default;
using System;
using System.Collections.Generic;
using Xunit;

namespace ShoppingBasket.Test
{
    public class BasketTest
    {
        public static List<object[]> GetTestProductsWithTheirPricesSum() => new List<object[]>()
        {
            new object[] { new Product[0], 0 },
            new object[] { new[] { new Product(ProductType.Bread, "Bread", 1), new Product(ProductType.Butter, "Butter", 0.8m), new Product(ProductType.Milk, "Milk", 1.15m) }, 2.95m }
        };

        [Fact]
        public void AddProductThrowsArgNullExceptionWhenAddingNullProducts()
        {
            var basket = new Basket();

            Assert.Throws<ArgumentNullException>(() => basket.AddProduct(null));
        }

        [Theory]
        [MemberData(nameof(GetTestProductsWithTheirPricesSum))]
        public void TotalIsSumOfProductPricesWhenNoDiscounts(IEnumerable<Product> products, decimal total)
        {
            var basket = new Basket();

            foreach (var product in products)
            {
                basket.AddProduct(product);
            }

            Assert.Equal(total , basket.GetTotal());
        }
    }
}
