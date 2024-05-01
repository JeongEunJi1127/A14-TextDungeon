using System;
using System.Collections.Generic;
using A14_TextDungeon.Scene;

namespace A14_TextDungeon.Manager
{
    public class QuestManager
    {
        private List<Quest> quests;
        
        public QuestManager()
        {
            InitializeQuests();
        }

        public void StartQuest()
        {
            ShowQuests(); // 퀘스트 목록 표시
            int selectedQuestIndex = GetSelectedQuestIndex(); // 선택한 퀘스트 인덱스 가져오기
            quests[selectedQuestIndex].ShowDetails(); // 선택한 퀘스트의 상세 내용 표시
        }
        
        private void InitializeQuests()
        {
            quests = new List<Quest>
            {
                new Quest("마을을 위협하는 미니언 처치", "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!"),
                new Quest("장비를 장착해보자", "모험가여, 장비를 장착하여 더욱 강력해져야 합니다.\n더 많은 몬스터를 처치하기 위해서는 강력한 장비가 필요합니다."),
                new Quest("더욱 더 강해지기!", "이제는 모험가님도 강해져야 할 때입니다.\n순간의 기습 공격으로 몬스터들을 처치하세요!")
            };
        }

        public void ShowQuests()
        {
            Console.WriteLine("Quest!!\n");
            for (int i = 0; i < quests.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {quests[i].Title}");
            }
            Console.WriteLine("\n원하시는 퀘스트를 선택해주세요.");
            int selectedQuestIndex = GetSelectedQuestIndex();
            quests[selectedQuestIndex].ShowDetails();
        }

        private int GetSelectedQuestIndex()
        {
            int selectedQuestIndex;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out selectedQuestIndex) && selectedQuestIndex >= 1 && selectedQuestIndex <= quests.Count)
                {
                    return selectedQuestIndex - 1;
                }
                else
                {
                    Console.WriteLine("올바른 번호를 입력해주세요.");
                }
            }
        }
    }
}
