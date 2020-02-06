namespace ShoppingBasket.Test
{
    public static class ProductHelper
    {
        public static Product GetBread()
        {
            return new Product(ProductType.Bread, "Bread", 1);
        }

        public static Product GetButter()
        {
            return new Product(ProductType.Butter, "Butter", 0.8m);
        }

        public static Product GetMilk()
        {
            return new Product(ProductType.Milk, "Milk", 1.15m);
        }
    }
}
