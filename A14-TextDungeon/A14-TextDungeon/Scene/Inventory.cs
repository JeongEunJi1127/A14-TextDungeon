using A14_TextDungeon.Manager;

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

            if(items.Count == 0)
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
            ShowEquipPageInput();

            //items = 인벤토리 스크립트 안에 모여있는 것들(리스트)
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

        public static void ShowInventory()
        {
            RefrshInventory(false);
            Console.WriteLine("\n1. 장착관리\n");
            Console.WriteLine("0. 나가기\n");
            ShowInventoryInput();
        }

        public static void ShowEquipPageInput()
        {
            int input;
            int index;
            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    index = input - 1;
                    if(input == 0)
                    {
                        ShowInventory();
                    }
                    else if(index < 0 || index >= items.Count)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        ShowEquipPageInput();
                    }
                    else
                    {
                        selectItemIndex = index;
                        break;
                    }
                }
            }
        }

        private static void ShowInventoryInput()
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
                            ShowEquipPage();
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
}
