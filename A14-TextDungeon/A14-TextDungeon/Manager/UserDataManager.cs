namespace A14_TextDungeon
{
    public class UserDataManager
    {
        public string saveName;
        public void SetName()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 이름을 설정해주세요.\n");

            string userName = Console.ReadLine();

            Console.WriteLine($"입력하신 이름은 {userName} 입니다.\n");
            Console.WriteLine("1. 저장");
            Console.WriteLine("2. 취소\n");

            SetNameInput(userName);
        }

        public void SetJob()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 직업을 설정해주세요.\n");
            Console.WriteLine($"1. {Manager.Instance.gameManager.jobStat[0].Name}" +
                            $"\tHP:{Manager.Instance.gameManager.jobStat[0].HP}" +
                            $"\tMP:{Manager.Instance.gameManager.jobStat[0].MP}" +
                            $"\t공격력:{Manager.Instance.gameManager.jobStat[0].AttackPower}" +
                            $"   방어력:{Manager.Instance.gameManager.jobStat[0].Defense}");
            Console.WriteLine($"2. {Manager.Instance.gameManager.jobStat[1].Name}" +
                            $"\tHP:{Manager.Instance.gameManager.jobStat[1].HP}" +
                            $"\tMP:{Manager.Instance.gameManager.jobStat[1].MP}" +
                            $"\t공격력:{Manager.Instance.gameManager.jobStat[1].AttackPower}" +
                            $"   방어력:{Manager.Instance.gameManager.jobStat[1].Defense}");

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
            Console.WriteLine("원하는 직업을 입력해주세요.\n");
            int input;
            bool isValidNum = int.TryParse(Console.ReadLine(), out input);

            if (isValidNum)
            {                
                User selectJob = Manager.Instance.gameManager.jobStat[input - 1];

                int level = selectJob.Level;
                User.UserJob userJob = selectJob.Job;                
                float attackPower = selectJob.AttackPower;
                float defense = selectJob.Defense;
                float hp = selectJob.HP;
                float mp = selectJob.MP;
                int gold = selectJob.Gold;
                bool isdead = selectJob.IsDead;

                switch (userJob)
                {
                    case User.UserJob.Warrior: 
                    case User.UserJob.Rogue:                        
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
            Manager.Instance.gameManager.user = new User(saveName, level, job, attackPower, defense, hp, mp, gold, isdead);
            Manager.Instance.gameManager.Init();
            Manager.Instance.gameManager.village.ShowVillage();
        }
    }
}
