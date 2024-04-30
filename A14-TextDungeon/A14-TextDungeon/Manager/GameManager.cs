using A14_TextDungeon.Data;
using A14_TextDungeon.Scene;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using static A14_TextDungeon.Data.User;

namespace A14_TextDungeon.Manager
{
    internal class GameManager
    {
        public static User user;
        
        public static Monster minion;
        public static Monster vacuity;
        public static Monster siegeMinion;
        public static Monster senaMon;

        public static Skill[] skills = new Skill[]
        {
                new Skill ("알파 스트라이크", "공격력 * 2 로 하나의 적을 공격합니다.", 10),
                new Skill ("더블 스트라이크", "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.", 15)
        };

        public static string saveName;

        public static float maxHp; 
        public static int maxMp;
        public static void  Init()
        {
            //user = new User("UserName",1,"전사", 10, 5, 100, 50, 1500, false );
            maxHp = user.HP;
            maxMp = user.MP;

            minion = new Monster("미니언", 2, 5, 15, false );
            vacuity = new Monster("공허충", 3, 9, 10, false);
            siegeMinion = new Monster("대포미니언 ", 5, 8, 25, false);
            senaMon = new Monster("세나몬 ", 10, 20, 50, false);
        }

        static void Main(string[] args)
        {
            SetName();

           /* Init();
            Village.ShowVillage();*/
        } 

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
            user = new User(saveName, level, job, attackPower, defense, hp, mp, gold, isdead);
            Init();
            Village.ShowVillage();
        }

    }
}
