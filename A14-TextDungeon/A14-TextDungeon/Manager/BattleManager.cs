using A14_TextDungeon.UI;
using  A14_TextDungeon.Scene;
using A14_TextDungeon.Data;

namespace A14_TextDungeon.Manager
{
    internal class BattleManager
    {
        public static Monster[] monsters = new Monster[3];
        public static int monsterCount = 0;

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
                    BattleManager.monsterCount++;
                }
                Console.WriteLine($"HP {nowMonsterHp} -> {monster.HP}\n");
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

            foreach (int i in monsterNum)
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

        // 게임 끝났는지 검사
        public static bool BattleEnd()
        {
            if (GameManager.user.IsDead)
            {
                // 파일 리셋 로직 구현 필요
                Battle.BattleResult(false);
                return true;
            }
            else if (monsters[0].IsDead && monsters[1].IsDead && monsters[2].IsDead)
            {
                Battle.BattleResult(true);
                return true;
            }
            return false;
        }
    }
}