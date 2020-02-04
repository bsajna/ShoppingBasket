using System;
using Xunit;

namespace ShoppingBasket.Test
{
    public class ProductTest
    {
        [Fact]
        public void ProductWithNegativePriceThrowsArgOutOfRangeException()
        {
            // This tests that product ctor throws ArgumentOutOfRange exception only when price is -1, I'm honestly not sure how to write test that asserts
            // that method throws an exception on any negative value... Try some random value each time? Multiple values? If multiple values, then which values?... See my dilemma?
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product(ProductType.Bread, "Bread", -1));
        }
    }
}
