namespace A14_TextDungeon
{
    public class QuestManager
    {
        public List<Quest> quests = new List<Quest>
        {
            //quest.Add(newQuest())
            //quests.Add(new Quest("name", "설명", 0, false, false, rewards1배열));
            //string name, string description, int targetCount, bool IsAccepted, bool IsCompleted, string[] rewards
            new Quest ("[마을을 위협하는 미니언 처치]","이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!",1,false,false,new string[] { "쓸만한 방패 x 1", "5G" }),
            new Quest ("[장비를 장착해보자!]","이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!",1,false,false,new string[] { "여신의 축복 x 1", "5G" }),
            new Quest ("[더욱 더 강해지기!]","이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!",1,false,false, new string[] { "강화된 마체테 x 1", "5G" })
        };

        public void ShowQuests()
        {
            Item item = new Item("Dummy",2, Item.ItemType.Armor,"Dummy",false);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quest!!");
                Console.WriteLine("1. 마을을 위협하는 미니언 처치"+ GetQuestStatusDisplay(0) );
                Console.WriteLine("2. 장비를 장착해보자" + GetQuestStatusDisplay(1));
                Console.WriteLine("3. 더욱 더 강해지기!" + GetQuestStatusDisplay(2));
                Console.WriteLine("\n0. 나가기\n\n");
                Console.WriteLine("\n원하시는 퀘스트를 선택해주세요.\n");

                int input;
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);

                switch (input)
                {
                    case 0:
                        Manager.Instance.gameManager.village.ShowVillage();
                        return;
                    case 1:
                        //매개변수? 메소드로 선언? 싱글톤?->하나의 정보를 소지
                        //인스턴스 선언?
                        //퀘스트가 객체여서 list 이름
                        ShowMinionQuest();
                        return;
                    case 2:
                        ShowEquipmentQuest(item);
                        return;
                    case 3:
                        ShowLevelUpQuest();
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }
        public string GetQuestStatusDisplay(int stagenum)
        {
            if (quests[stagenum].IsAccepted == true)
            {
                return " (진행중)";
            }
            else
            {
                return "";
            }
        }

        public void AcceptOrRejectQuest(string input, int stagenum)
        {
            switch (input)
            {
                case "0":
                    Manager.Instance.gameManager.village.ShowVillage();
                    return;
                case "1":
                    Console.WriteLine("\n\n퀘스트를 수락했습니다!");
                    quests[stagenum].IsAccepted = true;
                    Console.WriteLine("돌아가려면 Enter를 누르세요");
                    Console.ReadLine();
                    return;
                case "2":
                    Console.WriteLine("\n\n퀘스트를 거절했습니다.");
                    Console.WriteLine("돌아가려면 Enter를 누르세요");
                    Console.ReadLine();
                    return;
                default:
                    Console.WriteLine("잘못된 선택입니다.");
                    Console.ReadLine();
                    break;
            }
        }

        public void DisplayQuestOptions(int stagenum)
        {
            if (quests[stagenum].IsAccepted)
            {
                Console.WriteLine("이미 진행중인 퀘스트 입니다");
            }
            else
            {
                Console.WriteLine("\n1. 수락");
                Console.WriteLine("2. 거절\n");
            }
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n--------------------");
        }

        //퀘스트 1: 미니언 잡기
        public void ShowMinionQuest()
        {
            Console.WriteLine("Quest!!\n\n");
            Console.WriteLine(quests[0].Name,"\n");
            Console.WriteLine(quests[0].Description,"\n");
            Console.WriteLine($"미니언 {quests[0].TargetCount}마리 처치 ({quests[0].CurrentCount}/{Manager.Instance.questManager.quests[0].TargetCount})\n");
            Console.WriteLine("- 보상 -");
            foreach (string s in quests[0].Rewards)
            {
                Console.WriteLine(s);
            }
            DisplayQuestOptions(0);
            string input = Console.ReadLine();
            AcceptOrRejectQuest(input, 0);
        }

        //퀘스트 2: 장비 장착하기 
        public void ShowEquipmentQuest(Item item)
        {
            Console.WriteLine("Quest!!\n\n");
            Console.WriteLine(quests[1].Name,"\n");
            Console.WriteLine(quests[1].Description,"\n");
            Console.WriteLine($"인벤토리에서 아무 장비나 {quests[0].TargetCount}개 이상 장착하기)\n");
            Console.WriteLine("- 보상 -");
            foreach (string s in quests[1].Rewards)
            {
                Console.WriteLine(s);
            }
            DisplayQuestOptions(1);
            string input = Console.ReadLine();
            AcceptOrRejectQuest(input,1); 
        }

        public void ClearEquipmentQuest()
        {
            // 퀘스트 클리어 여부를 확인할 때 사용
            foreach (Item item in Manager.Instance.inventoryManager.items)
            {
                if (item.IsEquippd)
                {
                    // 해당 아이템과 관련된 퀘스트를 찾기
                    foreach (Quest quest in quests)
                    {
                        if (quest.Name == "[장비를 장착해보자!]")
                        {
                            if (quest.IsCompleted == true)
                            {
                                quest.ClaimRewards(1); // 보상 획득
                            }
                        }
                    }
                }
            }
        }

        //퀘스트 3: 레벨업 
        public void ShowLevelUpQuest()
        {
            Console.WriteLine("Quest!!\n\n");
            Console.WriteLine(quests[2].Name,"\n");
            Console.WriteLine(quests[2].Description,"\n");
            Console.WriteLine($"인벤토리에서 아무 장비나 {quests[2].TargetCount}개 이상 장착하기)\n");
            Console.WriteLine("- 보상 -");
            foreach (string s in quests[2].Rewards)
            {
                Console.WriteLine(s);
            }
            DisplayQuestOptions(2);
            string input = Console.ReadLine();
            AcceptOrRejectQuest(input,2);
        }
    }
}
