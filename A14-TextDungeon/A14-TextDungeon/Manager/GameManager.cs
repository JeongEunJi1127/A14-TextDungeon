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

        public static Inventory inventory;
        public static void  Init()
        {
            user = new User("UserName",1,"전사", 10, 5, 100, 1500, false );
            maxHp = user.HP;

            minion = new Monster("미니언", 2, 5, 15, false );
            vacuity = new Monster("공허충", 3, 9, 10, false);
            siegeMinion = new Monster("대포미니언 ", 5, 8, 25, false);

            inventory = new Inventory();
            Item testItem1 = new Item("무쇠갑옷", 5, ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다.");
            Item testItem2 = new Item("스파르타의 창", 7, ItemType.Weapon, "스파르타의 전사들이 사용했다는 전설의 창입니다.");
            Item testItem3 = new Item("낡은 검", 2, ItemType.Weapon, "쉽게 볼 수 있는 낡은 검 입니다.");
            inventory.AddItem(testItem1);
            inventory.AddItem(testItem2);
            inventory.AddItem(testItem3);

        }

        static void Main(string[] args)
        {
            Init();
            Village.ShowVillage();
        }
    }
}
