namespace A14_TextDungeon
{
    public class Status
    {
        public void ShowStatus()
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
                            Manager.Instance.gameManager.village.ShowVillage();
                            return;
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

         void RefreshPlayerStatus()
        {
            List<Item> items = Manager.Instance.inventoryManager.items;
            int weaponAttack = 0;
            int armorDefense = 0;
            if(items != null)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].IsEquippd)
                    {
                        if (items[i].Itemtype == Item.ItemType.Weapon)
                        {
                            weaponAttack += items[i].ItemStat;
                        }
                        else if (items[i].Itemtype == Item.ItemType.Armor)
                        {
                            armorDefense += items[i].ItemStat;
                        }
                    }
                }
            }

            Console.WriteLine("\n== 상태창 ==\n");
            Console.WriteLine($"LV. {Manager.Instance.gameManager.user.Level}");
            Console.WriteLine($"{Manager.Instance.gameManager.user.Name} ({Manager.Instance.gameManager.user.Job})");

            if( weaponAttack > 0 )
            {
                Console.WriteLine($"공격력 : {Manager.Instance.gameManager.user.AttackPower+ weaponAttack} (+{weaponAttack})");
            }
            else
            {
                Console.WriteLine($"공격력 : {Manager.Instance.gameManager.user.AttackPower}");
            }

            if (armorDefense > 0)
            {
                Console.WriteLine($"방어력 : {Manager.Instance.gameManager.user.Defense + armorDefense} (+{armorDefense})");
            }
            else
            {
                Console.WriteLine($"방어력 : {Manager.Instance.gameManager.user.Defense}");
            }

            Console.WriteLine($"HP: {Manager.Instance.gameManager.user.HP} / {Manager.Instance.gameManager.user.MaxHP} ");
            Console.WriteLine($"MP: {Manager.Instance.gameManager.user.MP} / {Manager.Instance.gameManager.user.MaxMP} ");

            Console.WriteLine($"Gold : {Manager.Instance.gameManager.user.Gold} G");
            Console.WriteLine($"EXP : {Manager.Instance.gameManager.user.Exp}/{Manager.Instance.gameManager.user.MaxExp}\n\n");

            Console.WriteLine("0. 나가기");
        }
    }
}
