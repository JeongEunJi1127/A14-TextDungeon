using A14_TextDungeon.Data;
using A14_TextDungeon.Manager;
using A14_TextDungeon.Scene;

namespace A14_TextDungeon.UI
{
    internal class RestInput
    {
        public static void HealInput()
        {
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
                        Village.ShowVillage();
                    }
                    else if (input == 1)
                    {
                        if (GameManager.user.HP >= GameManager.user.MaxHP && GameManager.user.MP >= GameManager.user.MaxMP)
                        {
                            Console.WriteLine("더이상 회복할 수 없습니다!");
                        }
                        else
                        {
                            if (GameManager.user.Gold < 1000)
                            {
                                Console.WriteLine("소지금이 부족합니다.");
                            }
                            else
                            {
                                //골드 소모후 회복
                                GameManager.user.Gold -= 1000;

                                Console.WriteLine("소지금을 1000G 내고 휴식을 취했습니다");
                                Console.WriteLine($"HP +{GameManager.user.MaxHP - GameManager.user.HP}," +
                                                  $"MP +{GameManager.user.MaxMP - GameManager.user.MP}");
                                GameManager.user.HP = GameManager.user.MaxHP;
                                GameManager.user.MP = GameManager.user.MaxMP;
                            }
                        }
                        Thread.Sleep(1000);
                        Console.Clear();
                        Rest.ShowRestUI();
                    }
                    else if (input == 2)
                    {
                        if (GameManager.user.HP >= GameManager.user.MaxHP)
                        {
                            Console.WriteLine("체력을 더 회복할 수 없습니다!");
                        }
                        else
                        {
                            if (Rest.hpPotionCount <= 0)
                            {
                                Console.WriteLine("포션이 부족합니다!");
                            }
                            else
                            {
                                //HP포션 소모후 회복
                                Inventory.RemovePotionFromInventory(ItemType.HPPotion);
                                if (GameManager.user.HP + 30 > GameManager.user.MaxHP)
                                {
                                    Console.WriteLine($"HP +{GameManager.user.MaxHP - GameManager.user.HP}");
                                    GameManager.user.HP = GameManager.user.MaxHP;
                                }
                                else
                                {
                                    Console.WriteLine($"HP +30");
                                    GameManager.user.HP += 30;
                                }
                            }
                        }
                        Thread.Sleep(1000);
                        Console.Clear();
                        Rest.ShowRestUI();
                    }
                    else if (input == 3)
                    {
                        if (GameManager.user.MP >= GameManager.user.MaxMP)
                        {
                            Console.WriteLine("더이상 회복할 수 없습니다!");
                        }
                        else
                        {
                            if (Rest.mpPotionCount <= 0)
                            {
                                Console.WriteLine("포션이 부족합니다!");
                            }
                            else
                            {
                                //MP포션 소모후 회복
                                Inventory.RemovePotionFromInventory(ItemType.MPPotion);
                                if (GameManager.user.MP + 30 > GameManager.user.MaxMP)
                                {
                                    Console.WriteLine($"MP +{GameManager.user.MaxMP - GameManager.user.MP}");
                                    GameManager.user.MP = GameManager.user.MaxMP;
                                }
                                else
                                {
                                    Console.WriteLine($"MP +30");
                                    GameManager.user.MP += 30;
                                }
                            }
                        }
                        Thread.Sleep(1000);
                        Console.Clear();
                        Rest.ShowRestUI();

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
