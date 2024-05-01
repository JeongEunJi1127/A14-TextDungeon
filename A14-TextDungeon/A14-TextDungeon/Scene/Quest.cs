using System;
using A14_TextDungeon.Scene;

namespace A14_TextDungeon.Manager
{
    public class Quest
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int TargetCount { get; private set; }
        public int CurrentCount { get; private set; }
        public bool IsCompleted { get; private set; }
        public string[] Rewards { get; private set; }

        public Quest(string name, string description, int targetCount, string[] rewards)
        {
            Name = name;
            Description = description;
            TargetCount = targetCount;
            CurrentCount = 0;
            IsCompleted = false;
            Rewards = rewards;
        }

        public void UpdateProgress(int amount)
        {
            CurrentCount += amount;
            if (CurrentCount >= TargetCount)
            {
                IsCompleted = true;
            }
        }

        public void ClaimRewards()
        {
            // 보상을 플레이어에게 주는 로직
            Console.WriteLine($"퀘스트 \"{Name}\"를 완료하였습니다. 보상을 획득합니다:");
            foreach (string reward in Rewards)
            {
                Console.WriteLine(reward);
            }
        }

        public void ShowMinionQuest(QuestManager questManager)
        {
            Quest minionQuest = new Quest("마을을 위협하는 미니언 처치",
                                          "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!",
                                          1,
                                          new string[] { "쓸만한 방패 x 1", "5G" });
            Console.WriteLine($"Quest!!\n{minionQuest.Description}\n\n- 미니언 {minionQuest.TargetCount}마리 처치 ({minionQuest.CurrentCount}/{minionQuest.TargetCount})\n");
            Console.WriteLine("- 보상 -");
            foreach (string reward in minionQuest.Rewards)
            {
                Console.WriteLine(reward);
            }
            Console.WriteLine("\n1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine("\n--------------------");
            string input = Console.ReadLine();
            questManager.AcceptOrRejectQuest(input); 
        }

        public static void ShowEquipmentQuest(Item item)
        {

            if (item.IsEquipped)
            {
                Quest quest = new Quest("장비를 장착해보자", "인벤토리에서 아무 장치나 완료해보자!", 1, new string[] { "10골드", "경험치 100" });
                quest.UpdateProgress(1); // 퀘스트 진행상황 업데이트
                Console.WriteLine("- 보상 -");
                if (quest.IsCompleted)
                {
                    Console.WriteLine($"퀘스트 \"{quest.Name}\"를 완료했습니다!");
                    quest.ClaimRewards(); // 보상 획득
                }
            }
        }
        
        public static void ShowLevelUpQuest()
        {
            Console.WriteLine("Quest!!\n");
            Console.WriteLine("더욱 더 강해지기!\n");
            Console.WriteLine("- 목표 -");
            Console.WriteLine("캐릭터 레벨을 상승시키세요.");
            Console.WriteLine("- 보상 -");
            Console.WriteLine("강화된 무기, 더 강력한 방어구\n");
        }
    }
}
