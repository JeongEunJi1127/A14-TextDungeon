﻿namespace A14_TextDungeon { 
    
    public class Inventory
    {
        public void ShowEquipPage()
        {           
            Manager.Instance.inventoryManager.RefrshInventory(true);
            Console.WriteLine("장착하거나 장착을 해제하고싶은 장비의 번호를 입력하세요\n");
            Console.WriteLine("0. 나가기\n");
            ShowEquipPageInput();
            Item selectedItem = Manager.Instance.inventoryManager.items[Manager.Instance.inventoryManager.selectItemIndex];
            //items = 인벤토리 스크립트 안에 모여있는 것들(리스트)
            if (selectedItem.IsEquippd)
            {
                Manager.Instance.inventoryManager.UnEquipItem(selectedItem);
                Console.WriteLine($"\n{selectedItem.ItemName}이(가) 장착 해제되었습니다");
                Thread.Sleep(1000);
                ShowEquipPage();
            }
            else
            {
                Manager.Instance.inventoryManager.EquipItem(selectedItem);
                Console.WriteLine($"\n{selectedItem.ItemName}이(가) 장착되었습니다");
                Thread.Sleep(1000);
                ShowEquipPage();
            }         
        }

        public void ShowInventory()
        {
            Manager.Instance.inventoryManager.RefrshInventory(false);
            Console.WriteLine("\n1. 장착관리\n");
            Console.WriteLine("0. 나가기\n");
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
