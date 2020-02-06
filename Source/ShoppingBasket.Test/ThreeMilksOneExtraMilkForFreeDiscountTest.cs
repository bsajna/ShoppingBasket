using ShoppingBasket.Discounts;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShoppingBasket.Test
{
    public class ThreeMilksOneExtraMilkForFreeDiscountTest : DiscountTest
    {
        public static List<object[]> GetTestProducts()
        {
            // List of test args - list of products to calculate the discount for, expected discount amount, 
            // and expected list of products affected or involved in the discount
            var result = new List<object[]>()
            {
                 // Empty basket, total discount should be zero
                new object[] { new Product[0], 0, new Product[0] },

                 // Three products, no discount here
                new object[] { new[] { ProductHelper.GetBread(), ProductHelper.GetButter(), ProductHelper.GetMilk() }, 0m, new Product[0] }
            };

            var products = new[] { ProductHelper.GetMilk(), ProductHelper.GetMilk(), ProductHelper.GetMilk(), ProductHelper.GetMilk(), ProductHelper.GetMilk() };
            result.Add(new object[] { products, ProductHelper.GetMilk().Price, new[] { products[0], products[1], products[2], products[3] } });

            products = new[] { ProductHelper.GetMilk(), ProductHelper.GetMilk(), ProductHelper.GetMilk(), ProductHelper.GetMilk(), ProductHelper.GetMilk(),
                ProductHelper.GetMilk(), ProductHelper.GetMilk(), ProductHelper.GetMilk(), ProductHelper.GetMilk(), ProductHelper.GetMilk(), ProductHelper.GetBread(), 
                ProductHelper.GetButter() };
            result.Add(new object[] { products, ProductHelper.GetMilk().Price * 2m, 
                new[] { products[0], products[1], products[2], products[3], products[4], products[5], products[6], products[7] } });

            return result;
        }


        public ThreeMilksOneExtraMilkForFreeDiscountTest() : base(new ThreeMilksOneExtraMilkForFreeDiscount())
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
                (affectedProducts.Any() ? result.Products.All(p => affectedProducts.Count(ap => ap == p) == 1) : true));
        }
    }
}
