namespace A14_TextDungeon { 
    
    public class Inventory
    {
        public void ShowEquipPage()
        {           
            Manager.Instance.inventoryManager.RefrshInventory(true);
            Console.WriteLine("장착하거나 장착을 해제하고싶은 장비의 번호를 입력하세요\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기\n");
            Console.ForegroundColor = ConsoleColor.White;
            ShowEquipPageInput();
            Item selectedItem = Manager.Instance.inventoryManager.items[Manager.Instance.inventoryManager.selectItemIndex];
            //items = 인벤토리 스크립트 안에 모여있는 것들(리스트)
            if (selectedItem.Itemtype == Item.ItemType.HPPotion || selectedItem.Itemtype == Item.ItemType.MPPotion)
            {
                Console.WriteLine("아쉽지만 포션은 장착 할 수 없습니다..");
                Thread.Sleep(1000);
                Console.Clear();
                ShowInventory();
            }
            else
            {
                if (selectedItem.IsEquippd)
                {
                    Manager.Instance.inventoryManager.UnEquipItem(selectedItem);
                    Console.WriteLine($"\n{selectedItem.ItemName}이(가) 장착 해제되었습니다");
                    Thread.Sleep(1000);
                    ShowEquipPage();
                }
                else
                {
                    Manager.Instance.inventoryManager.EquippedItemCheck(selectedItem);
                    Console.WriteLine($"\n{selectedItem.ItemName}이(가) 장착되었습니다");
                    Thread.Sleep(1000);
                    ShowEquipPage();
                }
            }

                
        }

        public void ShowInventory()
        {
            Manager.Instance.inventoryManager.RefrshInventory(false);
            Console.WriteLine("\n1. 장착관리\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기\n");
            Console.ForegroundColor = ConsoleColor.White;
            ShowInventoryInput();
        }

        public void ShowEquipPageInput()
        {
            int input;
            int index;

            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                index = input - 1;
                if (input == 0)
                {
                    Manager.Instance.gameManager.inventory.ShowInventory();
                    return;
                }
                else if (index < 0 || index >= Manager.Instance.inventoryManager.items.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    ShowEquipPageInput();
                }
                else
                {
                    Manager.Instance.inventoryManager.selectItemIndex = index;
                }
            }
        }

        public void ShowInventoryInput()
        {
            int input;

            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                switch (input)
                {
                    case 0:
                        Manager.Instance.gameManager.village.ShowVillage();
                        return;
                    case 1:
                        Manager.Instance.gameManager.inventory.ShowEquipPage();
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        ShowInventoryInput();
                        break;
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                ShowInventoryInput();
            }
        }
    }   
}
