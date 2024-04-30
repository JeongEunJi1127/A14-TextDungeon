using System;
using System.Collections.Generic;

namespace A14_TextDungeon.Scene
{
    public enum ItemType
    {
        Armor,
        Wepon,
        Potion
    }

    public class Inventory
    {
        public List<Item> items;

        public Inventory()
        {
            items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public void RefrshInventory()
        {
            Console.Clear();
            Console.WriteLine("\n인벤토리\n");
            Console.WriteLine("[아이템 목록]\n");

            if(items.Count == 0)
            {
                Console.WriteLine("인벤토리에 아무것도 없군요 ...");
            }
            else
            {
                for(int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"- {items[i].DisplayItem()}\n");
                }
            }
        }

        public void ShowInventory()
        {
            RefrshInventory();
            Console.WriteLine("\n1. 장착관리\n");
            Console.WriteLine("0. 나가기\n");
            ShowInventoryInput();
        }

        private void ShowInventoryInput()
        {
            int input;
            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    switch (input)
                    {
                        case 0:
                            Village.ShowVillage();
                            break;
                        case 1:
                            //장착관리 페이지 제작
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }
        }
    }

    

    public class Item
    {
        public string ItemName;
        public string ItemDescription;
        public ItemType ItemType;
        public int ItemStat;

        

        public Item(string name,int stat,ItemType type,string desription)
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
