using System;
using A14_TextDungeon.Scene;

namespace A14_TextDungeon.Data
{
    public class Item
    {
        public string ItemName { get; private set; }
        public string ItemDescription { get; private set; }
        public ItemType ItemType { get; private set; }
        public int ItemStat { get; private set; }
        public bool IsEquippd { get;  set; }

        public Item(string name, int stat, ItemType type, string desription, bool isEquippd = false)
        {
            ItemName = name;
            ItemDescription = desription;
            ItemType = type;
            ItemStat = stat;
            IsEquippd = isEquippd;

        }

        public string DisplayItem()
        {
            string type;
            switch (ItemType)
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

