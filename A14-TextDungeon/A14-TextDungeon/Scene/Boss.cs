namespace A14_TextDungeon
{
    public class Boss
    {
        public bool isFirst = true;
        public Monster bossMon;
        public void BossInit()
        {
            Manager.Instance.battleManager.monsters.Clear();    
            Manager.Instance.battleManager.monsters.Add(new Monster("진유록 마왕", 10, 30,15, 100, false));
            bossMon = Manager.Instance.battleManager.monsters[0];
            BossStage();
        }

        public void BossStage()
        {
            Console.WriteLine("??? : TIL 쓰셨어요?");
            Thread.Sleep(1000);
            Console.WriteLine("??? : 왜 대답이 없지? 그리고 왜 다들 캠 끄고 계세요?");
            Thread.Sleep(1000);
            Console.WriteLine("??? : .....");
            Thread.Sleep(1000);
            Console.WriteLine("??? : .....");
            Thread.Sleep(1500);
            Console.WriteLine($"??? : 대답...안해?\n");
            Thread.Sleep(1000);
            Console.WriteLine($"야생의 [{bossMon.Name}]이 나타났다!");
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
                    Console.WriteLine($"LV.{bossMon.Level} {bossMon.Name}의 TIL 저주!\n");
                    break;
                case 1:
                    // 스킬 공격 데미지 1.5배
                    Console.WriteLine($"LV.{bossMon.Level} {bossMon.Name}의 잔소리!\n");
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
                Console.WriteLine($"당신은 {bossMon.Name}에게 패배했습니다..\n\n");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"[{bossMon.Name}] : 빨리 TIL 쓰러 가시죠. 이따 검사합니다.");
                Console.WriteLine("-------------------------------------------------");
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
                Console.WriteLine($"[{bossMon.Name}] : .....아직도 캠을 안키셨네요?\n");
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
            Console.WriteLine("== Battle ==\n\n== Result ==\n");

            // 플레이어가 이기면
            if (isWin)
            {

                Console.WriteLine("\n== Victory ==\n");
                Console.WriteLine($"보스 [{bossMon.Name}]과의 전투에서 승리했습니다!\n\n");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"[{bossMon.Name}] : 이번 한 번만 넘어가 드립니다. 다음에 올때는 캠 키셔야 돼요.");
                Console.WriteLine("-------------------------------------------------\n\n");
                Manager.Instance.battleManager.ShowReward();
                Console.WriteLine("\n게임을 전부 클리어했습니다.\n");
            }
            // 지면
            else
            {
                Console.WriteLine("\n== You Lose ==");
                Console.WriteLine($"보스 [{bossMon.Name}]과의 전투에서 패배했습니다..\n\n");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"[{bossMon.Name}] : 빨리 TIL 쓰러 가시죠. 이따 검사합니다.");
                Console.WriteLine("-------------------------------------------------\n\n");
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
