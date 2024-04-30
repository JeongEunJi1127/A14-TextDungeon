namespace A14_TextDungeon.Scene
{
    public class Village
    { 
        public static void ShowVillage()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("3. 인벤 토리\n");

            Inventory inventory = new Inventory();
            
            #region 테스트아이템목록
            Item testItem1 = new Item("무쇠갑옷", 5, ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다.");
            Item testItem2 = new Item("스파르타의 창", 7, ItemType.Wepon, "스파르타의 전사들이 사용했다는 전설의 창입니다.");
            Item testItem3 = new Item("낡은 검", 2, ItemType.Wepon, "쉽게 볼 수 있는 낡은 검 입니다.");
            inventory.AddItem(testItem1);
            inventory.AddItem(testItem2);
            inventory.AddItem(testItem3);
            #endregion

            while (true)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.\n");
                int input;
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);

                if (isValidNum)
                {
                    switch (input)
                    {
                        case 1:
                            Console.Clear();
                            Status.ShowStatus();
                            break;
                        case 2:
                            Console.Clear();
                            Battle.ShowBattle(true);
                            break;
                        case 3:
                            Console.Clear();
                            inventory.ShowInventory();
                            break;

                        default:
                            Console.WriteLine("\n잘못된 입력입니다.\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\n숫자를 입력해주세요.\n");
                }
            }
        }
    }
}
