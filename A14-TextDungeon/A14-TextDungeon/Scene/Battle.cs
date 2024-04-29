using A14_TextDungeon.Data;
using A14_TextDungeon.Manager;

namespace A14_TextDungeon.Scene
{
    public class Battle
    {
        public static Monster[] monsters = new Monster[3];
        public static int monsterCount = 0;

        // Battle 첫 화면
        public static void ShowBattle(bool isFirst)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            // Battle 화면으로 돌아올 때마다 랜덤한 몬스터 값이 뽑히는 경우 방지
            if (isFirst)
            {
                RandomMonster();
            }

            // 랜덤하게 뽑힌 세마리의 몬스터 
            for (int i = 0; i < 3; i++)
            {
                if (monsters[i].IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"LV.{monsters[i].Level} {monsters[i].Name}  Dead");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"LV.{monsters[i].Level} {monsters[i].Name}  HP {monsters[i].HP}");
                }
            }

            // 플레이어 레벨 & 직업 & Hp
            ShowPlayerStat();

            // 입력값 받기
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 뒤로 가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input;

            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
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
        static void RandomMonster()
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

        // Player 페이즈 함수 
        static void PlayerPhase()
        {
            Console.WriteLine("Battle!!\n");

            ShowMonsterStat();

            // 플레이어 스탯 보여주기
            ShowPlayerStat();

            Console.WriteLine("0. 취소\n");
            Console.WriteLine("대상을 선택해주세요.\n");

            int input;
            
            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
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
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                        else
                        {
                            PlayerAttack(input - 1);
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

        // 플레이어 공격화면
        static void PlayerAttack(int monsterNum)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            float damage = GameManager.user.AttackDamage(GameManager.user.AttackPower);

            Console.WriteLine($"{GameManager.user.Name}의 공격!");
            Console.WriteLine($"LV.{monsters[monsterNum].Level} {monsters[monsterNum].Name} 을(를) 맞췄습니다. [데미지 : {damage}]\n");
            Console.WriteLine($"LV.{monsters[monsterNum].Level} {monsters[monsterNum].Name}");
           
            if (monsters[monsterNum].IsDead)
            {
                Console.WriteLine($"HP {monsters[monsterNum].HP} -> Dead\n");
            }
            else
            {
                float nowMonsterHp = monsters[monsterNum].HP;
                monsters[monsterNum].TakeDamage(damage);
                if (monsters[monsterNum].IsDead)
                {
                    monsterCount++;
                }
                Console.WriteLine($"HP {nowMonsterHp} -> {monsters[monsterNum].HP}\n");
            }

            // 입력값
            Console.WriteLine("0. 다음\n");
            int input;

            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    if (input == 0)
                    {
                        if (GameEnd())
                        {
                            Thread.Sleep(1000);
                            Village.ShowVillage();
                        }
                        else
                        {
                            //몬스터 턴 실행
                            EnemyPhase();
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

        //몬스터 턴 실행
        static void EnemyPhase()
        {
            Console.Clear();

            for (int i = 0; i < 3; i++)
            {
                // 몬스터 데미지 계산
                float monsterDamage = monsters[i].AttackDamage(monsters[i].AttackPower);

                if (!monsters[i].IsDead)
                {
                    Console.WriteLine("Battle!!\n");
                    Console.WriteLine($"LV.{monsters[i].Level} {monsters[i].Name}의 공격 !");
                    Console.WriteLine($"{GameManager.user.Name}을(를) 맞췄습니다. [데미지 : {monsterDamage}]");
                    Console.WriteLine();
                    Console.WriteLine($"LV.{GameManager.user.Level} {GameManager.user.Name}");

                    float nowHp = GameManager.user.HP;
                    GameManager.user.TakeDamage(monsterDamage);
                    Console.WriteLine($"HP {nowHp} -> {GameManager.user.HP}\n");

                    // 게임 끝나면 함수 탈출
                    if (GameManager.user.IsDead)
                    {
                        Thread.Sleep(2000);
                        break;
                    }     
                   
                    // 입력값
                    Console.WriteLine("0. 다음");
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
            }
            if (GameEnd())
            {
                Thread.Sleep(1000);
                Village.ShowVillage();
            }
            else
            {
                //플레이어 턴 실행
                PlayerPhase();
            }
        }

        // 플레이어 스탯 보여주는 함수
        static void ShowPlayerStat()
        {
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"LV.{GameManager.user.Level}  Chad ({GameManager.user.Job})");
            Console.WriteLine($"HP {GameManager.user.HP}/{GameManager.maxHp}\n");
        }

        // 몬스터 스탯 보여주는 함수
        static void ShowMonsterStat()
        {
            // 각 몬스터 정보 출력 - 죽은 몬스터면 Dead 처리
            for (int i = 0; i < 3; i++)
            {
                if (monsters[i].IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{i + 1} LV.{monsters[i].Level} {monsters[i].Name} Dead");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1} LV.{monsters[i].Level} {monsters[i].Name} HP {monsters[i].HP}");
                }
            }
        }

        // 게임 끝났는지 검사
        static bool GameEnd()
        {
            if (GameManager.user.IsDead)
            {
                BattleResult(false);
                return true;
            }
            else if (monsters[0].IsDead && monsters[1].IsDead && monsters[2].IsDead)
            {
                BattleResult(true);
                return true;
            }
            return false;
        }


        // Battle 결과창 보여주는 함수
        static void BattleResult(bool isWin)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");

            // 플레이어가 이기면
            if (isWin)
            {
                Console.WriteLine("Victory\n");
                Console.WriteLine($"던전에서 몬스터 {monsterCount}마리를 잡았습니다.");
            }
            // 지면
            else
            {
                Console.WriteLine("You Lose");
            }

            ShowPlayerStat();

            // 입력값 받기
            Console.WriteLine("0. 다음\n");
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
