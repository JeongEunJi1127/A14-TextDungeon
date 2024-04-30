using A14_TextDungeon.Manager;
using A14_TextDungeon.Scene;

namespace A14_TextDungeon.Data
{
    public class User
    {

        public int Level { get; private set; }
        public int Gold { get; private set; }
        public float AttackPower { get; private set; }
        public float Defense { get; private set; }
        public float HP { get; set; }
        public int MP { get; set; }

        public string Name { get; private set; }
        public string Job { get; private set; }
        public bool IsDead { get; private set; }

        public User(string name, int level, string job, float attackPower, float defense, float hp, int mp, int gold, bool isDead)
        {
            Name = name;
            Level = level;
            Job = job;
            AttackPower = attackPower;
            Defense = defense;
            HP = hp;
            MP = mp;
            Gold = gold;
            IsDead = isDead;
        }

        public float AttackDamage(float damage)
        {
            int range = (int)MathF.Ceiling((damage / 10));
            Random random = new Random();
            damage = random.Next((int)(damage - range), (int)(damage + range + 1));

            return damage;
        }

        public void TakeDamage(float damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                HP = 0;
                Die();
            }
        }

        public void LevelUp()
        {
            Console.WriteLine($"[레벨업!]\n현재 플레이어 레벨 : {Level}");
        }

        public void Die()
        {
            IsDead = true;
        }

        public static string SetName()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 이름을 설정해주세요.\n");

            string userName = Console.ReadLine();

            Console.WriteLine($"입력하신 이름은 {userName} 입니다.\n");
            Console.WriteLine("1. 저장");
            Console.WriteLine("2. 취소\n");

            return SetNameInput(userName);
        }

        public static void SetJob()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 직업을 설정해주세요.\n");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 도적\n");

            SetJobInput();
        }


        public static string SetNameInput(string userName)
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
                            // 플레이어 이름 저장
                            Console.Clear();
                            return userName;
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

        public static int SetJobInput()
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
                            return input;
                            
                        /*case 2:
                            Console.Clear();
                            Battle.ShowBattle(true);
                            break;
                        default:
                            Console.WriteLine("\n잘못된 입력입니다.\n");
                            break;*/
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
