using ShoppingBasket.Discounts;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShoppingBasket.Test
{
    public class TwoButtersBreadDiscountTest : DiscountTest
    {
        public static List<object[]> GetTestProducts()
        {
            // List of test args - list of products to calculate the discount for, expected discount amount, 
            // and expected list of products affected or involved in the discount
            var result = new List<object[]>()
            {
                 // Empty basket, total discount should be zero
                new object[] { new Product[0], 0, new Product[0] },

                 // Three products, total discount should be sum of their prices, no discount here
                new object[] { new[] { ProductHelper.GetBread(), ProductHelper.GetButter(), ProductHelper.GetMilk() }, 0m, new Product[0] }
            };

            var products = new[] { ProductHelper.GetBread(), ProductHelper.GetButter(), ProductHelper.GetButter(), ProductHelper.GetButter() };
            result.Add(new object[] { products, 0.5m, new[] { products[0], products[1], products[2] } });

            products = new [] { ProductHelper.GetBread(), ProductHelper.GetButter(), ProductHelper.GetButter(), ProductHelper.GetButter(),
                ProductHelper.GetBread(), ProductHelper.GetButter(), ProductHelper.GetButter(), ProductHelper.GetButter(), ProductHelper.GetButter() };
            result.Add(new object[] { products, 1m, new[] { products[0], products[1], products[2], products[3], products[4], products[5] } });

            return result;
        }


        public TwoButtersBreadDiscountTest() : base(new TwoButtersBreadDiscount())
        {
        }

        [Theory]
        [MemberData(nameof(GetTestProducts))]
        public void GetDiscountAmountIsCorrectAndAppliedToExpectedProducts(IEnumerable<Product> products, decimal total, IEnumerable<Product> affectedProducts)
        {
            var result = discount.GetDiscountAmount(products);

            Assert.Equal(total, result.DiscountAmount);
            // Assert that resulting products are contained in expected products and each element is contained once
            Assert.True(result.Products != null && result.Products.Count() == affectedProducts.Count() && 
                (affectedProducts.Any() ?  result.Products.All(p => affectedProducts.Count(ap => ap == p) == 1) : true));
        }
    }
}
