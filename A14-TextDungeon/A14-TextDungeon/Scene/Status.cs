using A14_TextDungeon.Manager;

namespace A14_TextDungeon.Scene
{
    public class Status
    {
        public static void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("== 상태 보기 =");
            Console.WriteLine();
            Console.WriteLine("-");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine("-");
            Console.WriteLine();
            Console.WriteLine($"체력: {GameManager.user.HP} ");
            Console.WriteLine($"Gold : {GameManager.user.Gold} G");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            string input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    Console.Clear();
                    break;
            }
        }
    }
}
