namespace A14_TextDungeon
{
    public class Battle
    {
        // Battle 첫 화면
        public void ShowBattle(bool isFirst)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            // Battle 화면으로 돌아올 때마다 랜덤한 몬스터 값이 뽑히는 경우 방지
            if (isFirst)
            {
                if (Manager.Instance.gameManager.user.StageNum == 4)
                {
                    Manager.Instance.gameManager.boss.BossInit();
                }
                else
                {
                    Manager.Instance.battleManager.RandomMonster(Manager.Instance.gameManager.user.StageNum);
                }
            }

            ShowMonsterStat(false);
            ShowPlayerStat();

            // 입력값 받기
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 포션 먹기\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("4. 도망가기\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            ShowBattleInput();
        }

        // Player 페이즈 구현 
        public void PlayerPhase()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - 나의 턴\n");

            ShowMonsterStat(true);
            ShowPlayerStat();

            Console.WriteLine("0. 취소\n");
            Console.WriteLine("대상을 선택해주세요.\n");

            PlayerPhaseInput();
        }

        // Enemy 페이즈 구현
        public void EnemyPhase()
        {
            Console.Clear();

            Manager.Instance.battleManager.EnemyAttack();

            // 전투 종료 조건
            if (Manager.Instance.battleManager.BattleEnd())
            {
                Thread.Sleep(1000);
                Manager.Instance.gameManager.village.ShowVillage();
            }
            else
            {
                //플레이어 턴 실행
                ShowBattle(false);
            }
        }

        // 스킬창 구현
        public void SkillStatus()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            Manager.Instance.gameManager.battle.ShowMonsterStat(true);
            Manager.Instance.gameManager.battle.ShowPlayerStat();

            int userJob = 0;

            switch (Manager.Instance.gameManager.user.Job)
            {
                case User.UserJob.Warrior:
                    userJob = (int)User.UserJob.Warrior;
                    break;
                case User.UserJob.Rogue:
                    userJob = (int)User.UserJob.Rogue;
                    break;
                default:
                    break;
            }

            // userJob 값에 따라 스킬 설명이 다르게 출력됨.
            for (int i = 0; i < Manager.Instance.gameManager.skillList[userJob - 1].Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Manager.Instance.gameManager.skillList[userJob - 1][i].Name} - MP {Manager.Instance.gameManager.skillList[userJob - 1][i].MP}");
                Console.WriteLine($"   {Manager.Instance.gameManager.skillList[userJob - 1][i].Description}");
            }

            Console.WriteLine("0. 취소\n");

            switch ((User.UserJob)userJob)
            {
                case User.UserJob.Warrior:
                    SkillStatusInput();
                    break;
                case User.UserJob.Rogue:
                    SkillStatusRogueInput();
                    break;
            }
        }

        // 플레이어 스탯 보여주는 함수
        public void ShowPlayerStat()
        {
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"LV.{Manager.Instance.gameManager.user.Level} {Manager.Instance.gameManager.user.Name} ({Manager.Instance.gameManager.user.Job})");
            Console.WriteLine($"HP {Manager.Instance.gameManager.user.HP}/{Manager.Instance.gameManager.user.MaxHP}");
            Console.WriteLine($"MP {Manager.Instance.gameManager.user.MP}/{Manager.Instance.gameManager.user.MaxMP}\n");
        }

        // 몬스터 스탯 보여주는 함수
        public void ShowMonsterStat(bool showNum)
        {
            for (int i = 0; i < Manager.Instance.battleManager.monsters.Count; i++)
            {
                if (showNum)
                {
                    if (Manager.Instance.battleManager.monsters[i].IsDead)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{i + 1} LV.{Manager.Instance.battleManager.monsters[i].Level} {Manager.Instance.battleManager.monsters[i].Name} Dead");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1} LV.{Manager.Instance.battleManager.monsters[i].Level} {Manager.Instance.battleManager.monsters[i].Name} HP {Manager.Instance.battleManager.monsters[i].HP}");

                    }
                }
                else
                {
                    if (Manager.Instance.battleManager.monsters[i].IsDead)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"LV.{Manager.Instance.battleManager.monsters[i].Level} {Manager.Instance.battleManager.monsters[i].Name}  Dead");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"LV.{Manager.Instance.battleManager.monsters[i].Level} {Manager.Instance.battleManager.monsters[i].Name}  HP {Manager.Instance.battleManager.monsters[i].HP}");
                    }
                }
            }
        }

        // Battle 결과창 보여주는 함수 - 게임이 끝나면 실행
        public void BattleResult(bool isWin)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");

            // 플레이어가 이기면
            if (isWin)
            {
                Manager.Instance.gameManager.user.StageNum++;
                Console.WriteLine("Victory\n");
                Console.WriteLine($"던전에서 몬스터 {Manager.Instance.battleManager.monsterCount}마리를 잡았습니다.");
                Manager.Instance.battleManager.ShowReward();
                Console.WriteLine("\n전투가 끝나 MP를 10 회복합니다.\n");
                Manager.Instance.gameManager.user.MP += 10;
            }
            // 지면
            else
            {
                Console.WriteLine("You Lose");
                Console.WriteLine("Game Over");
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
                return;
            }
            Manager.Instance.gameManager.user.LevelUP(Manager.Instance.battleManager.monsterExp);
            ShowPlayerStat();

            // 입력값 받기
            Console.WriteLine("0. 다음\n");

            BattleResultInput();
        }

        public void ShowBattleInput()
        {
            int input;

            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        PlayerPhase();
                        return;
                    case 2:
                        Console.Clear();
                        SkillStatus();
                        return;
                    case 3:
                        Console.Clear();
                        Manager.Instance.battleManager.UsePotion();
                        return;
                    case 4:
                        Console.Clear();
                        Manager.Instance.battleManager.GiveUP();
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        ShowBattleInput();
                        break;
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                ShowBattleInput();
            }
        }

        public void PlayerPhaseInput()
        {
            int input;

            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                if (input == 0)
                {
                    Console.Clear();
                    Manager.Instance.gameManager.battle.ShowBattle(false);
                    return;
                }

                else if (1 <= input && input <= Manager.Instance.battleManager.monsters.Count)
                {
                    if (Manager.Instance.battleManager.monsters[input - 1].IsDead)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        PlayerPhaseInput();
                    }
                    else
                    {
                        Manager.Instance.battleManager.PlayerAttack(input - 1);
                        Thread.Sleep(1000);
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    PlayerPhaseInput();
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                PlayerPhaseInput();
            }
        }

        public void PlayerAttackInput()
        {
            int input;

            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                if (input == 0)
                {
                    if (Manager.Instance.battleManager.BattleEnd())
                    {
                        Thread.Sleep(1000);
                        Manager.Instance.gameManager.village.ShowVillage();
                    }
                    else
                    {
                        if (Manager.Instance.gameManager.user.StageNum == 4)
                        {
                            Manager.Instance.gameManager.boss.BossPhase();
                        }
                        else
                        {
                            //몬스터 턴 실행
                            Manager.Instance.gameManager.battle.EnemyPhase();
                        }       
                    }
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    PlayerAttackInput();
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                PlayerAttackInput();
            }
        }

        public void EnemyPhaseInput()
        {
            int input;

            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                if (input == 0)
                {
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    EnemyPhaseInput();
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                EnemyPhaseInput();
            }
        }

        public void BossPhaseInput()
        {
            int input;

            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                if (input == 0)
                {
                    Manager.Instance.gameManager.battle.PlayerPhase();
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    BossPhaseInput();
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                BossPhaseInput();
            }
        }

        public void BattleResultInput()
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
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        BattleResultInput();
                        break;
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                BattleResultInput();
            }
        }

        public void SkillStatusInput()
        {
            int input;
            Console.WriteLine("사용할 스킬을 선택해주세요.\n");

            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                switch (input)
                {
                    case 0:
                        Manager.Instance.gameManager.battle.ShowBattle(false);
                        return;
                    case 1:
                        if (Manager.Instance.gameManager.user.MP >= 10)
                        {
                            Console.WriteLine("공격할 몬스터를 선택해주세요.\n");
                            ChooseMonsterInput(1);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("MP가 부족합니다.\n");
                            SkillStatusInput();
                        }
                        break;
                    case 2:
                        if (Manager.Instance.gameManager.user.MP >= 15)
                        {
                            Console.WriteLine("공격할 몬스터를 선택해주세요.\n");
                            Console.Clear();
                            Console.WriteLine("몬스터 2마리를 랜덤으로 공격합니다.\n");

                            Random random = new Random();
                            List<int> randomNumbers = new List<int>();
                            int cnt = Manager.Instance.battleManager.monsters.Count(monster => !monster.IsDead);

                            if (cnt < 2)
                            {
                                for (int i = 0; i < Manager.Instance.battleManager.monsters.Count; i++)
                                {
                                    if (!Manager.Instance.battleManager.monsters[i].IsDead)
                                    {
                                        randomNumbers.Add(i);
                                    }
                                }
                            }
                            else
                            {
                                while (randomNumbers.Count < 2)
                                {
                                    int randomNum = random.Next(0, Manager.Instance.battleManager.monsters.Count);
                                    // 살아있는 몬스터 중 랜덤한 몬스터 2마리 뽑기
                                    if (!randomNumbers.Contains(randomNum) && !Manager.Instance.battleManager.monsters[randomNum].IsDead)
                                    {
                                        randomNumbers.Add(randomNum);
                                    }
                                }
                                randomNumbers.Sort();
                            }
                            Thread.Sleep(1000);
                            Manager.Instance.battleManager.PlayerSkill(randomNumbers, 2);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("MP가 부족합니다.\n");
                            SkillStatusInput();
                        }
                        break;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        SkillStatusInput();
                        break;
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                SkillStatusInput();
            }
        }

        public void SkillStatusRogueInput()
        {
            int input;
            Console.WriteLine("사용할 스킬을 선택해주세요.\n");

            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                switch (input)
                {
                    case 0:
                        Manager.Instance.gameManager.battle.ShowBattle(false);
                        return;
                    case 1:
                        if (Manager.Instance.gameManager.user.MP >= Manager.Instance.gameManager.skillList[(int)User.UserJob.Rogue - 1][0].MP)
                        {
                            Console.WriteLine("공격할 몬스터를 선택해주세요.\n");
                            ChooseMonsterInput(3);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("MP가 부족합니다.\n");
                            SkillStatusRogueInput();
                        }
                        break;
                    case 2:
                        if (Manager.Instance.gameManager.user.MP >= Manager.Instance.gameManager.skillList[(int)User.UserJob.Rogue - 1][1].MP)
                        {
                            Console.WriteLine("공격할 몬스터를 선택해주세요.\n");
                            ChooseMonsterInput(4);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("MP가 부족합니다.\n");
                            SkillStatusRogueInput();
                        }
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        SkillStatusRogueInput();
                        break;
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                SkillStatusRogueInput();
            }
        }

        public void ChooseMonsterInput(int skillNum)
        {
            int input;

            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                if (input == 0)
                {
                    Console.Clear();
                    Manager.Instance.gameManager.battle.SkillStatus();
                    return;
                }
                else if (1 <= input && input <= Manager.Instance.battleManager.monsters.Count)
                {
                    if (Manager.Instance.battleManager.monsters[input - 1].IsDead)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        ChooseMonsterInput(skillNum);
                    }
                    else
                    {
                        List<int> list = new List<int>() { input - 1 };
                        Manager.Instance.battleManager.PlayerSkill(list, skillNum);
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    ChooseMonsterInput(skillNum);
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                ChooseMonsterInput(skillNum);
            }
        }
    }
}
