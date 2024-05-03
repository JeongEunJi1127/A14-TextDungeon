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

        public bool AcceptOrRejectQuest(int input, int stagenum)
        {
            switch (input)
            {
                case 0:
                    return true;
                case 1:
                    if(!quests[stagenum].IsAccepted)
                    {
                        Console.WriteLine("\n\n퀘스트를 수락했습니다!");
                        quests[stagenum].IsAccepted = true;
                        Console.WriteLine("돌아가려면 아무 키나 누르세요");
                        Console.ReadKey();
                    }
                    return false;

                case 2:
                    if(!quests[stagenum].IsAccepted)
                    {
                        Console.WriteLine("\n\n퀘스트를 거절했습니다.");
                        Console.WriteLine("돌아가려면 아무 키나 누르세요");
                        Console.ReadKey();
                    }
                    //함수를 끝낼 때는 return;
                    return false;
                default:
                    return false; 
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

        public void QuestClear(int num)
        {
            if(quests[num].IsAccepted)
            {
                quests[num].IsCompleted = true;
                ClaimRewards(num);
            }
        }

        // 보상을 주는 로직
        public void ClaimRewards(int stagenum)
        {
            Console.WriteLine($"퀘스트 \"{quests[stagenum].Name}\"를 완료하였습니다. 보상을 획득합니다:");
            Console.WriteLine("- 보상 -");
            Console.WriteLine($"{quests[stagenum].Rewards}\n\n");

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
            quests[stagenum].IsAccepted =false;
            quests[stagenum].IsCompleted = false;
        }

        //퀘스트 1: 미니언 잡기
        public void ShowMinionQuest()
        {
                Console.Clear();
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
        }

        //퀘스트 2: 장비 장착하기 
        public void ShowEquipmentQuest(Item item)
        {
            Console.Clear();
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
            //AcceptOrRejectQuest(input,1); 
        }

        //퀘스트 3: 레벨업 
        public void ShowLevelUpQuest()
        {
            Console.Clear();
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
            //AcceptOrRejectQuest(input,2);
        }
    }
}
