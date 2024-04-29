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
        public static void Init()
        {
            user = new User("UserName", 1, "전사", 10, 5, 100, 1500, false);
            maxHp = user.HP;

            minion = new Monster("미니언", 2, 5, 15, false);
            vacuity = new Monster("공허충", 3, 9, 10, false);
            siegeMinion = new Monster("대포미니언 ", 5, 8, 25, false);
        }
        static void Main(string[] args)
        {
            Init();
            Village.ShowVillage();
        }
    }
}
