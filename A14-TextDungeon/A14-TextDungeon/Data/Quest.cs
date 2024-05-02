namespace A14_TextDungeon
{
    public class Quest
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int TargetCount { get; private set; }
        public int CurrentCount { get; private set; }
        public bool IsAccepted {get; set;}
        public bool IsCompleted { get; set; }
        public string[] Rewards { get; private set; }

        public Quest(string name, string description, int targetCount, bool IsAccepted, bool IsCompleted, string[] rewards)
        {
            Name = name;
            Description = description;
            TargetCount = targetCount;
            CurrentCount = 0;
            IsCompleted = false;
            IsAccepted = false;
            Rewards = rewards;
        }

        public void UpdateProgress(int amount)
        {
            CurrentCount += amount;
            if (CurrentCount >= TargetCount)
            {
                IsCompleted = true;
                ClaimRewards(0);
            }
        }


        // 보상을 주는 로직
        public void ClaimRewards(int stagenum)
        {
            Console.WriteLine($"퀘스트 \"{Manager.Instance.questManager.quests[stagenum].Name}\"를 완료하였습니다. 보상을 획득합니다:");
            Console.WriteLine("- 보상 -");
            Console.WriteLine($"{Manager.Instance.questManager.quests[stagenum].Rewards}\n\n");

            switch(stagenum)
            {
                case 0:
                    Console.WriteLine($"골드 5G를 획득하였습니다. 현재 보유한 골드: {Manager.Instance.gameManager.user.Gold}G");

                    Item questReward = new Item("쓸만한 갑옷",5,Item.ItemType.Armor,"범부에 불과했던 남자가, 오직 한 사람만을 떠올리며 만든 갑옷이다. 제법 쓸만하다."); // 아이템 생성

                    Manager.Instance.inventoryManager.AddItem(questReward);
                    Manager.Instance.gameManager.user.AddGold(5); // AddGold()대신, Quest Gold로 넣기
                    break;
                case 1:
                    Console.WriteLine($"골드 5G를 획득하였습니다. 현재 보유한 골드: {Manager.Instance.gameManager.user.Gold}G");
                    
                    Item questReward1 = new Item("여신의 축복",30,Item.ItemType.HPPotion,"이 갑옷에는 알 수 없는 힘이 깃들어 있다.");

                    Manager.Instance.inventoryManager.AddItem(questReward1);
                    Manager.Instance.gameManager.user.AddGold(5);
                    break;
                case 2:
                    Console.WriteLine($"골드 5G를 획득하였습니다. 현재 보유한 골드: {Manager.Instance.gameManager.user.Gold}G");
                    
                    Item questReward2 = new Item("강화된 마체테",5, Item.ItemType.Weapon,"피에 젖어 있다.");

                    Manager.Instance.inventoryManager.AddItem(questReward2);
                    Manager.Instance.gameManager.user.AddGold(5);
                    break;
            }
            //퀘스트 반복
            //다시 false 넣기
            Manager.Instance.questManager.quests[stagenum].IsAccepted =false;
            Manager.Instance.questManager.quests[stagenum].IsCompleted = false;
        }
    }
}
