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
                        case 0:
                            Console.Clear();
                            Village.ShowVillage();
                            break;
                        case 1:
                            Console.Clear();
                            Battle.PlayerPhase();
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
                    else if (1 <= input && input <= 3)
                    {
                        if (Battle.monsters[input - 1].IsDead)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                        else
                        {
                            Battle.PlayerAttack(input - 1);
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
                        if (Battle.GameEnd())
                        {
                            Thread.Sleep(1000);
                            Village.ShowVillage();
                        }
                        else
                        {
                            //몬스터 턴 실행
                            Battle.EnemyPhase();
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
    }
}
