using A14_TextDungeon.Data;
using A14_TextDungeon.Manager;

namespace A14_TextDungeon.Scene
{
    public class Battle
    {
        public static Monster[] monsters = new Monster[3];
        public static void ShowBattle()
        {
            Console.WriteLine("Battle!!\n");

            // monster 변수에 랜덤한 몬스터 종류 할당
            Random rand  = new Random();

            for(int i = 0;i<3;i++)
            {
                int monsterNum = rand.Next(0, 3);

                switch (monsterNum)
                {
                    case 0:
                        monsters[i] = GameManager.minion;
                        break;
                    case 1:
                        monsters[i] = GameManager.vacuity;
                        break;
                    case 2:
                        monsters[i] = GameManager.siegeMinion;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine($"LV.{monsters[0].Level} {monsters[0].Name} HP {monsters[0].HP}");
            Console.WriteLine($"LV.{monsters[1].Level} {monsters[1].Name} HP {monsters[1].HP}");
            Console.WriteLine($"LV.{monsters[2].Level} {monsters[2].Name} HP {monsters[2].HP}\n");

            ShowPlayerStat();

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
                            Village.ShowVillage(); 
                            break;
                        case 1:
                            // 공격함수
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

        public static void ShowPlayerStat()
        {
            Console.WriteLine("[내정보]");
            Console.WriteLine($"LV.{GameManager.user.Level}  Chad ({GameManager.user.Job})");
            Console.WriteLine($"HP {GameManager.user.HP}/{GameManager.maxHp}\n");
        }

        //몬스터 턴 실행
        static void EnemyPhase()
        {
            Console.Clear();
            Console.WriteLine("Battle !!");

            int i = 0;
            while(true)
            {
                //임시 데이터
                float monsterDamage = (float)Math.Ceiling(monsters[i].AttackPower * 1.1f);

                if (monsters[i].IsDead)
                {
                    return;
                }
                else
                {
                    Console.WriteLine($"LV.{monsters[i].Level} {monsters[i].Name}의 공격 !");
                    Console.WriteLine($"{GameManager.user.Name}을(를) 맞췄습니다. [데미지 : {monsterDamage}]");
                    Console.WriteLine();
                    Console.WriteLine($"LV.{GameManager.user.Level} {GameManager.user.Name}");
                    Console.WriteLine($"HP {GameManager.user.HP} -> {GameManager.user.HP - monsterDamage}");
                    //GameManager.user.HP Set 변경
                    //GameManager.user.HP -= monsterDamage;
                    Console.WriteLine("0. 다음");
                    int input;
                    bool isValidNum = int.TryParse(Console.ReadLine(), out input);

                    if (isValidNum)
                    {
                        switch (input)
                        {
                            case 0:
                                i++;
                                break;
                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;
                        }
                    }                    
                }
                if (i == monsters.Length)
                {
                    break;
                }
            }
            //플레이어 턴 실행
            //PlayerPhase
            Console.Clear();
            Console.WriteLine("플레이어턴 실행");
            Thread.Sleep(1000);
        }
    }
}
