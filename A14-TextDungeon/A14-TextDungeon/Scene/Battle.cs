using A14_TextDungeon.Data;
using A14_TextDungeon.Manager;

namespace A14_TextDungeon.Scene
{
    public class Battle
    {
        public static Monster[] monsters = new Monster[3];
        public int monsterCount = 0;

        // Battle 화면 보여주는 함수
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

            // 랜덤하게 뽑힌 세마리의 몬스터 
            Console.WriteLine($"LV.{monsters[0].Level} {monsters[0].Name} HP {monsters[0].HP}");
            Console.WriteLine($"LV.{monsters[1].Level} {monsters[1].Name} HP {monsters[1].HP}");
            Console.WriteLine($"LV.{monsters[2].Level} {monsters[2].Name} HP {monsters[2].HP}\n");

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

        // Player 페이즈 함수 - 몬스터 죽이면 monsterCount += 1 필요
        public void PlayerPhase()
        {
            Console.WriteLine("Battle!!\n");
        }

        // Enemy 페이즈 함수 
        public void EnemyPhase()
        {
            Console.WriteLine("Battle!!\n");
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

        // 플레이어 스탯 보여주는 함수
        public static void ShowPlayerStat()
        {
            Console.WriteLine("[내정보]");
            Console.WriteLine($"LV.{GameManager.user.Level}  Chad ({GameManager.user.Job})");
            Console.WriteLine($"HP {GameManager.user.HP}/{GameManager.maxHp}\n");
        }
    }
}
