using System;
using System.Collections.Generic;
using Serilog;
using ShoppingBasket.Discounts;
using static ShoppingBasket.Console.ProductHelper;

namespace ShoppingBasket.Console
{
    // Test console to try out different shopping basket configurations
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console().CreateLogger();

            // 1 bread, 1 butter, 1 milk
            var products = new[] { GetBread(), GetButter(), GetMilk() };
            var basket = CreateAndSetupBasketWithTwoDiscounts(products);
            
            basket.GetTotal();

            //2 butters, 2 breads
            products = new[] { GetBread(), GetButter(), GetBread(), GetButter() };
            basket = CreateAndSetupBasketWithTwoDiscounts(products);

            basket.GetTotal();

            // 4 milks
            products = new[] { GetMilk(), GetMilk(), GetMilk(), GetMilk() };
            basket = CreateAndSetupBasketWithTwoDiscounts(products);

            basket.GetTotal();

            // 2 butters, 1 bread, 8 milks
            products = new[] { GetButter(), GetButter(), GetBread(), GetMilk(), GetMilk(), GetMilk(), GetMilk(), GetMilk(), GetMilk(), GetMilk(), GetMilk() };
            basket = CreateAndSetupBasketWithTwoDiscounts(products);

            basket.GetTotal();

            System.Console.ReadKey();
        }

        static Basket CreateAndSetupBasketWithTwoDiscounts(IEnumerable<Product> products)
        {
            var basket = new Basket(new IDiscount[] { new TwoButtersBreadDiscount(), new ThreeMilksOneExtraMilkForFreeDiscount() });

            AddProductsToBAsket(products, basket);

            return basket;
        }

        static void AddProductsToBAsket(IEnumerable<Product> products, Basket basket)
        {
            foreach (var p in products)
            {
                basket.AddProduct(p);
            }
        }
    }
}
