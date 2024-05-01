using A14_TextDungeon.Data;
using A14_TextDungeon.Manager;
using A14_TextDungeon.UI;
using static A14_TextDungeon.Data.User;

namespace A14_TextDungeon.Scene
{
    public class Battle
    {
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

            ShowMonsterStat(false);
            ShowPlayerStat();

            // 입력값 받기
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            BattleInput.ShowBattleInput();
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
                        BattleManager.monsters[i] = new Monster("미니언", 2, 5, 15, false);
                        break;
                    case 1:
                        BattleManager.monsters[i] = new Monster("공허충", 3, 9, 10, false);
                        break;
                    case 2:
                        BattleManager.monsters[i] = new Monster("대포미니언 ", 5, 8, 25, false);
                        break;
                    default:
                        break;
                }
            }
        }

        // Player 페이즈 구현 
        public static void PlayerPhase()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - 나의 턴\n");

            ShowMonsterStat(true);
            ShowPlayerStat();

            Console.WriteLine("0. 취소\n");
            Console.WriteLine("대상을 선택해주세요.\n");
            
            BattleInput.PlayerPhaseInput();
        }

        // Enemy 페이즈 구현
        public static void EnemyPhase()
        {
            Console.Clear();

            for (int i = 0; i < BattleManager.monsters.Length; i++)
            {
                // 몬스터 데미지 계산
                float monsterDamage = BattleManager.monsters[i].AttackDamage(BattleManager.monsters[i].AttackPower);

                if (!BattleManager.monsters[i].IsDead)
                {
                    Console.WriteLine("Battle!! - 몬스터 턴\n");
                    Console.WriteLine($"LV.{BattleManager.monsters[i].Level} {BattleManager.monsters[i].Name}의 공격 !");
                    Console.WriteLine($"{GameManager.user.Name}을(를) 맞췄습니다. [데미지 : {monsterDamage}]\n");
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

                    BattleInput.EnemyPhaseInput();
                }
            }

            // 전투 종료 조건
            if (BattleManager.BattleEnd())
            {
                Thread.Sleep(1000);
                Village.ShowVillage();
            }
            else
            {
                //플레이어 턴 실행
                ShowBattle(false);
            }
        }

        // 스킬창 구현
        public static void SkillStatus()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            Battle.ShowMonsterStat(true);
            Battle.ShowPlayerStat();

            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"LV.{GameManager.user.Level}  Chad ({GameManager.user.Job})");
            Console.WriteLine($"HP {GameManager.user.HP}/{GameManager.maxHp}");
            Console.WriteLine($"MP {GameManager.user.MP}/{GameManager.maxMp}\n");

            int userJob = 0;

            switch (GameManager.user.Job)
            {
                case "전사":
                    userJob = (int)UserJob.Warrior;                    
                    break;
                case "도적":
                    userJob = (int)UserJob.Rogue;                    
                    break;
                default:
                    break;
            }

            // userJob 값에 따라 스킬 설명이 다르게 출력됨.
            for (int i = 0; i < GameManager.skillList[userJob - 1].Length; i++)
            {
                Console.WriteLine($"{i + 1}. {GameManager.skillList[userJob - 1][i].Name} - MP {GameManager.skillList[userJob - 1][i].MP}");
                Console.WriteLine($"   {GameManager.skillList[userJob - 1][i].Description}");
            }

            Console.WriteLine("0. 취소\n");
            
            switch((UserJob)userJob)
            {
                case UserJob.Warrior:
                    BattleInput.SkillStatusInput();
                    break;
                case UserJob.Rogue:
                    BattleInput.SkillStatusRogueInput();
                    break;
            }

            
        }



        // 플레이어 스탯 보여주는 함수
        public static void ShowPlayerStat()
        {
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"LV.{GameManager.user.Level}  Chad ({GameManager.user.Job})");
            Console.WriteLine($"HP {GameManager.user.HP}/{GameManager.maxHp}");
            Console.WriteLine($"MP {GameManager.user.MP}/{GameManager.maxMp}\n");
        }

        // 몬스터 스탯 보여주는 함수
        public static void ShowMonsterStat(bool showNum)
        {
            for (int i = 0; i < 3; i++)
            {
                if (showNum)
                {
                    if (BattleManager.monsters[i].IsDead)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{i + 1} LV.{BattleManager.monsters[i].Level} {BattleManager.monsters[i].Name} Dead");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1} LV.{BattleManager.monsters[i].Level} {BattleManager.monsters[i].Name} HP {BattleManager.monsters[i].HP}");
                    }
                }
                else
                {
                    if (BattleManager.monsters[i].IsDead)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"LV.{BattleManager.monsters[i].Level} {BattleManager.monsters[i].Name}  Dead");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"LV.{BattleManager.monsters[i].Level} {BattleManager.monsters[i].Name}  HP {BattleManager.monsters[i].HP}");
                    }
                }
            }
        }


        // Battle 결과창 보여주는 함수 - 게임이 끝나면 실행
        public static void BattleResult(bool isWin)
        {
            GameManager.user.MP += 10;

            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");

            // 플레이어가 이기면
            if (isWin)
            {
                Console.WriteLine("Victory\n");
                Console.WriteLine($"던전에서 몬스터 {BattleManager.monsterCount}마리를 잡았습니다.");
                BattleManager.ShowReward();
            }
            // 지면
            else
            {
                Console.WriteLine("You Lose");
            }
            GameManager.user.LevelUP(BattleManager.monsterExp);
            ShowPlayerStat();

            // 입력값 받기
            Console.WriteLine("0. 다음\n");

            BattleInput.BattleResultInput();
        }
    }
}
