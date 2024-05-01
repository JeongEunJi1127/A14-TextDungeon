using A14_TextDungeon.Data;
using A14_TextDungeon.Manager;
using A14_TextDungeon.UI;

namespace A14_TextDungeon.Scene
{
    public enum ItemType
    {
        Armor,
        Weapon,
        Potion
    }

    public class Inventory
    {
        public static List<Item> items;
        public static int selectItemIndex = 0;
        public Inventory()
        {
            items = new List<Item>();
        }

       
         public static void AddItem(Item item)
        {
            if (items == null) 
            { 
                items = new List<Item>(); 
            }

            items.Add(item);
        }
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public static void RefrshInventory(bool isEquipPage)
        {
            Console.Clear();
            Console.WriteLine("\n인벤토리\n");
            Console.WriteLine("[아이템 목록]\n");

            if(items == null ||items.Count == 0)
            {
                Console.WriteLine("인벤토리에 아무것도 없군요...");
            }
            else
            {
                for(int i = 0; i < items.Count; i++)
                {
                    if (isEquipPage && !items[i].IsEquippd)
                    {
                        Console.WriteLine($"{i+1}. {items[i].DisplayItem()}\n");
                    }
                    else if(!isEquipPage && !items[i].IsEquippd)
                    {
                        Console.WriteLine($"- {items[i].DisplayItem()}\n");
                    }
                    else
                    {
                        Console.WriteLine($"[E] {items[i].DisplayItem()}\n");
                    }
                }
            }
        }

        public static void ShowEquipPage()
        {           
            RefrshInventory(true);
            Console.WriteLine("장착을 원하는 장비의 번호를 입력하세요\n");
            Console.WriteLine("0. 나가기\n");
            InventoryInput.ShowEquipPageInput();

            if (items[selectItemIndex].IsEquippd)
            {
                UnEquipItem(items[selectItemIndex]);
                ShowEquipPage();
            }
            else
            {
                EquipItem(items[selectItemIndex]);
                ShowEquipPage();
            }
            
        }

        //아이템 장착
        public static void EquipItem(Item item)
        {
            item.IsEquippd = true;            

        }
        
        //아이템 장착 해제
        public static void UnEquipItem(Item item)
        {
            item.IsEquippd = false;
        }

        //아이템 장착여부확인
        public static bool IsEquipped(Item item)
        {           
            return item.IsEquippd; // 장착되지 않은 아이템이라면 false 반환
        }

        public static void ShowInventory()
        {
            RefrshInventory(false);
            Console.WriteLine("\n1. 장착관리\n");
            Console.WriteLine("0. 나가기\n");
            InventoryInput.ShowInventoryInput();
        }
    }   
}
