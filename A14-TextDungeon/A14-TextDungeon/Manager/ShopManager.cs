namespace A14_TextDungeon
{
    public class ShopManager
    {
        public List<ShopProduct> products = new List<ShopProduct>();
        public void AddProduct(ShopProduct product)
        {
            products.Add(product);
        }
        public void RemoveProduct(ShopProduct product)
        {
            products.Remove(product);
        }
        public void ClearShop()
        {
            products.Clear();
        }
    }
}
