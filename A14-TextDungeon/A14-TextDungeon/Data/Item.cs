namespace A14_TextDungeon
{
    public class Item
    {
        public enum ItemType
        {
            Armor,
            Weapon,
            HPPotion,
            MPPotion
        }

        public string ItemName { get;  set; }
        public string ItemDescription { get;  set; }
        public ItemType Itemtype { get;  set; }
        public int ItemStat { get;  set; }
        public bool IsEquippd { get;  set; }

        public Item(string itemName, string itemDescription, ItemType itemtype, int itemStat,  bool isEquippd = false)
        {
            ItemName = itemName;
            ItemDescription = itemDescription;
            Itemtype = itemtype;
            ItemStat = itemStat;
            IsEquippd = isEquippd; 
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

