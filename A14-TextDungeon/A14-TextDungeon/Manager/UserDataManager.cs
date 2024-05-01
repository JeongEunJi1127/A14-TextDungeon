using A14_TextDungeon.Data;
using A14_TextDungeon.Scene;
using static A14_TextDungeon.Data.User;

namespace A14_TextDungeon.Manager
{
    internal class UserDataManager
    {
        public static string saveName;
        public static void SetName()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 이름을 설정해주세요.\n");

            string userName = Console.ReadLine();

            Console.WriteLine($"입력하신 이름은 {userName} 입니다.\n");
            Console.WriteLine("1. 저장");
            Console.WriteLine("2. 취소\n");

            SetNameInput(userName);
        }

        public static void SetJob()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 직업을 설정해주세요.\n");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 도적\n");

            SetJobInput();
        }

        public static void SetNameInput(string userName)
        {
            while (true)
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
                            break;
                        case 2:
                            SetName();
                            break;
                        default:
                            Console.WriteLine("\n잘못된 입력입니다.\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\n숫자를 입력해주세요.\n");
                }
            }
        }


        public static void SetJobInput()
        {
            while (true)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.\n");
                int input;
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);

                UserJob userJob = (UserJob)input;

                if (isValidNum)
                {
                    int level = 1;

                    string job;
                    float attackPower;
                    float defense;
                    float hp;
                    int mp;

                    int gold = 1500;
                    bool isdead = false;

                    switch (userJob)
                    {
                        case UserJob.Warrior:
                            job = "전사";
                            attackPower = 10;
                            defense = 5;
                            hp = 100;
                            mp = 50;

                            CreateUser(level, job, attackPower, defense, hp, mp, gold, isdead);
                            break;
                        case UserJob.Rogue:
                            job = "도적";
                            attackPower = 15;
                            defense = 3;
                            hp = 80;
                            mp = 70;

                            CreateUser(level, job, attackPower, defense, hp, mp, gold, isdead);
                            break;
                        default:
                            Console.WriteLine("\n잘못된 입력입니다.\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\n숫자를 입력해주세요.\n");
                }
            }
        }

        public static void CreateUser(int level, string job, float attackPower, float defense, float hp, int mp, int gold, bool isdead)
        {
            Console.Clear();
            GameManager.user = new User(saveName, level, job, attackPower, defense, hp, mp, gold, isdead);
            GameManager.Init();
            Village.ShowVillage();
        }
    }
}
