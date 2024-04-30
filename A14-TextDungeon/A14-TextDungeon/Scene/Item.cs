using System;

namespace A14_TextDungeon.Scene
{
    public class Item
    {
        public string ItemName;
        public string ItemDescription;
        public ItemType ItemType;
        public int ItemStat;



        public Item(string name, int stat, ItemType type, string desription)
        {
            ItemName = name;
            ItemDescription = desription;
            ItemType = type;
            ItemStat = stat;
        }

        public string DisplayItem()
        {
            string type;
            switch (ItemType)
            {
                case ItemType.Armor:
                    type = "방어력 +";
                    break;
                case ItemType.Wepon:
                    type = "공격력 +";
                    break;
                case ItemType.Potion:
                    type = "HP회복 +";
                    break;
                default:
                    type = "";
                    break;
            }
            return $"{ItemName} | {type}{ItemStat} | {ItemDescription}";
        }
    }
}
  
