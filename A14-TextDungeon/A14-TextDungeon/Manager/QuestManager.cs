using System;
using System.Collections.Generic;
using A14_TextDungeon.Scene;

namespace A14_TextDungeon.Quest
{
    public class QuestManager
    {
        private List<Quest> quests;

        public QuestManager()
        {
            quests = new List<Quest>
            {
                new KillMinionsQuest(),
                new EquipItemQuest(),
                new StrengthenQuest()
            };
        }

        public void ShowQuests()
        {
            Console.WriteLine("Quest!!\n");

            for (int i = 0; i < quests.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {quests[i].Name}");
            }

            Console.WriteLine("\n원하시는 퀘스트를 선택해주세요.");
            int input;
            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum && input >= 1 && input <= quests.Count)
                {
                    ShowQuestDetails(input - 1);
                    break;
                }
                else
                {
                    Console.WriteLine("올바른 번호를 입력해주세요.");
                }
            }
        }

        private void ShowQuestDetails(int index)
        {
            Quest selectedQuest = quests[index];

            Console.WriteLine($"\n{selectedQuest.Name}\n\n{selectedQuest.Description}\n");
            selectedQuest.CheckProgress();
            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n1. 수락\n2. 거절");

            int input;
            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum && (input == 1 || input == 2))
                {
                    if (input == 1)
                    {
                        Console.WriteLine("\n퀘스트를 수락했습니다.\n");
                        // 수락한 퀘스트를 처리하는 로직 추가
                    }
                    else
                    {
                        Console.WriteLine("\n퀘스트를 거절했습니다.\n");
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("올바른 번호를 입력해주세요.");
                }
            }
        }
    }
}
