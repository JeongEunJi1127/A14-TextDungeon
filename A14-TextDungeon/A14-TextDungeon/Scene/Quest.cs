using System;

namespace A14_TextDungeon.Scene
{
    //종료조건, 퀘스트 각각 나누기,
    public class QuestScene
    {
        private static bool quest1Accepted = false; // 퀘스트 1이 수락되었는지 여부를 저장하는 변수
        private static int quest1MinionCount = 0; // 퀘스트 1의 미니언 처치 개수를 저장하는 변수

        public static void ShowQuests()
        {
            Console.WriteLine("Quest!!\n");
            Console.WriteLine(GetQuestStatusText(1, "마을을 위협하는 미니언 처치"));
            Console.WriteLine("2. 장비를 장착해보자");
            Console.WriteLine("3. 더욱 더 강해지기!\n");
            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");

            int input;
            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum && input >= 1 && input <= 3)
                {
                    ShowQuestDetails(input);
                    break;
                }
                else
                {
                    Console.WriteLine("올바른 번호를 입력해주세요.");
                }
            }
        }

        private static string GetQuestStatusText(int questNumber, string questName)
        {
            if (questNumber == 1 && quest1Accepted)
            {
                return $"1. {questName}(진행중)";
            }
            else
            {
                return $"{questNumber}. {questName}";
            }
        }

        private static void ShowQuestDetails(int questNumber)
        {
            switch (questNumber)
            {
                case 1:
                    Console.WriteLine("\n마을을 위협하는 미니언 처치\n");
                    Console.WriteLine("이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?");
                    Console.WriteLine("마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!");
                    Console.WriteLine("모험가인 자네가 좀 처치해주게!\n");
                    Console.WriteLine($"- 미니언 5마리 처치 ({quest1MinionCount}/5)\n"); // 퀘스트 진행 상황 출력
                    Console.WriteLine("- 보상-\n\t쓸만한 방패 x 1\n\t5G\n");
                    Console.WriteLine(quest1Accepted ? "1. 이미 수락된 퀘스트입니다." : "1. 수락");
                    Console.WriteLine("2. 거절\n\n");

                    Console.WriteLine("0. 나가기\n");

                    break;
                case 2:
                    // 퀘스트 2에 대한 설명 추가
                    break;
                case 3:
                    // 퀘스트 3에 대한 설명 추가
                    break;
                default:
                    break;
            }

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input;
            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    switch (input)
                    {
                        case 0:
                            Console.WriteLine("\n이전 화면으로 돌아갑니다.\n");
                            return;
                        case 1:
                            if (quest1Accepted)
                            {
                                Console.WriteLine("\n이미 수락된 퀘스트입니다.\n");
                            }
                            else
                            {
                                Console.WriteLine("\n퀘스트를 수락했습니다.\n");
                                quest1Accepted = true;
                            }
                            break;
                        case 2:
                            Console.WriteLine("\n퀘스트를 거절했습니다.\n");
                            break;
                        default:
                            Console.WriteLine("올바른 번호를 입력해주세요.");
                            break;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("올바른 번호를 입력해주세요.");
                }
            }
        }

        // 미니언 처치 퀘스트 진행 상황 업데이트 메서드
        public static void UpdateMinionCount()
        {
            if (quest1Accepted) // 퀘스트 1이 수락되어 있는 경우에만 처리
            {
                quest1MinionCount++; // 미니언 처치 개수 증가
                Console.WriteLine($"미니언 5마리 처치: {quest1MinionCount}/5"); // 현재 진행 상황 출력
            }
        }
    }
}
