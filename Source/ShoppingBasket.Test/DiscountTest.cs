using System;
using System.Linq;
using Xunit;

namespace ShoppingBasket.Test
{
    /// <summary>
    /// Contains common tests for a discount.
    /// </summary>
    public abstract class DiscountTest
    {
        private static readonly Product[] emptyProductsList = new Product[0];
        protected readonly IDiscount discount;

        public DiscountTest(IDiscount discount)
        {
            this.discount = discount;
        }

        [Fact]
        public void GetDiscountAmountThrowsArgNullExceptionWhenPassingNull()
        {
            Assert.Throws<ArgumentNullException>(() => discount.GetDiscountAmount(null));
        }

        [Fact]
        public void GetDiscountAmountDoesNotReturnSameCollectionInstancePassedAsArg()
        {
            var amountAndProducts = discount.GetDiscountAmount(emptyProductsList);

            Assert.True(amountAndProducts.Products != emptyProductsList);
        }

        [Fact]
        public void GetDiscountAmountReturnsZeroAndEmptyCollectionWhenEmptyCollectionPassedAsArg()
        {
            var amountAndProducts = discount.GetDiscountAmount(emptyProductsList);

            Assert.True(amountAndProducts.DiscountAmount == 0 && amountAndProducts.Products != null && amountAndProducts.Products.Count() == 0);
        }
    }
}
