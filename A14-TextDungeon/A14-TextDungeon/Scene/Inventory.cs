using A14_TextDungeon.Data;
using A14_TextDungeon.Manager;
using A14_TextDungeon.UI;

namespace A14_TextDungeon.Scene
{
    public enum ItemType
    {
        Armor,
        Weapon,
        HPPotion,
        MPPotion
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
        public static void RemoveItem(Item item)
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
            Console.WriteLine("장착하거나 장착을 해제하고싶은 장비의 번호를 입력하세요\n");
            Console.WriteLine("0. 나가기\n");
            InventoryInput.ShowEquipPageInput();

            //items = 인벤토리 스크립트 안에 모여있는 것들(리스트)
            if (items[selectItemIndex].IsEquippd)
            {
                UnEquipItem(items[selectItemIndex]);
                Console.WriteLine($"\n{items[selectItemIndex].ItemName}이(가) 장착 해제되었습니다");
                Thread.Sleep(1000);
                ShowEquipPage();
            }
            else
            {
                EquipItem(items[selectItemIndex]);
                Console.WriteLine($"\n{items[selectItemIndex].ItemName}이(가) 장착되었습니다");
                Thread.Sleep(1000);
                ShowEquipPage();
            }
            
        }

        //아이템 장착
        //장착 퀘스트 클리어 조건
        public static void EquipItem(Item item)
        {
            item.IsEquippd = true;            
            if(QuestManager.quests[1].IsAccepted)
            {
                QuestManager.quests[1].IsCompleted = true;
                //기타 보상에 대한 내용 추가 -> Quest에 함수로 만들어서 빼기
                foreach(string reward in QuestManager.quests[1].Rewards)
                {
                    Item questReward = new Item("이름",5,ItemType.Armor,"설명",true); // 아이템 생성
                    AddItem(questReward); // 인벤토리에 아이템 추가
                }
            }
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

        //포션 사용
        public static void RemovePotionFromInventory(ItemType potionType)
        {
            // 지정된 유형의 첫 번째 포션이 있는지 찾아 인벤토리에서 제거합니다.
            Item potionToRemove = items.FirstOrDefault(item => item.ItemType == potionType);
            if (potionToRemove != null)
            {
                items.Remove(potionToRemove);
            }
            
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
