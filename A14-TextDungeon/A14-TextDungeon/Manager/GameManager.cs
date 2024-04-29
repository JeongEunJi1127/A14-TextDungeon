using A14_TextDungeon.Data;
using A14_TextDungeon.Scene;

namespace A14_TextDungeon.Manager
{
    internal class GameManager
    {
        public void  Init()
        {
            User user = new User("UserName",1,"전사", 10, 5, 100, 1500, false );

            Monster minion = new Monster("미니언", 2, 1, 15, false );
            Monster siegeMinion = new Monster("미니언", 2, 1, 15, false);
            Monster vacuity = new Monster("미니언", 2, 1, 15, false);
        }
        static void Main(string[] args)
        {
            Village.ShowVillage();
        }
    }
}
