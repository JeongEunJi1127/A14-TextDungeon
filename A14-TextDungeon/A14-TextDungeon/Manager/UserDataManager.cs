namespace A14_TextDungeon
{
    public class UserDataManager
    {
        public string saveName;
        public void SetName()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n세나의 전설");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("에 오신 용사님 환영합니다.\n\n용사님의 이름을 알려주세요.\n");
            string userName = Console.ReadLine();

            Console.WriteLine($"\n입력하신 이름은 {userName} 입니다.\n");
            Console.WriteLine("1. 저장");
            Console.WriteLine("2. 취소\n");

            SetNameInput(userName);
        }

        public void SetJob()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n세나의 전설");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("에 오신 용사님 환영합니다.\n\n용사님의 직업을 알려주세요.\n\n");
            Console.WriteLine($"1. {Manager.Instance.gameManager.jobStat[0].Name}\n" +
                            $"HP:{Manager.Instance.gameManager.jobStat[0].HP}\n" +
                            $"MP:{Manager.Instance.gameManager.jobStat[0].MP} \n" +
                            $"공격력:{Manager.Instance.gameManager.jobStat[0].AttackPower}\n" +
                            $"방어력:{Manager.Instance.gameManager.jobStat[0].Defense} \n");
            Console.WriteLine($"2. {Manager.Instance.gameManager.jobStat[1].Name}\n" +
                            $"HP:{Manager.Instance.gameManager.jobStat[1].HP}\n" +
                            $"MP:{Manager.Instance.gameManager.jobStat[1].MP}\n" +
                            $"공격력:{Manager.Instance.gameManager.jobStat[1].AttackPower}\n" +
                            $"방어력:{Manager.Instance.gameManager.jobStat[1].Defense}\n");

            SetJobInput();
        }

        public void SetNameInput(string userName)
        {
            int input;
            bool isValidNum = int.TryParse(Console.ReadLine(), out input);

            if (isValidNum)
            {
                switch (input)
                {
                    case 1:
                        saveName = userName;
                        SetJob();
                        return;
                    case 2:
                        SetName();
                        return;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다.\n");
                        SetNameInput(userName);
                        break;
                }
            }
            else
            {
                Console.WriteLine("\n숫자를 입력해주세요.\n");
                SetNameInput(userName);
            }
        }


        public void SetJobInput()
        {
            Console.WriteLine("\n원하는 직업을 입력해주세요.\n");
            int input;
            bool isValidNum = int.TryParse(Console.ReadLine(), out input);

            if (isValidNum)
            {
                
                switch (input)
                {
                    case (int)User.UserJob.Warrior:
                    case (int)User.UserJob.Rogue:

                        User selectJob = Manager.Instance.gameManager.jobStat[input - 1];

                        int level = selectJob.Level;
                        User.UserJob userJob = selectJob.Job;
                        float attackPower = selectJob.AttackPower;
                        float defense = selectJob.Defense;
                        float hp = selectJob.HP;
                        float mp = selectJob.MP;
                        int gold = selectJob.Gold;
                        bool isdead = selectJob.IsDead;

                        CreateUser(level, userJob, attackPower, defense, hp, mp, gold, isdead);
                        return;
                    default:
                        Console.WriteLine("\n잘못된 입력입니다.\n");
                        SetJobInput();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\n숫자를 입력해주세요.\n");
                SetJobInput();
            }
        }

        public void CreateUser(int level, User.UserJob job, float attackPower, float defense, float hp, float mp, int gold, bool isdead)
        {
            Console.Clear();
            Manager.Instance.gameManager.user = new User(saveName, level, job, attackPower, defense, hp, mp, gold, 1, isdead);
            Manager.Instance.fileManager.SaveData();
        }
    }
}
