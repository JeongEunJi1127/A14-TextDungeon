namespace A14_TextDungeon
{
    public class InventoryManager
    {
        public List<Item> items = new List<Item>();
        public int selectItemIndex = 0;

        public void AddItem(Item item)
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
        public void RefrshInventory(bool isEquipPage)
        {
            Console.Clear();
            Console.WriteLine("\n인벤토리\n");
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
        }

        //아이템 장착
        //장착 퀘스트 클리어 조건
        public void EquipItem(Item item)
        {
            item.IsEquippd = true;

            if (Manager.Instance.questManager.quests[1].IsAccepted)
            {
                Manager.Instance.questManager.quests[1].IsCompleted = true;

                //기타 보상에 대한 내용 추가 -> Quest에 함수로 만들어서 빼기
                foreach (string reward in Manager.Instance.questManager.quests[1].Rewards)
                {
                    // 아이템 추가
                }
            }
        }

        //아이템 장착 해제
        public void UnEquipItem(Item item)
        {
            item.IsEquippd = false;
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
        }
    }
}
