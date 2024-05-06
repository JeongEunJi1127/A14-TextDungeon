using A14_TextDungeon.Data;

namespace A14_TextDungeon
{
    public class QuestManager
    {
        public List<Quest> quests = new List<Quest>();

        public void ShowQuests()
        {
            Item item = new Item("Dummy", "Dummy", Item.ItemType.Armor, 2, false);
            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Quest!!");
                Console.WriteLine("1. 마을을 위협하는 미니언 처치" + GetQuestStatusDisplay(0));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("2. 장비를 장착해보자" + GetQuestStatusDisplay(1));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("3. 더욱 더 강해지기!" + GetQuestStatusDisplay(2));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n0. 나가기");
                Console.ForegroundColor = ConsoleColor.White;
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
                        ShowQuests();
                        break;
                }
            }
        }

        public void ResetQuest()
        {
            quests.Clear();
        }

        public void AddQuest(Quest quest)
        {
            quests.Add(quest);
        }

        public string GetQuestStatusDisplay(int stagenum)
        {
            if (quests[stagenum].IsCompleted)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                return " (완료)";
            }
            else if (quests[stagenum].IsAccepted)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                return " (진행중)";
                
            }
            else
            {
                return "";
            }
        }

        public void AcceptOrRejectQuest(int input, int stagenum)
        {
            while(true)
            {
                switch (input)
                {
                    case 0:
                        Manager.Instance.questManager.ShowQuests();
                        return;
                    case 1:

                        if (!quests[stagenum].IsAccepted)
                        {
                            Console.WriteLine("\n\n퀘스트를 수락했습니다!");
                            quests[stagenum].IsAccepted = true;
                            Manager.Instance.fileManager.SaveData();
                            Console.WriteLine("돌아가려면 아무 키나 누르세요");
                            Console.ReadKey();
                        }
                        Manager.Instance.questManager.ShowQuests();
                        return;

                    case 2:
                        if (!quests[stagenum].IsAccepted)
                        {
                            Console.WriteLine("\n\n퀘스트를 거절했습니다.");
                            Console.WriteLine("돌아가려면 아무 키나 누르세요");
                            Console.ReadKey(); ;
                        }
                        Manager.Instance.questManager.ShowQuests();
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public void DisplayQuestOptions(int stagenum)
        {
            if (quests[stagenum].IsAccepted)
            {
                Console.WriteLine("이미 진행중인 퀘스트 입니다\n\n");
            }
            else
            {
                Console.WriteLine("\n1. 수락");
                Console.WriteLine("2. 거절\n");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n0. 나가기");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--------------------");
        }

        public void QuestClear(int num)
        {
            if (quests[num].IsAccepted && quests[num].IsWaiting == false)
            {
                quests[num].IsCompleted = true;
                quests[num].IsWaiting = true;
                Manager.Instance.fileManager.SaveData();
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\n-----------------------------------------\n");
                Console.WriteLine("|||[퀘스트 완료] 퀘스트 창에서 보상을 확인하세요|||");
                Console.WriteLine("\n-----------------------------------------\n\n");
                Console.ForegroundColor = ConsoleColor.White;

                Console.ReadLine();
            }
        }

        // 보상을 주는 로직
        public void ClaimRewards(int stagenum)
        {
            Console.Clear();
            Console.WriteLine("----------------------\n\n");
            Console.WriteLine($"[{quests[stagenum].Name}]");
            Console.WriteLine("퀘스트를 완료하였습니다. 보상을 획득합니다\n");
            Console.WriteLine("- 보상 -");
            foreach (string s in quests[stagenum].Rewards)
            {
                Console.WriteLine($"{s}");
            }

            switch (stagenum)
            {
                case 0:
                    Item questReward = new Item("쓸만한 갑옷", "범부에 불과했던 남자가, 오직 한 사람만을 떠올리며 만든 갑옷이다. 제법 쓸만하다.", Item.ItemType.Armor, 5); // 아이템 생성

                    Manager.Instance.inventoryManager.AddItem(questReward);
                    Manager.Instance.gameManager.user.AddGold(5); // AddGold()대신, Quest Gold로 넣기
                    break;
                case 1:
                    Item questReward1 = new Item("여신의 축복", "이 갑옷에는 알 수 없는 힘이 깃들어 있다.", Item.ItemType.HPPotion, 30);

                    Manager.Instance.inventoryManager.AddItem(questReward1);
                    Manager.Instance.gameManager.user.AddGold(5);
                    break;
                case 2:
                    Item questReward2 = new Item("강화된 마체테", "피에 젖어 있다.", Item.ItemType.Weapon, 5);

                    Manager.Instance.inventoryManager.AddItem(questReward2);
                    Manager.Instance.gameManager.user.AddGold(5);
                    break;
            }
            //퀘스트 반복
            //다시 false 넣기
            quests[stagenum].IsAccepted = false;
            quests[stagenum].IsCompleted = false;
            quests[stagenum].IsWaiting = false;
            Manager.Instance.fileManager.SaveData();
            Console.WriteLine("보상을 받으려면 Enter를 누르세요");
            Console.ReadLine();
            Manager.Instance.gameManager.village.ShowVillage();
        }

        public void InputQuestNumber(int stagenum)
        {
            while (true)
            {
                var isSuccess = int.TryParse(Console.ReadLine(), out int input);
                if (!isSuccess)
                {
                    Console.WriteLine("잘못된 입력입니다");
                }
                else
                {
                    AcceptOrRejectQuest(input, stagenum);
                }
            }
        }
        //퀘스트 1: 미니언 잡기
        public void ShowMinionQuest()
        {
            if (quests[0].IsCompleted)
            {
                ClaimRewards(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Quest!!\n\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(quests[0].Name, "\n");
                Console.WriteLine($"미니언 {quests[0].TargetCount}마리 처치 ({quests[0].CurrentCount}/{Manager.Instance.questManager.quests[0].TargetCount})\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(quests[0].Description, "\n\n");
                Console.WriteLine("\n- 보상 -");
                foreach (string s in quests[0].Rewards)
                {
                    Console.WriteLine(s);
                }
                DisplayQuestOptions(0);
                InputQuestNumber(0);
            }
        }

        //퀘스트 2: 장비 장착하기 
        public void ShowEquipmentQuest(Item item)
        {
            if (quests[1].IsCompleted)
            {
                ClaimRewards(1);
                Manager.Instance.questManager.ShowQuests();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Quest!!\n\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(quests[1].Name, "\n");
                Console.WriteLine($"(인벤토리에서 아무 장비나 {quests[0].TargetCount}개 이상 장착하기)\n");
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(quests[1].Description, "\n");
                Console.WriteLine("\n- 보상 -");
                foreach (string s in quests[1].Rewards)
                {
                    Console.WriteLine(s);
                }
                DisplayQuestOptions(1);
                InputQuestNumber(1);
            }
        }

        //퀘스트 3: 레벨업 
        public void ShowLevelUpQuest()
        {
            if(quests[2].IsCompleted)
            {
                ClaimRewards(2);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Quest!!\n\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(quests[2].Name, "\n");
                Console.WriteLine($"({quests[2].TargetCount}레벨업 하기)\n");
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(quests[2].Description, "\n");
                Console.WriteLine("\n- 보상 -");
                foreach (string s in quests[2].Rewards)
                {
                    Console.WriteLine(s);
                }
                DisplayQuestOptions(2);
                InputQuestNumber(2);
            }
        }
    }
}