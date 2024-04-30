using A14_TextDungeon.Data;
using A14_TextDungeon.Scene;

namespace A14_TextDungeon.Manager
{
    internal class GameManager
    {
        public static User user;
        public static Monster minion;
        public static Monster vacuity;
        public static Monster siegeMinion;

        public static float maxHp;

        public static string saveName;

        public static void  Init()
        {
            //user = new User("UserName",1,"전사", 10, 5, 100, 50, 1500, false );
            maxHp = user.HP;

            minion = new Monster("미니언", 2, 5, 15, false );
            vacuity = new Monster("공허충", 3, 9, 10, false);
            siegeMinion = new Monster("대포미니언 ", 5, 8, 25, false);
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

                if (isValidNum)
                {
                    switch (input)
                    {
                        case 1:
                            Console.Clear();
                            user = new User(saveName, 1, "전사", 10, 5, 100, 50, 1500, false);
                            Init();
                            Village.ShowVillage();
                            break;
                        case 2:
                            Console.Clear();
                            user = new User(saveName, 1, "도적", 10, 5, 100, 50, 1500, false);
                            Init();
                            Village.ShowVillage();
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

    }
}
