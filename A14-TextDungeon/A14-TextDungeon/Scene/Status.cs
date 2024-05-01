using A14_TextDungeon.Manager;
using A14_TextDungeon.Data;

namespace A14_TextDungeon.Scene
{
    public class Status
    {
        public static void ShowStatus()
        {
            Console.Clear();
            RefreshPlayerStatus();

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

        static void RefreshPlayerStatus()
        {
            List<Item> items = Inventory.items;
            int weaponAttack = 0;
            int armorDefense = 0;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].IsEquippd)
                {
                    if (items[i].ItemType == ItemType.Weapon)
                    {
                        weaponAttack += items[i].ItemStat;
                    }
                    else if (items[i].ItemType == ItemType.Armor)
                    {
                        armorDefense += items[i].ItemStat;
                    }
                }              
            }

            Console.WriteLine("\n== 상태창 ==\n");
            Console.WriteLine($"LV. {GameManager.user.Level}");
            Console.WriteLine($"{GameManager.user.Name} ({GameManager.user.Job})");
            if( weaponAttack > 0 )
            {
                Console.WriteLine($"공격력 : {GameManager.user.AttackPower+ weaponAttack} (+{weaponAttack})");
            }
            else
            {
                Console.WriteLine($"공격력 : {GameManager.user.AttackPower}");
            }

            if (armorDefense > 0)
            {
                Console.WriteLine($"방어력 : {GameManager.user.Defense + armorDefense} (+{armorDefense})");
            }
            else
            {
                Console.WriteLine($"방어력 : {GameManager.user.Defense}");
            }

            Console.WriteLine($"체력: {GameManager.user.HP} ");
            Console.WriteLine($"Gold : {GameManager.user.Gold} G");
            Console.WriteLine($"EXP : {GameManager.user.Exp}/{GameManager.user.MaxExp}\n\n");

            Console.WriteLine("0. 나가기");
        }
    }
}
