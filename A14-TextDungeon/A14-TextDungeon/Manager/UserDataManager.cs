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
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 도적\n");

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
            Console.WriteLine("원하시는 행동을 입력해주세요.\n");
            int input;
            bool isValidNum = int.TryParse(Console.ReadLine(), out input);

            if (isValidNum)
            {
                int level = 1;
                User.UserJob userJob = (User.UserJob)input;
                string job;
                float attackPower;
                float defense;
                float hp;
                int mp;

                int gold = 1500;
                bool isdead = false;

                switch (userJob)
                {
                    case User.UserJob.Warrior:
                        job = "전사";
                        attackPower = 10;
                        defense = 5;
                        hp = 100;
                        mp = 50;

                        CreateUser(level, User.UserJob.Warrior, attackPower, defense, hp, mp, gold, isdead);
                        return;
                    case User.UserJob.Rogue:
                        job = "도적";
                        attackPower = 15;
                        defense = 3;
                        hp = 800;
                        mp = 70;

                        CreateUser(level, User.UserJob.Rogue, attackPower, defense, hp, mp, gold, isdead);
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

        public void CreateUser(int level, User.UserJob job, float attackPower, float defense, float hp, int mp, int gold, bool isdead)
        {
            Console.Clear();
            Manager.Instance.gameManager.user = new User(saveName, level, job, attackPower, defense, hp, mp, gold, isdead);
            Manager.Instance.gameManager.Init();
            Manager.Instance.gameManager.village.ShowVillage();
        }
    }
}
