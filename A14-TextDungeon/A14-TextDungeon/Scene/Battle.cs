using A14_TextDungeon.Data;
using A14_TextDungeon.Manager;
using A14_TextDungeon.UI;
using static System.Net.Mime.MediaTypeNames;

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

            for (int i = 0; i < monsters.Length; i++)
            {
                // 몬스터 데미지 계산
                float monsterDamage = monsters[i].AttackDamage(monsters[i].AttackPower);

                if (!monsters[i].IsDead)
                {
                    Console.WriteLine("Battle!! - 몬스터 턴\n");
                    Console.WriteLine($"LV.{monsters[i].Level} {monsters[i].Name}의 공격 !");
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
            if (BattleEnd())
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

        // 공격 구현
        public static void PlayerAttack(int monsterNum)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - 나의 턴\n");
            Console.WriteLine($"{GameManager.user.Name}의 공격!");

            float damage = GameManager.user.AttackDamage(GameManager.user.AttackPower);

            // 회피 
            if (IsEvasion())
            {
                Console.WriteLine($"LV.{monsters[monsterNum].Level} {monsters[monsterNum].Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.\n");
                AttackMonster(monsters[monsterNum], 0);
            }
            // 회피하지 않고 공격할 수 있으면
            else
            {
                // 치명타 
                if (IsCritical())
                {
                    damage = (float)Math.Round(damage * 1.6f);
                    Console.WriteLine($"LV.{monsters[monsterNum].Level} {monsters[monsterNum].Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!\n");
                }
                else
                {
                    Console.WriteLine($"LV.{monsters[monsterNum].Level} {monsters[monsterNum].Name} 을(를) 맞췄습니다. [데미지 : {damage}]\n");
                }
                AttackMonster(monsters[monsterNum], damage);
            }

            Console.WriteLine("0. 다음\n");
            BattleInput.PlayerAttackInput();
        }

        // 몬스터 hp 깎는 함수
        public static void AttackMonster(Monster monster, float damage)
        {
            Console.WriteLine($"LV.{monster.Level} {monster.Name}");

            if (monster.IsDead)
            {
                Console.WriteLine($"HP {monster.HP} -> Dead\n");
            }
            else
            {
                float nowMonsterHp = monster.HP;
                monster.TakeDamage(damage);
                if (monster.IsDead)
                {
                    monsterCount++;
                }
                Console.WriteLine($"HP {nowMonsterHp} -> {monster.HP}\n");
            }
        }

        //스킬 공격 구현
        public static void PlayerSkill(List<int> monsterNum, int skillNum)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - 나의 턴\n");
            Console.WriteLine($"{GameManager.user.Name}의 스킬 공격!\n");

            float damage = 0.0f;

            // 알파 스트라이크
            if (skillNum == 1)
            {
                GameManager.user.MP -= 10;
                damage += GameManager.user.AttackPower * 2;
            }
            // 더블 스트라이크
            else if (skillNum == 2)
            {
                GameManager.user.MP -= 15;
                damage += GameManager.user.AttackPower * 1.5f;
            }
            
            foreach(int i in monsterNum)
            {
                float skillDamage = damage;
                // 치명타 
                if (IsCritical())
                {
                    skillDamage = (float)Math.Round(damage * 1.6f);
                    Console.WriteLine($"LV.{monsters[i].Level} {monsters[i].Name} 을(를) 맞췄습니다. [스킬 데미지 : {skillDamage}] - 치명타 공격!!\n");
                }
                else
                {
                    Console.WriteLine($"LV.{monsters[i].Level} {monsters[i].Name} 을(를) 맞췄습니다. [스킬 데미지 : {skillDamage}]\n");
                }

                AttackMonster(monsters[i], skillDamage);
            }

            Console.WriteLine("0. 다음\n");
            BattleInput.PlayerAttackInput();
        }

        // 스킬창 구현
        public static void SkillStatus()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            ShowMonsterStat(true);
            ShowPlayerStat();

            for(int i = 0; i < GameManager.skills.Length; i++)
            {
                Console.WriteLine($"{i+1}. {GameManager.skills[i].Name} - MP {GameManager.skills[i].MP}");
                Console.WriteLine($"   {GameManager.skills[i].Description}");
            }
            Console.WriteLine("0. 취소\n");

            BattleInput.SkillStatusInput();     
        }
        
        // 치명타
        public static bool IsCritical()
        {
            Random random = new Random();
            int percentage = random.Next(0, 100);

            if (percentage <= 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // 회피 
        public static bool IsEvasion()
        {
            Random random = new Random();
            int percentage = random.Next(0, 100);

            if (percentage <= 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // 플레이어 스탯 보여주는 함수
        static void ShowPlayerStat()
        {
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"LV.{GameManager.user.Level}  Chad ({GameManager.user.Job})");
            Console.WriteLine($"HP {GameManager.user.HP}/{GameManager.maxHp}");
            Console.WriteLine($"MP {GameManager.user.MP}/{GameManager.maxMp}\n");
        }

        // 몬스터 스탯 보여주는 함수
        static void ShowMonsterStat(bool showNum)
        {
            for (int i = 0; i < 3; i++)
            {
                if (showNum)
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
                else
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
            }
        }

        // 게임 끝났는지 검사
        public static bool BattleEnd()
        {
            if (GameManager.user.IsDead)
            {
                // 파일 리셋 로직 구현 필요
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


        // Battle 결과창 보여주는 함수 - 게임이 끝나면 실행
        static void BattleResult(bool isWin)
        {
            GameManager.user.MP += 10;

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

            BattleInput.BattleResultInput();
        }
    }
}
