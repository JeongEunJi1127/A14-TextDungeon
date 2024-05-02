namespace A14_TextDungeon
{
    public class Rest
    {
        public int hpPotionCount = 0;
        public int mpPotionCount = 0;
        public void ShowRestUI()
        {
            RefreshUI();
        }

        public void RefreshUI()
        {
            hpPotionCount = 0;
            mpPotionCount = 0;
            Console.WriteLine("\n[회복]\n");
            Console.WriteLine("1000G를 내거나 보유중인 포션을 이용해 회복을 할 수 있습니다.\n");
            CheckPotionAmount();
            Console.WriteLine("[소지금]\n");
            Console.WriteLine($"{Manager.Instance.gameManager.user.Gold}G");
            Console.WriteLine("\n[보유중인 포션]\n");
            Console.WriteLine($"HP포션 : {hpPotionCount} MP포션 : {mpPotionCount}\n");
            Console.WriteLine($"HP: {Manager.Instance.gameManager.user.HP} / {Manager.Instance.gameManager.user.MaxHP} ");
            Console.WriteLine($"MP: {Manager.Instance.gameManager.user.MP} / {Manager.Instance.gameManager.user.MaxMP} ");
            HealInput();
        }

        public void CheckPotionAmount()
        {
            if (Manager.Instance.inventoryManager.items != null)
            {
                for (int i = 0; i < Manager.Instance.inventoryManager.items.Count; i++)
                {
                    if (Manager.Instance.inventoryManager.items[i].Itemtype == Item.ItemType.HPPotion)
                    {
                        hpPotionCount++;
                    }
                    else if (Manager.Instance.inventoryManager.items[i].Itemtype == Item.ItemType.MPPotion)
                    {
                        mpPotionCount++;
                    }
                }
            }            
        }

        public void HealInput()
        {
            User user = Manager.Instance.gameManager.user;

            Console.WriteLine("원하는 행동을 입력해 주세요\n");
            Console.WriteLine("1. 1000G를 내고 휴식\n");
            Console.WriteLine("2. 보유중인 HP포션을 사용해 HP회복\n");
            Console.WriteLine("3. 보유중인 MP포션을 사용해 MP회복\n");
            Console.WriteLine("0. 나가기\n");
            int input;
            while (true)
            {
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);
                if (isValidNum)
                {
                    if (input == 0)
                    {
                        //나가기
                        Manager.Instance.gameManager.village.ShowVillage();
                        return;
                    }
                    else if (input == 1)
                    {
                        if (user.HP >= user.MaxHP && user.MP >= user.MaxMP)
                        {
                            Console.WriteLine("더이상 회복할 수 없습니다!");
                        }
                        else
                        {
                            if (user.Gold < 1000)
                            {
                                Console.WriteLine("소지금이 부족합니다.");
                            }
                            else
                            {
                                //골드 소모후 회복
                                user.Gold -= 1000;

                                Console.WriteLine("소지금을 1000G 내고 휴식을 취했습니다");
                                Console.WriteLine($"HP +{user.MaxHP - user.HP}," +
                                                  $"MP +{user.MaxMP - user.MP}");
                                user.HP = user.MaxHP;
                                user.MP = user.MaxMP;
                            }
                        }
                        Thread.Sleep(1000);
                        Console.Clear();
                        Manager.Instance.gameManager.rest.ShowRestUI();
                        return;
                    }
                    else if (input == 2)
                    {
                        if (user.HP >= user.MaxHP)
                        {
                            Console.WriteLine("체력을 더 회복할 수 없습니다!");
                        }
                        else
                        {
                            if (Manager.Instance.gameManager.rest.hpPotionCount <= 0)
                            {
                                Console.WriteLine("포션이 부족합니다!");
                            }
                            else
                            {
                                //HP포션 소모후 회복
                                Manager.Instance.inventoryManager.RemovePotionFromInventory(Item.ItemType.HPPotion);
                                if (user.HP + 30 > user.MaxHP)
                                {
                                    Console.WriteLine($"HP +{user.MaxHP - user.HP}");
                                    user.HP = user.MaxHP;
                                }
                                else
                                {
                                    Console.WriteLine($"HP +30");
                                    user.HP += 30;
                                }
                            }
                        }
                        Thread.Sleep(1000);
                        Console.Clear();
                        Manager.Instance.gameManager.rest.ShowRestUI();
                        return;
                    }
                    else if (input == 3)
                    {
                        if (user.MP >= user.MaxMP)
                        {
                            Console.WriteLine("더이상 회복할 수 없습니다!");
                        }
                        else
                        {
                            if (Manager.Instance.gameManager.rest.mpPotionCount <= 0)
                            {
                                Console.WriteLine("포션이 부족합니다!");
                            }
                            else
                            {
                                //MP포션 소모후 회복
                                Manager.Instance.inventoryManager.RemovePotionFromInventory(Item.ItemType.MPPotion);
                                if (user.MP + 30 > user.MaxMP)
                                {
                                    Console.WriteLine($"MP +{user.MaxMP - user.MP}");
                                    user.MP = user.MaxMP;
                                }
                                else
                                {
                                    Console.WriteLine($"MP +30");
                                    user.MP += 30;
                                }
                            }
                        }
                        Thread.Sleep(1000);
                        Console.Clear();
                        Manager.Instance.gameManager.rest.ShowRestUI();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
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
