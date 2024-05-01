using A14_TextDungeon.Data;
using A14_TextDungeon.Manager;
using A14_TextDungeon.UI;

namespace A14_TextDungeon.Scene
{
    internal class Boss
    {
        public static bool isFirst = true;
        public static void BossInit()
        {
            BattleManager.monsters.Clear();
            BattleManager.monsters.Add(new Monster("세나몬", 10, 10, 100, false));
            BossStage();
        }

        public static void BossStage()
        {
            Console.WriteLine("??? : 후후후..");
            Thread.Sleep(1000);
            Console.WriteLine("??? : 용케 여기까지 왔구나..");
            Thread.Sleep(1000);
            Console.WriteLine("??? : .....");
            Thread.Sleep(1000);
            Console.WriteLine("??? : .....");
            Thread.Sleep(1500);
            Console.WriteLine($"[{BattleManager.monsters[0].Name}] : 하지만.... 이제 끝이다!\n");
            Thread.Sleep(1000);
            Console.WriteLine($"[{BattleManager.monsters[0].Name}]이 나타났다!");
            Thread.Sleep(1000);
        }

        public static void BossPhase()
        { 
            Console.Clear();
            Console.WriteLine("보스 턴\n");

            // 광폭화 - 한 번만 발생
            if (BattleManager.monsters[0].HP < 30)
            {
                BossBerserk(isFirst);
                isFirst = false;
            }
            BossAttack();
        }

        public static void BossAttack()
        {
            // 보스 패턴 3개
            Random random = new Random();
            int attackType = random.Next(0, 2);
            float monsterDamage = BattleManager.monsters[0].AttackDamage(BattleManager.monsters[0].AttackPower);

            Console.WriteLine($"{BattleManager.monsters[0].Name}의 공격!");
            switch(attackType)
            {
                case 0:
                    // 기본 공격
                    Console.WriteLine($"LV.{BattleManager.monsters[0].Level} {BattleManager.monsters[0].Name}의 펀치!\n");
                    break;
                case 1:
                    // 스킬 공격 데미지 1.5배
                    Console.WriteLine($"LV.{BattleManager.monsters[0].Level} {BattleManager.monsters[0].Name}의 파이어볼!\n");
                    monsterDamage *= 1.5f;
                    break;
                default:
                    break;
            }

            Console.WriteLine($"{GameManager.user.Name}을(를) 맞췄습니다. [데미지 : {monsterDamage}]\n");
            Console.WriteLine($"LV.{GameManager.user.Level} {GameManager.user.Name}");

            float nowHp = GameManager.user.HP;
            GameManager.user.TakeDamage(monsterDamage);
            Console.WriteLine($"HP {nowHp} -> {GameManager.user.HP}\n");

            // 게임 끝나면 탈출
            if (GameManager.user.IsDead)
            {
                Console.WriteLine($"당신은 {BattleManager.monsters[0].Name}에게 패배했습니다..");
                Thread.Sleep(2000);
            }

            Console.WriteLine("0. 다음\n");
            BattleInput.EnemyPhaseInput();

            // 전투 종료 조건
            if (BattleManager.BattleEnd())
            {
                Thread.Sleep(1000);
                Village.ShowVillage();
            }
            else
            {
                //플레이어 턴 실행
                Battle.ShowBattle(false);
            }
        }

        public static void BossBerserk(bool isFirst)
        {
            if (isFirst)
            {
                Console.WriteLine($"[{BattleManager.monsters[0].Name}]이 치명타를 입었습니다.");
                Console.WriteLine($"[{BattleManager.monsters[0].Name}]의 현재 HP : {BattleManager.monsters[0].HP}");
                Thread.Sleep(1000);
                Console.WriteLine($"[{BattleManager.monsters[0].Name}] : .....");
                Thread.Sleep(1000);
                Console.WriteLine($"[{BattleManager.monsters[0].Name}] : .....");
                Thread.Sleep(1000);
                Console.WriteLine($"[{BattleManager.monsters[0].Name}] : .....아직 끝나지 않았다!\n");
                Thread.Sleep(1000);
                Console.WriteLine($"[{BattleManager.monsters[0].Name}]이 광폭화 합니다.");
                Thread.Sleep(800);
                Console.WriteLine($"[{BattleManager.monsters[0].Name}]의 Hp가 가득 찹니다.");
                Thread.Sleep(800);
                Console.WriteLine($"[{BattleManager.monsters[0].Name}]의 의 공격력이 20 오릅니다.\n");
                Thread.Sleep(800);

                BattleManager.monsters[0].Berserk();
            }
        }

       
        public static void BossBattleResult(bool isWin)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");

            // 플레이어가 이기면
            if (isWin)
            {
                Console.WriteLine("Victory\n");
                Console.WriteLine($"보스 [{BattleManager.monsters[0].Name}]과의 전투에서 승리했습니다!");
                BattleManager.ShowReward();
                Console.WriteLine("\n게임을 전부 클리어했습니다.\n");
                Console.WriteLine("다시 하시겠습니까?\n");
                Thread.Sleep(10000);
                //파일 초기화 구현하기
            }
            // 지면
            else
            {
                Console.WriteLine("You Lose");
                Console.WriteLine($"보스 [{BattleManager.monsters[0].Name}]과의 전투에서 패배했습니다..");
                Console.WriteLine("게임을 다시 하시겠습니까?\n");
                Thread.Sleep(10000);
                //파일 초기화 구현하기
            }
        }
    }
}
