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
        public static Inventory inventory;

        public static Skill[] skills = new Skill[]
        {
                new Skill ("알파 스트라이크", "공격력 * 2 로 하나의 적을 공격합니다.", 10),
                new Skill ("더블 스트라이크", "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.", 15)
        };   

        public static float maxHp; 
        public static int maxMp;

        // 저장해야 할 파일 초기화 - 유저 정보, 인벤토리 정보, 상점 정보 등?
        public static void  Init()
        {
           

            maxHp = user.HP;
            maxMp = user.MP;
          

            //Item testItem1 = new Item("무쇠갑옷", 5, ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다.");
            //Item testItem2 = new Item("스파르타의 창", 7, ItemType.Weapon, "스파르타의 전사들이 사용했다는 전설의 창입니다.");
            //Item testItem3 = new Item("낡은 검", 2, ItemType.Weapon, "쉽게 볼 수 있는 낡은 검 입니다.");
            //Inventory.AddItem(testItem1);
            //Inventory.AddItem(testItem2);
            //Inventory.AddItem(testItem3);
        }

        static void Main(string[] args)
        {
            UserDataManager.SetName();
        }         
    }
}
