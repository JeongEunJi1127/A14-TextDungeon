using A14_TextDungeon.Scene;

namespace A14_TextDungeon.UI
{
    internal class InventoryInput
    {
        public static void ShowEquipPageInput()
        {
            int input;
            int index;
            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    index = input - 1;
                    if (input == 0)
                    {
                        Inventory.ShowInventory();
                    }
                    else if (index < 0 || index >= Inventory.items.Count)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        ShowEquipPageInput();
                    }
                    else
                    {
                        Inventory.selectItemIndex = index;
                        break;
                    }
                }
            }
        }

        public static void ShowInventoryInput()
        {
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
                        case 1:
                            Inventory.ShowEquipPage();
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
