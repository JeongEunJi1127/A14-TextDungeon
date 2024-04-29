using A14_TextDungeon.Manager;

namespace A14_TextDungeon.Scene
{
    public class Status
    {
        public static void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("== 상태창 ==");
            Console.WriteLine();
            Console.WriteLine($"LV. {GameManager.user.Level}");
            Console.WriteLine($"{GameManager.user.Name} ({GameManager.user.Job})");
            Console.WriteLine($"공격력 : {GameManager.user.AttackPower}");
            Console.WriteLine($"공격력 : {GameManager.user.Defense}");
            Console.WriteLine($"체력: {GameManager.user.HP} ");
            Console.WriteLine($"Gold : {GameManager.user.Gold} G");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input;
            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    switch (input)
                    {
                        case 0:
                            Village.ShowVillage();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                }
            }

        }
    }
}
