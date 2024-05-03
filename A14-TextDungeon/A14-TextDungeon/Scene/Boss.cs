namespace A14_TextDungeon
{
    public class Boss
    {
        public bool isFirst = true;
        public Monster bossMon;
        public void BossInit()
        {
            Manager.Instance.battleManager.monsters.Clear();    
            Manager.Instance.battleManager.monsters.Add(new Monster("세나몬", 10, 30,15, 100, false));
            bossMon = Manager.Instance.battleManager.monsters[0];
            BossStage();
        }

        public void BossStage()
        {
            Console.WriteLine("??? : 후후후..");
            Thread.Sleep(1000);
            Console.WriteLine("??? : 용케 여기까지 왔구나..");
            Thread.Sleep(1000);
            Console.WriteLine("??? : .....");
            Thread.Sleep(1000);
            Console.WriteLine("??? : .....");
            Thread.Sleep(1500);
            Console.WriteLine($"[{bossMon.Name}] : 하지만.... 이제 끝이다!\n");
            Thread.Sleep(1000);
            Console.WriteLine($"[{bossMon.Name}]이 나타났다!");
            Thread.Sleep(1000);
        }

        public void BossPhase()
        { 
            Console.Clear();
            Console.WriteLine("보스 턴\n");

            // 광폭화 - 한 번만 발생
            if (bossMon.HP < 30)
            {
                BossBerserk(isFirst);
                isFirst = false;
            }
            BossAttack();
        }

        public void BossAttack()
        {
            // 보스 패턴 3개
            Random random = new Random();
            int attackType = random.Next(0, 2);
            float monsterDamage = bossMon.AttackDamage(bossMon.AttackPower);

            Console.WriteLine($"{bossMon.Name}의 공격!");
            switch(attackType)
            {
                case 0:
                    // 기본 공격
                    Console.WriteLine($"LV.{bossMon.Level} {bossMon.Name}의 펀치!\n");
                    break;
                case 1:
                    // 스킬 공격 데미지 1.5배
                    Console.WriteLine($"LV.{bossMon.Level} {bossMon.Name}의 파이어볼!\n");
                    monsterDamage *= 1.5f;
                    break;
                default:
                    break;
            }

            Console.WriteLine($"{Manager.Instance.gameManager.user.Name}을(를) 맞췄습니다. [데미지 : {monsterDamage}]\n");
            Console.WriteLine($"LV.{Manager.Instance.gameManager.user.Level} {Manager.Instance.gameManager.user.Name}");

            float nowHp = Manager.Instance.gameManager.user.HP;
            Manager.Instance.gameManager.user.TakeDamage(monsterDamage);
            Console.WriteLine($"HP {nowHp} -> {Manager.Instance.gameManager.user.HP}\n");

            // 게임 끝나면 탈출
            if (Manager.Instance.gameManager.user.IsDead)
            {
                Console.WriteLine($"당신은 {bossMon.Name}에게 패배했습니다..");
                Thread.Sleep(2000);
            }

            Console.WriteLine("0. 다음\n");
            Manager.Instance.gameManager.battle.EnemyPhaseInput();

            // 전투 종료 조건
            if (Manager.Instance.battleManager.BattleEnd())
            {
                Thread.Sleep(1000);
                Manager.Instance.gameManager.village.ShowVillage();
            }
            else
            {
                //플레이어 턴 실행
                Manager.Instance.gameManager.battle.ShowBattle(false);
            }
        }

        public void BossBerserk(bool isFirst)
        {
            if (isFirst)
            {
                Console.WriteLine($"[{bossMon.Name}]이 치명타를 입었습니다.");
                Console.WriteLine($"[{bossMon.Name}]의 현재 HP : {bossMon.HP}");
                Thread.Sleep(1000);
                Console.WriteLine($"[{bossMon.Name}] : .....");
                Thread.Sleep(1000);
                Console.WriteLine($"[{bossMon.Name}] : .....");
                Thread.Sleep(1000);
                Console.WriteLine($"[{bossMon.Name}] : .....아직 끝나지 않았다!\n");
                Thread.Sleep(1000);
                Console.WriteLine($"[{bossMon.Name}]이 광폭화 합니다.");
                Thread.Sleep(800);
                Console.WriteLine($"[{bossMon.Name}]의 Hp가 가득 찹니다.");
                Thread.Sleep(800);
                Console.WriteLine($"[{bossMon.Name}]의 의 공격력이 20 오릅니다.\n");
                Thread.Sleep(800);

                bossMon.Berserk();
            }
        }

       
        public void BossBattleResult(bool isWin)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");

            // 플레이어가 이기면
            if (isWin)
            {
                Console.WriteLine("Victory\n");
                Console.WriteLine($"보스 [{bossMon.Name}]과의 전투에서 승리했습니다!");
                Manager.Instance.battleManager.ShowReward();
                Console.WriteLine("\n게임을 전부 클리어했습니다.\n");
            }
            // 지면
            else
            {
                Console.WriteLine("You Lose");
                Console.WriteLine($"보스 [{bossMon.Name}]과의 전투에서 패배했습니다..");
                Console.WriteLine("Game Over");
            }
            Thread.Sleep(1000);
            Console.WriteLine("게임이 다시 시작됩니다.");
            Thread.Sleep(1000);
            Console.WriteLine("3");
            Thread.Sleep(1000);
            Console.WriteLine("2");
            Thread.Sleep(1000);
            Console.WriteLine("1");
            Thread.Sleep(1000);
            Manager.Instance.fileManager.ResetData();
            Thread.Sleep(2000);
            return;
        }
    }
}
