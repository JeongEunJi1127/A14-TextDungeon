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
        public static Monster senaMon;

        public static Skill[] skills = new Skill[]
        {
                new Skill ("알파 스트라이크", "공격력 * 2 로 하나의 적을 공격합니다.", 10),
                new Skill ("더블 스트라이크", "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.", 15)
        };

        public static float maxHp; 
        public static int maxMp;
        public static void  Init()
        {
            user = new User("UserName",1,"전사", 10, 5, 100, 50, 1500, false );
            maxHp = user.HP;
            maxMp = user.MP;

            minion = new Monster("미니언", 2, 5, 15, false );
            vacuity = new Monster("공허충", 3, 9, 10, false);
            siegeMinion = new Monster("대포미니언 ", 5, 8, 25, false);
            senaMon = new Monster("세나몬 ", 10, 20, 50, false);
        }

        static void Main(string[] args)
        {
            string userName = User.SetName();
            Console.WriteLine("저장된 이름:" + userName);

            Thread.Sleep(3000);



            Init();
            Village.ShowVillage();
        }

        public enum UserJob
        {
            Warrior = 0,
            Rogue = 1,
        }
    }
}
