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

        public string ItemName { get; private set; }
        public string ItemDescription { get; private set; }
        public ItemType Itemtype { get; private set; }
        public int ItemStat { get; private set; }
        public bool IsEquippd { get;  set; }

        public Item(string name, int stat, ItemType type, string desription, bool isEquippd = false)
        {
            ItemName = name;
            ItemDescription = desription;
            Itemtype = type;
            ItemStat = stat;
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

