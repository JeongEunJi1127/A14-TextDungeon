namespace A14_TextDungeon
{
    public class ShopProduct : Item
    {
        public int Price;
        public bool IsBuy;
        public ShopProduct(Item item,int price,bool isbuy = false) : base(item.ItemName, item.ItemStat, item.Itemtype, item.ItemDescription)
        {
            Price = price;
            IsBuy = isbuy;
        }

        public string ProductsName()
        {
            if(IsBuy)
            {
                return $"{DisplayItem()} | 구매완료";
            }
            else
            {
                return $"{DisplayItem()} | {Price} G";
            }
        }
    }
}
