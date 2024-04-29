using A14_TextDungeon.Data;
using A14_TextDungeon.Manager;

namespace A14_TextDungeon.Scene
{
    public class Battle
    {
        public static Monster[] monsters = new Monster[3];
        public int monsterCount = 0;

        // Battle 화면 보여주는 함수
        public static void ShowBattle(bool isFirst)
        {
            Console.WriteLine("Battle!!\n");

            // Battle 화면에 처음 들어오면 - 화면으로 돌아올 때마다 랜덤한 몬스터 값이 뽑히는 경우 방지
            if (isFirst)
            {
                RandomMonster();
            }

            // 랜덤하게 뽑힌 세마리의 몬스터 
            Console.WriteLine($"LV.{monsters[0].Level} {monsters[0].Name} HP {monsters[0].HP}");
            Console.WriteLine($"LV.{monsters[1].Level} {monsters[1].Name} HP {monsters[1].HP}");
            Console.WriteLine($"LV.{monsters[2].Level} {monsters[2].Name} HP {monsters[2].HP}");

            // 플레이어 레벨 & 직업 & Hp
            ShowPlayerStat();

            // 입력값 받기
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 뒤로 가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input;
            bool isValidNum = int.TryParse( Console.ReadLine(), out input );

            while (true)
            {
                if (isValidNum)
                {
                    switch(input)
                    {
                        case 0:
                            Console.Clear();
                            Village.ShowVillage(); 
                            break;
                        case 1:
                            Console.Clear();
                            PlayerPhase();
                            break;
                        default :
                            Console.WriteLine("1잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }
        }
        public static void RandomMonster()
        {
            // monster 변수에 랜덤한 몬스터 종류 할당
            Random rand = new Random();

            for (int i = 0; i < 3; i++)
            {
                int monsterNum = rand.Next(0, 3);

                switch (monsterNum)
                {
                    case 0:
                        monsters[i] = new Monster("미니언", 2, 5, 15, false);
                        break;
                    case 1:
                        monsters[i] = new Monster("공허충", 3, 9, 10, false);
                        break;
                    case 2:
                        monsters[i] = new Monster("대포미니언 ", 5, 8, 25, false);
                        break;
                    default:
                        break;
                }
            }
        }

        // Player 페이즈 함수 - 몬스터 죽이면 monsterCount += 1 필요
        public static void PlayerPhase()
        {
            Console.WriteLine("Battle!!\n");

            ShowMonsterStat();

            // 플레이어 스탯 보여주기
            ShowPlayerStat();

            Console.WriteLine("0. 취소\n");
            Console.WriteLine("대상을 선택해주세요.\n");

            int input;
            bool isValidNum = int.TryParse (Console.ReadLine(), out input );

            while (true)
            {
                if (isValidNum)
                {
                    if (input == 0)
                    {
                        Console.Clear();
                        ShowBattle(false);
                    }
                    else if (1 <= input && input <= 3)
                    {
                        if (monsters[input - 1].IsDead)
                        {
                            Console.WriteLine("2잘못된 입력입니다.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            monsters[input - 1].TakeDamage(GameManager.user.AttackPower);
                            ShowMonsterStat();
                            Thread.Sleep(2000);
                            EnemyPhase();
                        }
                    }
                    else
                    {
                        Console.WriteLine("3잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
                break;
            }
        }

        // Battle 결과창 보여주는 함수
        public void BattleResult(bool isWin)
        {
            Console.WriteLine("Battle!! - Result\n");

            // 플레이어가 이기면
            if(isWin)
            {
                Console.WriteLine("Victory\n");
                Console.WriteLine($"던전에서 몬스터 {monsterCount}마리를 잡았습니다.\n");
            }
            else
            {
                Console.WriteLine("You Lose\n");
            }

            ShowPlayerStat();

            // 입력값 받기
            Console.WriteLine("0. 다음\n");

            int input;
            bool isValidNum = int.TryParse(Console.ReadLine(), out input);

            while (true)
            {
                if (isValidNum)
                {
                    switch (input)
                    {
                        case 0:
                            Village.ShowVillage();
                            break;
                        default:
                            Console.WriteLine("4잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }
        }

        // 플레이어 스탯 보여주는 함수
        public static void ShowPlayerStat()
        {
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"LV.{GameManager.user.Level}  Chad ({GameManager.user.Job})");
            Console.WriteLine($"HP {GameManager.user.HP}/{GameManager.maxHp}\n");
        }

        public static void ShowMonsterStat()
        {
            // 각 몬스터 정보 출력 - 죽은 몬스터면 Dead 처리
            for (int i = 0; i < 3; i++)
            {
                if (monsters[i].IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{i + 1} LV. {monsters[i].Level} {monsters[i].Name}  Dead");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1} LV. {monsters[i].Level} {monsters[i].Name}  HP {monsters[i].HP}");
                }
            }
        }

        //몬스터 턴 실행
        static void EnemyPhase()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < 3; i++)
            {
                float monsterDamage = (float)Math.Ceiling(monsters[i].AttackPower * 1.1f);

                if (!monsters[i].IsDead)
                {
                    Console.WriteLine($"LV.{monsters[i].Level} {monsters[i].Name}의 공격 !");
                    Console.WriteLine($"{GameManager.user.Name}을(를) 맞췄습니다. [데미지 : {monsterDamage}]");
                    Console.WriteLine();
                    Console.WriteLine($"LV.{GameManager.user.Level} {GameManager.user.Name}");
                    Console.WriteLine($"HP {GameManager.user.HP} -> {GameManager.user.HP - monsterDamage}");

                    GameManager.user.HP -= monsterDamage;

                    Console.WriteLine("0. 다음");

                    int input;
                    bool isValidNum = int.TryParse(Console.ReadLine(), out input);

                    while (true)
                    {
                        if (isValidNum)
                        {
                            if (input == 0)
                            {
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        }
                        break;
                    }
                }
            }

            //플레이어 턴 실행
            PlayerPhase();
        }
    }
}
