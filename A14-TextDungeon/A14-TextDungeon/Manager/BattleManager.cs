namespace A14_TextDungeon
{
    public class BattleManager
    {
        public List <Monster>monsters = new List<Monster>();
        // 지금까지 잡은 몬스터 개수
        public int slayedMonster = 0;
        // 스테이지 넘버
        public int stageNum = 1;
        // 몬스터 개수
        public int monsterCount = 1;
        // 몬스터 경험치
        public int monsterExp = 0;
        // 골드 보상
        public int totalGoldReward = 0;

        // 보상 리스트
        private List<Item> rewards = new List<Item>();
        // 골드 보상
        private int goldReward = 0;


        // 랜덤한 몬스터 뽑기
        public void RandomMonster(int stageNum)
        {
            monsters.Clear();
            // 몬스터 확률
            int[] percent = { 0, 50 };
            Random random = new Random();

            if (stageNum == 1)
            {
                // 1층 -  3마리, 미니언 & 공허충
                monsterCount = 3;
            }
            else if (stageNum == 2)
            {
                // 2층 -  3~4마리, 미니언 & 공허충 & 대포미니언
                monsterCount = random.Next(3, 5);
                percent[0] = 40;
                percent[1] = 80;
            }
            else if (stageNum == 3)
            {
                // 3층 - 4~5마리, 공허충, 대포 미니언 나올 확률 up
                monsterCount = random.Next(4, 6);
                percent[0] = 50;
                percent[1] = 90;
            }

            for (int i = 0; i < monsterCount; i++)
            {
                int randomMonsterNum = random.Next(0, 100);

                // percent[0] 확률로 대포미니언
                if (randomMonsterNum < percent[0])
                {
                    monsters.Add(new Monster("대포미니언 ", 5, 8, 7,20, false));
                }
                // percent[1] - percent[0] 확률로 공허충
                else if (randomMonsterNum < percent[1])
                {
                    monsters.Add(new Monster("공허충", 3, 6, 11, 10, false));
                }
                // 100 - percent[1] - percent[0] 확률로 미니언
                else
                {
                    monsters.Add(new Monster("미니언", 2, 5, 7, 10, false));
                }
            }
        }

        // 몬스터 hp 깎는 함수
        public void AttackMonster(Monster monster, float damage)
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
                    slayedMonster++;
                    GiveReward(monster);                   
                }
                Console.WriteLine($"HP {nowMonsterHp} -> {monster.HP}\n");
            }
        }

        // 공격 구현
        public void PlayerAttack(int monsterNum)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - 나의 턴\n");
            Console.WriteLine($"{Manager.Instance.gameManager.user.Name}의 공격!");

            float damage = Manager.Instance.gameManager.user.AttackDamage(Manager.Instance.gameManager.user.AttackPower);

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
                    damage = (float)Math.Round(Manager.Instance.gameManager.user.AttackDamage(Manager.Instance.gameManager.user.AttackPower * 1.6f));
                    Console.WriteLine($"LV.{monsters[monsterNum].Level} {monsters[monsterNum].Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!\n");
                }
                else
                {
                    Console.WriteLine($"LV.{monsters[monsterNum].Level} {monsters[monsterNum].Name} 을(를) 맞췄습니다. [데미지 : {damage}]\n");
                }
                 AttackMonster(monsters[monsterNum], damage);
            }

            Console.WriteLine("0. 다음\n");
            Manager.Instance.gameManager.battle.PlayerAttackInput();
        }

        public void EnemyAttack()
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                // 몬스터 데미지 계산
                float monsterDamage = monsters[i].AttackDamage(monsters[i].AttackPower);

                if (!monsters[i].IsDead)
                {
                    Console.WriteLine("Battle!! - 몬스터 턴\n");
                    Console.WriteLine($"LV.{monsters[i].Level} {monsters[i].Name}의 공격 !");
                    Console.WriteLine($"{Manager.Instance.gameManager.user.Name}을(를) 맞췄습니다. [데미지 : {monsterDamage}]\n");
                    Console.WriteLine($"LV.{Manager.Instance.gameManager.user.Level} {Manager.Instance.gameManager.user.Name}");

                    float nowHp = Manager.Instance.gameManager.user.HP;
                    Manager.Instance.gameManager.user.TakeDamage(monsterDamage);
                    Console.WriteLine($"HP {nowHp} -> {Manager.Instance.gameManager.user.HP}\n");

                    // 게임 끝나면 탈출
                    if (Manager.Instance.gameManager.user.IsDead)
                    {
                        Console.WriteLine("당신은 죽었습니다..");
                        Thread.Sleep(2000);
                        break;
                    }

                    // 입력값
                    Console.WriteLine("0. 다음");
                    Manager.Instance.gameManager.battle.EnemyPhaseInput();
                }
            }
        }

        //스킬 공격 구현
        public void PlayerSkill(List<int> monsterNum, int skillNum)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - 나의 턴\n");
            Console.WriteLine($"{Manager.Instance.gameManager.user.Name}의 스킬 공격!\n");

            float damage = 0.0f;

            // 알파 스트라이크
            if (skillNum == 1)
            {
                Manager.Instance.gameManager.user.MP -= 10;
                damage += Manager.Instance.gameManager.user.AttackPower * 2;
            }
            // 더블 스트라이크
            else if (skillNum == 2)
            {
                Manager.Instance.gameManager.user.MP -= 15;
                damage += Manager.Instance.gameManager.user.AttackPower * 1.5f;
            }
            else if (skillNum == 3)
            {
                Manager.Instance.gameManager.user.MP -= 10;
                damage += Manager.Instance.gameManager.user.AttackPower +10f;
            }
            else if (skillNum == 4)
            {
                Manager.Instance.gameManager.user.MP -= 20;
                damage += Manager.Instance.gameManager.user.AttackPower * 1.5f;

                Manager.Instance.gameManager.user.Gold += (int)damage;
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
            Manager.Instance.gameManager.battle.PlayerAttackInput();
        }

        // 전투 포기
        public void GiveUP()
        {
            // 몬스터 잡으면 보상 바로 들어가는지 확인 -> 바로 들어가면 리스트로 관리
            Console.WriteLine("전투를 포기하시겠습니까? HP가 회복되지 않으며 보상을 얻을 수 없습니다.");
            Console.WriteLine("0. 포기하기 1. 계속하기");

            while (true)
            {
                int input;
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);

                if (isValidNum)
                {
                    switch (input)
                    {
                        case 0:
                            Manager.Instance.gameManager.village.ShowVillage();
                            return;
                        case 1:
                            Manager.Instance.gameManager.battle.ShowBattle(false);
                            return;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요");
                }
            }
        }

        // 치명타
        public bool IsCritical()
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
        public bool IsEvasion()
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
        public bool BattleEnd()
        {
            List<Monster> deadMonsters = monsters.Where(x => x.IsDead).ToList();

            if(stageNum == 4)
            {
                if (Manager.Instance.gameManager.user.IsDead)
                {
                    Manager.Instance.gameManager.boss.BossBattleResult(false);
                    return true;
                }
                else if (deadMonsters.Count == monsters.Count)
                {
                    Manager.Instance.gameManager.boss.BossBattleResult(true);
                    return true;
                }
            }
            else
            {
                if (Manager.Instance.gameManager.user.IsDead)
                {
                    // 파일 리셋 로직 구현 필요
                    Manager.Instance.gameManager.battle.BattleResult(false);
                    return true;
                }
                else if (deadMonsters.Count == monsters.Count)
                {
                    // 1~3층에서 클리어 & 플레이어가 모든 몬스터를 죽였을 때 보상 주기
                    foreach (Item reward in rewards)
                    {
                        Manager.Instance.inventoryManager.AddItem(reward);
                    }

                    Manager.Instance.gameManager.battle.BattleResult(true);
                    return true;
                }
            }
            return false;
        }

        //보상
        public void GiveReward(Monster monster)
        {
            monsterExp += monster.Level;
            goldReward = monster.Level * 100;
            totalGoldReward += goldReward;

            Random random = new Random();
            int itemChance = random.Next(1, 101);
            

            if (itemChance <= 30)
            {
                // 아이템 없음               
            }
            else if (itemChance <= 55)
            {
                // 포션 1개
                rewards.Add(new Item("HP회복 포션", 5, Item.ItemType.HPPotion, "HP를 회복해주는 포션이다")); 
            }
            else if (itemChance <= 80)
            {
                rewards.Add(new Item("MP회복 포션", 5, Item.ItemType.MPPotion, "HP를 회복해주는 포션이다"));
            }
            else if(itemChance <=100)
            {
                switch (monster.Name) 
                {
                    case "미니언":
                        rewards.Add(new Item("미니언의 지팡이", 3, Item.ItemType.Weapon, "미니언이 가지고있던 지팡이이다."));
                        break;
                    case "공허충":
                        rewards.Add(new Item("공허충 비늘 갑옷", 4, Item.ItemType.Armor, "공허충의 비늘로 만든 갑옷이다."));
                        break;
                    case "대포미니언":
                        rewards.Add(new Item("대포미니언의 대포", 6, Item.ItemType.Weapon, "대포미니언이 타고있던 대포이다."));
                        break;

                }
            }
        }

        public void ShowReward()
        {
            Console.WriteLine("\n[보상 목록]\n");
            Console.WriteLine($"몬스터를 잡고 경험치를 {monsterExp}획득했습니다!");
            Console.WriteLine($"몬스터를 잡고 골드를 {totalGoldReward}획득했습니다!\n");
            Manager.Instance.gameManager.user.Gold += totalGoldReward;
            Console.WriteLine("\n[획득 아이템]\n");         
            if (rewards.Count == 0)
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