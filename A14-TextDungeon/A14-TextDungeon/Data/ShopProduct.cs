using static A14_TextDungeon.Item;

namespace A14_TextDungeon
{
    public class ShopProduct
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public ItemType Itemtype { get; set; }
        public int ItemStat { get; set; }
        public bool IsEquippd { get; set; }
        public int Price { get; set; }
        public bool IsBuy { get; set; }
        public ShopProduct(string itemName, string itemDescription, ItemType itemtype, int itemStat, int price, bool isEquippd = false,  bool isbuy = false)
        {
            ItemName = itemName;
            ItemDescription = itemDescription;
            Itemtype = itemtype;
            ItemStat = itemStat;
            IsEquippd = isEquippd;
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

        public string DisplayItem()
        {
            string type;
            switch (Itemtype)
            {
                case ItemType.Armor:
                    type = "방어력 +";
                    break;
                case ItemType.Weapon:
                    type = "공격력 +";
                    break;
                case ItemType.HPPotion:
                    type = "HP회복 +";
                    break;
                case ItemType.MPPotion:
                    type = "MP회복 +";
                    break;
                default:
                    type = "";
                    break;
            }
            return $"{ItemName} | {type}{ItemStat} | {ItemDescription}";
        }
    }
}
