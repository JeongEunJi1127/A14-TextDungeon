

namespace A14_TextDungeon
{
    public class InventoryManager
    {
        public List<Item> items = new List<Item>();
        public int selectItemIndex = 0;

        public void ClearInventory()
        {
            items.Clear();
        }
        public void AddItem(Item item)
        {
            if (items == null)
            {
                items = new List<Item>();
            }
            items.Add(item);
            Manager.Instance.fileManager.SaveData();
        }
        public void RemoveItem(Item item)
        {
            items.Remove(item);
            Manager.Instance.fileManager.SaveData();
        }
        public void RefrshInventory(bool isEquipPage)
        {
            Console.Clear();
            Console.WriteLine("\n== 인벤토리 == \n");
            Console.WriteLine("[아이템 목록]\n");

            if (items == null || items.Count == 0)
            {
                Console.WriteLine("인벤토리에 아무것도 없군요...");
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (isEquipPage && !items[i].IsEquippd)
                    {
                        Console.WriteLine($"{i + 1}. {items[i].DisplayItem()}\n");
                    }
                    else if (!isEquipPage && !items[i].IsEquippd)
                    {
                        Console.WriteLine($"- {items[i].DisplayItem()}\n");
                    }
                    else
                    {
                        Console.WriteLine($"[E] {items[i].DisplayItem()}\n");
                    }
                }
            }
            Manager.Instance.fileManager.SaveData();
        }

        //아이템 장착
        public void EquipItem(Item item)
        {            
            item.IsEquippd = true;

            //장착 퀘스트 클리어 조건
            Manager.Instance.questManager.QuestClear(1);
        }

        //장비아이템 중복 장착 방지 로직
        public void EquippedItemCheck(Item item)
        {
            if(item.Itemtype == Item.ItemType.HPPotion || item.Itemtype == Item.ItemType.MPPotion)
            {
                Console.WriteLine("아쉽지만 포션은 장착 할 수 없습니다..");
                Thread.Sleep(1000);
                RefrshInventory(true);
            }
            else
            {
                Item.ItemType checkItemType = item.Itemtype;
                if (items.Count == 0 || items == null)
                {
                    EquipItem(item);
                }
                else
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].Itemtype == checkItemType && items[i].IsEquippd)
                        {
                            UnEquipItem(items[i]);
                            EquipItem(item);
                        }
                        else
                        {
                            EquipItem(item);
                        }
                    }
                }
            }
            
        }

        //아이템 장착 해제
        public void UnEquipItem(Item item)
        {
            item.IsEquippd = false;
            Manager.Instance.fileManager.SaveData();
        }

        //아이템 장착여부확인
        public bool IsEquipped(Item item)
        {
            return item.IsEquippd; // 장착되지 않은 아이템이라면 false 반환
        }

        //포션 사용
        public void RemovePotionFromInventory(Item.ItemType potionType)
        {
            // 지정된 유형의 첫 번째 포션이 있는지 찾아 인벤토리에서 제거합니다.
            Item potionToRemove = items.FirstOrDefault(item => item.Itemtype == potionType);
            if (potionToRemove != null)
            {
                items.Remove(potionToRemove);
            }
            Manager.Instance.fileManager.SaveData();
        }
    }
}
