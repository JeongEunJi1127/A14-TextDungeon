

namespace A14_TextDungeon
{
    public class Shop
    {
        List<ShopProduct> inventoryProducts = new List<ShopProduct>();
        public void ShowShopProducts()
        {
            DisplayShopProducts();
            ShopInput();
        }

        public void DisplayShopProducts()
        {
            Console.Clear();
            Console.WriteLine("\n[상점]\n");
            Console.WriteLine("\n[보유 골드]\n");
            Console.WriteLine($"{Manager.Instance.gameManager.user.Gold} G\n");
            Console.WriteLine("[아이템 목록]\n");

            if(Manager.Instance.shopManager.products == null || Manager.Instance.shopManager.products.Count == 0)
            {
                Console.WriteLine("현재 판매중인 상품이 없습니다.");
            }
            else
            {
                for (int i = 0; i < Manager.Instance.shopManager.products.Count; i++)
                {
                    Console.WriteLine(Manager.Instance.shopManager.products[i].ProductsName() + "\n");
                }               
            }
        }

        public void ShopInput()
        {
            int input;
            Console.WriteLine("\n1. 아이템 구매\n");
            Console.WriteLine("2. 아이템 판매\n");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하는 행동을 입력하세요");
            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                switch (input)
                {
                    case 0:
                        Manager.Instance.gameManager.village.ShowVillage(); 
                        break;
                    case 1:
                        //구매 페이지
                        BuyProducts();
                        break;
                    case 2:
                        //판매 페이지
                        SellProducts();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        ShopInput();
                        break;
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해 주세요");
                Thread.Sleep(1000);
                ShopInput();
            }
        }
        void BuyProducts()
        {
            Console.Clear();
            Console.WriteLine("\n[상점 - 구매]\n");
            Console.WriteLine("\n[보유 골드]\n");
            Console.WriteLine($"{Manager.Instance.gameManager.user.Gold} G\n");
            Console.WriteLine("[아이템 목록]");
            if (Manager.Instance.shopManager.products == null || Manager.Instance.shopManager.products.Count == 0)
            {
                Console.WriteLine("현재 판매중인 상품이 없습니다.");
            }
            else
            {
                for (int i = 0; i < Manager.Instance.shopManager.products.Count; i++)
                {
                    Console.WriteLine($"{i+1}. {Manager.Instance.shopManager.products[i].ProductsName()}\n");
                }
            }
            BuyProductsInput();
        }
        void BuyProductsInput()
        {
            int input;
            Console.WriteLine("\n구매하고 싶은 아이템을 선택하세요 :");
            Console.WriteLine("0 : 나가기");
            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                if (input > 0 && input < Manager.Instance.shopManager.products.Count)
                {
                    //아이템 구매
                    BuyItems(input);
                }
                else if(input == 0)
                {
                    //나가기
                    ShowShopProducts();
                }
                else
                {
                    Console.WriteLine("숫자를 다시 입력해 주세요");
                    Thread.Sleep(1000);
                    BuyProducts();
                }
            }
        }
        void BuyItems(int i)
        {
            if (Manager.Instance.shopManager.products[i -1].IsBuy == true)
            {
                Console.WriteLine("이미 구매한 아이템 입니다.");
                Thread.Sleep(1000);
                BuyProducts();
            }
            else
            {
                if(Manager.Instance.shopManager.products[i - 1].Price > Manager.Instance.gameManager.user.Gold)
                {
                    Console.WriteLine("골드가 부족합니다.");
                    Thread.Sleep(1000);
                    BuyProducts();
                }
                else
                {
                    Console.WriteLine("구매를 완료했습니다.");
                    Thread.Sleep(1000);
                    Manager.Instance.gameManager.user.Gold -= Manager.Instance.shopManager.products[i - 1].Price;
                    Manager.Instance.inventoryManager.AddItem(new Item(Manager.Instance.shopManager.products[i - 1].ItemName, Manager.Instance.shopManager.products[i - 1].ItemDescription, Manager.Instance.shopManager.products[i - 1].Itemtype, Manager.Instance.shopManager.products[i - 1].ItemStat));
                    Manager.Instance.shopManager.products[i - 1].IsBuy = true;
                    BuyProducts();
                }
            }
        }

        void SellProducts()
        {
            
            RefreshShopProducts();
            Console.Clear();
            Console.WriteLine("\n[상점 - 판매]\n");
            Console.WriteLine("\n[보유 골드]\n");
            Console.WriteLine($"{Manager.Instance.gameManager.user.Gold} G\n");
            Console.WriteLine("[아이템 목록]");
            if(Manager.Instance.inventoryManager.items.Count ==0 || Manager.Instance.inventoryManager.items == null)
            {
                Console.WriteLine("인벤토리에 판매할 아이템이 없습니다.");
                Thread.Sleep(1000);
                ShowShopProducts();
            }
            else
            {
                for(int i = 0; i< Manager.Instance.inventoryManager.items.Count; i++)
                {
                    Console.WriteLine($"{i+1}. {inventoryProducts[i].ProductsName()}\n"); 
                }
            }
            SellProductsInput();
        }

        void SellProductsInput()
        {
            int input;
            Console.WriteLine("\n판매하고 싶은 아이템을 선택하세요 :");
            Console.WriteLine("0 : 나가기");
            bool isValidNum = int.TryParse(Console.ReadLine(), out input);
            if (isValidNum)
            {
                if (input > 0 && input <= inventoryProducts.Count)
                {
                    //아이템 판매
                    SellItems(input);
                }
                else if (input == 0)
                {
                    //나가기
                    ShowShopProducts();
                }
                else
                {
                    Console.WriteLine("숫자를 다시 입력해 주세요");
                    Thread.Sleep(1000);
                    SellProducts();
                }
            }
        }

        void SellItems(int input)
        {
            if(inventoryProducts[input - 1].IsEquippd)
            {
                for (int i = 0; i < Manager.Instance.inventoryManager.items.Count; i++)
                {
                    if (Manager.Instance.inventoryManager.items[i].ItemName == inventoryProducts[input - 1].ItemName)
                    {
                        Manager.Instance.inventoryManager.UnEquipItem(Manager.Instance.inventoryManager.items[i]);
                    }
                }

            }

            Manager.Instance.gameManager.user.Gold += inventoryProducts[input - 1].Price;            
            for(int i = 0; i< Manager.Instance.inventoryManager.items.Count; i++)
            {
                if (Manager.Instance.inventoryManager.items[i].ItemName == inventoryProducts[input - 1].ItemName)
                {
                    Manager.Instance.inventoryManager.RemoveItem(Manager.Instance.inventoryManager.items[i]);
                }
            }

            for (int i = 0; i < Manager.Instance.shopManager.products.Count; i++)
            {
                if (inventoryProducts[input - 1].ItemName == Manager.Instance.shopManager.products[i].ItemName)
                {
                    Manager.Instance.shopManager.products[i].IsBuy = false;
                }
            }
            inventoryProducts.Remove(inventoryProducts[input - 1]);
            Manager.Instance.fileManager.SaveData();
            SellProducts();
        }

        void RefreshShopProducts()
        {
            if (!(Manager.Instance.inventoryManager.items.Count == 0 || Manager.Instance.inventoryManager.items == null))
            {
                for (int i = 0; i < Manager.Instance.inventoryManager.items.Count; i++)
                {
                    string itemName = Manager.Instance.inventoryManager.items[i].ItemName;
                    string itemDescription = Manager.Instance.inventoryManager.items[i].ItemDescription;
                    Item.ItemType itemType = Manager.Instance.inventoryManager.items[i].Itemtype;
                    int itemStat = Manager.Instance.inventoryManager.items[i].ItemStat;
                    bool isEquippd = Manager.Instance.inventoryManager.items[i].IsEquippd;

                    switch (Manager.Instance.inventoryManager.items[i].ItemName)
                    {
                        case "미니언의 지팡이":
                            inventoryProducts.Add(new ShopProduct(itemName,itemDescription,itemType, itemStat, 300, isEquippd));
                            break;
                        case "공허충 비늘 갑옷":
                            inventoryProducts.Add(new ShopProduct(itemName, itemDescription, itemType, itemStat, 400, isEquippd));
                            break;
                        case "대포미니언의 대포":
                            inventoryProducts.Add(new ShopProduct(itemName, itemDescription, itemType, itemStat, 500, isEquippd));
                            break;
                        case "HP회복 포션":
                            inventoryProducts.Add(new ShopProduct(itemName, itemDescription, itemType, itemStat, 200, isEquippd));
                            break;
                        case "MP회복 포션":
                            inventoryProducts.Add(new ShopProduct(itemName, itemDescription, itemType, itemStat, 200, isEquippd));
                            break;
                        default:
                            for(int j = 0; j < Manager.Instance.shopManager.products.Count; j++)
                            {
                                if(Manager.Instance.inventoryManager.items[i].ItemName == Manager.Instance.shopManager.products[j].ItemName)
                                {
                                    inventoryProducts.Add(new ShopProduct(itemName, itemDescription, itemType, itemStat, Manager.Instance.shopManager.products[j].Price, isEquippd));
                                }
                            }
                            break;                            
                    }               
                }
            }
        }
    }
}
