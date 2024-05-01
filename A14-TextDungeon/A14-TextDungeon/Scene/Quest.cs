using System;

namespace A14_TextDungeon.Scene
{
    public class Quest
    {
        // 퀘스트 제목과 설명 속성
        public string Title { get; set; }
        public string Description { get; set; }

        // 퀘스트 상태를 저장하는 변수들
        public bool Quest1Accepted { get; private set; } = false; // 퀘스트 1이 수락되었는지 여부를 저장하는 변수
        private int quest1MinionCount = 0; // 퀘스트 1의 미니언 처치 개수를 저장하는 변수

        // 퀘스트 생성자
        public Quest(string title, string description)
        {
            Title = title;
            Description = description;
        }

        // 퀘스트 상세 정보 출력 메서드
        public void ShowDetails()
        {
            Console.WriteLine($"퀘스트 제목: {Title}");
            Console.WriteLine($"퀘스트 설명: {Description}");
        }

        // 퀘스트 선택 화면 출력 메서드
        public void ShowQuests()
        {
            // 퀘스트 목록 출력
            Console.WriteLine("Quest!!\n");
            Console.WriteLine(GetQuestStatusText(1, "마을을 위협하는 미니언 처치"));
            Console.WriteLine(GetQuestStatusText(2, "장비를 장착해보자"));
            Console.WriteLine(GetQuestStatusText(3, "더욱 더 강해지기!\n"));
            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");

            // 입력 처리
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

        // 퀘스트 상태 텍스트 생성 메서드
        private string GetQuestStatusText(int questNumber, string questName)
        {
            // 퀘스트의 상태에 따라 텍스트 반환
            if (questNumber == 1) // 예시로 퀘스트 1만 처리
            {
                if (Quest1Accepted)
                {
                    return $"1. {questName}(진행중)";
                }
                else
                {
                    return $"{questNumber}. {questName}";
                }
            }
            else
            {
                return $"{questNumber}. {questName}";
            }
        }

        // 퀘스트 상세 정보 출력 메서드
        private void ShowQuestDetails(int questNumber)
        {
            switch (questNumber)
            {
                case 1:
                    // 퀘스트 1 상세 정보 출력
                    Console.WriteLine("\n마을을 위협하는 미니언 처치\n");
                    Console.WriteLine("이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?");
                    Console.WriteLine("마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!");
                    Console.WriteLine("모험가인 자네가 좀 처치해주게!\n");
                    Console.WriteLine($"- 미니언 5마리 처치 ({quest1MinionCount}/5)\n");
                    Console.WriteLine(Quest1Accepted ? "1. 이미 수락된 퀘스트입니다." : "1. 수락");
                    Console.WriteLine("2. 거절\n\n");

                    // 수락 또는 거절 선택
                    Console.WriteLine("원하시는 행동을 입력해주세요.");

                    int input;
                    bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                    if (isValidNum)
                    {
                        switch (input)
                        {
                            case 1:
                                if (Quest1Accepted)
                                {
                                    Console.WriteLine("\n이미 수락된 퀘스트입니다.\n");
                                }
                                else
                                {
                                    Console.WriteLine("\n퀘스트를 수락했습니다.\n");
                                    Quest1Accepted = true;
                                }
                                break;
                            case 2:
                                Console.WriteLine("\n퀘스트를 거절했습니다.\n");
                                break;
                            default:
                                Console.WriteLine("올바른 번호를 입력해주세요.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("올바른 번호를 입력해주세요.");
                    }
                    break;
                // 다른 퀘스트들의 상세 정보 추가
                default:
                    break;
            }
        }

        // 미니언 처치 퀘스트 진행 상황 업데이트 메서드
        public void UpdateMinionCount()
        {
            // 퀘스트가 수락되어 있는 경우에만 처리
            if (Quest1Accepted)
            {
                // 미니언 처치 개수 증가
                quest1MinionCount++;
                // 현재 진행 상황 출력
                Console.WriteLine($"미니언 5마리 처치: {quest1MinionCount}/5");

                // 완료 조건 충족 시 보상 받기 활성화
                if (quest1MinionCount >= 5)
                {
                    Console.WriteLine("\n보상을 받을 수 있습니다!");
                }
            }
        }
    }
}
