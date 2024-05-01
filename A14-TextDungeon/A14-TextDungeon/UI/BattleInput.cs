using A14_TextDungeon.Manager;
using A14_TextDungeon.Scene;

namespace A14_TextDungeon.UI
{
    internal class BattleInput
    {
        public static void ShowBattleInput()
        {
            int input;

            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    switch (input)
                    {
                        case 1:
                            Console.Clear();
                            Battle.PlayerPhase();
                            break;
                        case 2:
                            Console.Clear();
                            Battle.SkillStatus();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }
        }

        public static void PlayerPhaseInput()
        {
            int input;

            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    if (input == 0)
                    {
                        Console.Clear();
                        Battle.ShowBattle(false);
                    }
                    else if (1 <= input && input <= BattleManager.monsters.Count)
                    {
                        if (BattleManager.monsters[input - 1].IsDead)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                        else
                        {
                            BattleManager.PlayerAttack(input - 1);
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }
        }

        public static void PlayerAttackInput()
        {
            int input;

            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    if (input == 0)
                    {
                        if (BattleManager.BattleEnd())
                        {
                            Thread.Sleep(1000);
                            Village.ShowVillage();
                        }
                        else
                        {
                            if(BattleManager.stageNum == 4)
                            {
                                Boss.BossPhase();
                            }
                            else
                            {
                                //몬스터 턴 실행
                                Battle.EnemyPhase();
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }
        }

        public static void EnemyPhaseInput()
        {
            int input;

            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    if (input == 0)
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }
        }

        public static void BossPhaseInput()
        {
            int input;

            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    if (input == 0)
                    {
                        Battle.PlayerPhase();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }
        }

        public static void BattleResultInput()
        {
            int input;

            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    switch (input)
                    {
                        case 0:
                            Village.ShowVillage();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }
        }

        public static void SkillStatusInput()
        {
            int input;
            Console.WriteLine("사용할 스킬을 선택해주세요.\n");

            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    switch (input)
                    {
                        case 0:
                            Battle.ShowBattle(false);
                            break;
                        case 1:
                            if (GameManager.user.MP >= 10)
                            {
                                Console.WriteLine("공격할 몬스터를 선택해주세요.\n");
                                ChooseMonsterInput();
                            }
                            else
                            {
                                Console.WriteLine("MP가 부족합니다.\n");
                            }
                            break;
                        case 2:
                            if (GameManager.user.MP >= 15)
                            {
                                Console.WriteLine("공격할 몬스터를 선택해주세요.\n");
                                Console.Clear();
                                Console.WriteLine("몬스터 2마리를 랜덤으로 공격합니다.\n");

                                Random random = new Random();
                                List<int> randomNumbers = new List<int>();
                                int cnt = BattleManager.monsters.Count(monster => !monster.IsDead);

                                if (cnt < 2)
                                {
                                    for (int i = 0; i < BattleManager.monsters.Count; i++)
                                    {
                                        if (!BattleManager.monsters[i].IsDead)
                                        {
                                            randomNumbers.Add(i);
                                        }
                                    }
                                }
                                else
                                {
                                    while (randomNumbers.Count < 2)
                                    {
                                        int randomNum = random.Next(0, BattleManager.monsters.Count);
                                        // 살아있는 몬스터 중 랜덤한 몬스터 2마리 뽑기
                                        if (!randomNumbers.Contains(randomNum) && !BattleManager.monsters[randomNum].IsDead)
                                        {
                                            randomNumbers.Add(randomNum);
                                        }
                                    }
                                    randomNumbers.Sort();
                                }
                                Thread.Sleep(1000);
                                BattleManager.PlayerSkill(randomNumbers, 2);
                            }
                            else
                            {
                                Console.WriteLine("MP가 부족합니다.\n");
                            }
                            break;

                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }
        }

        public static void ChooseMonsterInput()
        {
            int input;

            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    if (input == 0)
                    {
                        Console.Clear();
                        Battle.SkillStatus();
                    }
                    else if (1 <= input && input <= BattleManager.monsters.Count)
                    {
                        if (BattleManager.monsters[input - 1].IsDead)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                        else
                        {
                            List<int> list = new List<int>() { input - 1 };
                            BattleManager.PlayerSkill(list, 1);
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }
        }
    }
}
