// QuestManager.cs

using System;
using A14_TextDungeon.Scene;

namespace A14_TextDungeon.Manager
{
    public class QuestManager
    {
        public static int ShowQuests()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quest!!");
                Console.WriteLine("1. 마을을 위협하는 미니언 처치");
                Console.WriteLine("2. 장비를 장착해보자");
                Console.WriteLine("3. 더욱 더 강해지기!\n");
                Console.WriteLine("0. 나가기\n\n");
                Console.WriteLine("\n원하시는 퀘스트를 선택해주세요.\n");

                int input;
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);

                switch (input)
                {
                    case 0:
                        Village.ShowVillage();
                        break;
                    case 1:
                        QuestManager questManager = new QuestManager(); // QuestManager의 인스턴스 생성
                        Quest quest = new Quest("Dummy", "Dummy", 0, new string[0]);
                        quest.ShowMinionQuest(questManager); // QuestManager 인스턴스 전달
                        break;
                    case 2:
                        Quest.ShowEquipmentQuest();
                        break;
                    case 3:
                        Quest.ShowStrengthQuest();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }

        public void AcceptOrRejectQuest(string input)
        {
            switch (input)
            {
                case "0":
                    Village.ShowVillage();
                    break;
                case "1":
                    Console.WriteLine("\n\n퀘스트를 수락했습니다!");
                    Console.WriteLine("돌아가려면 Enter를 누르세요");
                    Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("\n\n퀘스트를 거절했습니다.");
                    Console.WriteLine("돌아가려면 Enter를 누르세요");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
