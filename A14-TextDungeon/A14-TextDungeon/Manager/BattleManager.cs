using A14_TextDungeon.UI;
using  A14_TextDungeon.Scene;
using A14_TextDungeon.Data;

namespace A14_TextDungeon.Manager
{
    internal class BattleManager
    {
        public static Monster[] monsters = new Monster[3];
        public static int monsterCount = 0;
        public static int monsterExp = 0;
        public static int goldReward = 0;
        static List<Item> rewards = new List<Item>();

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
                    GiveReward(monster);                   
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

        //보상
        public static void GiveReward(Monster monster)
        {
            monsterExp += monster.Level;
            goldReward = monster.Level * 100;

            Random random = new Random();
            int itemChance = random.Next(1, 101);
            Console.WriteLine($"\n[RandomNum] {itemChance}\n");
            if (itemChance <= 30)
            {
                // 아이템 없음               
            }
            else if (itemChance <= 60)
            {
                // 포션 1개
                rewards.Add(new Item("포션", 5, ItemType.Potion, "HP를 회복해주는 포션이다"));
                Inventory.AddItem(new Item("포션", 5, ItemType.Potion, "HP를 회복해주는 포션이다"));
                
            }
            else if (itemChance <= 80)
            {
                rewards.Add(new Item("포션2", 5, ItemType.Potion, "HP를 회복해주는 포션이다"));
                Inventory.AddItem(new Item("포션2", 5, ItemType.Potion, "HP를 회복해주는 포션이다"));
            }
            else if(itemChance <=100)
            {
                switch (monster.Name) 
                {
                    case "미니언":
                        Inventory.AddItem(new Item("미니언의 지팡이", 3, ItemType.Weapon, "미니언이 가지고있던 지팡이이다."));
                        rewards.Add(new Item("미니언의 지팡이", 3, ItemType.Weapon, "미니언이 가지고있던 지팡이이다."));
                        break;
                    case "공허충":
                        Inventory.AddItem(new Item("공허충 비늘 갑옷", 4, ItemType.Armor, "공허충의 비늘로 만든 갑옷이다."));
                        rewards.Add(new Item("공허충 비늘 갑옷", 4, ItemType.Armor, "공허충의 비늘로 만든 갑옷이다."));
                        break;
                    case "대포미니언":
                        Inventory.AddItem(new Item("대포미니언의 대포", 6, ItemType.Weapon, "대포미니언이 타고있던 대포이다."));
                        rewards.Add(new Item("대포미니언의 대포", 6, ItemType.Weapon, "대포미니언이 타고있던 대포이다."));
                        break;

                }
            }
        }

        public static void ShowReward()
        {
            Console.WriteLine("\n[보상 목록]\n");
            Console.WriteLine($"몬스터를 잡고 경험치를 {monsterExp}획득했습니다!\n");
            Console.WriteLine("\n[획득 아이템]\n");
            if(rewards.Count == 0)
            {
                Console.WriteLine("획득한 아이템이 없습니다.");
            }
            else
            {
                for (int i = 0; i < rewards.Count; i++)
                {
                    Console.WriteLine($"{rewards[i].ItemName} - 1\n");
                }
            }
            

            rewards.Clear();
        }

    }
}