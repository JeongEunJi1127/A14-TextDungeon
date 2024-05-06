namespace A14_TextDungeon
{
    public class Village
    {
        public void ShowVillage()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n== 마을 ==.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine($"2. 전투 시작 (현재 진행 : {Manager.Instance.gameManager.user.StageNum}층)");
                Console.WriteLine("3. 인벤 토리");
                Console.WriteLine("4. 퀘스트 보기");
                Console.WriteLine("5. 회복");
                Console.WriteLine("6. 상점\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.\n");
                int input;
                bool isValidNum = int.TryParse(Console.ReadLine(), out input);

                if (isValidNum)
                {
                    switch (input)
                    {
                        case 1:
                            Console.Clear();
                            Manager.Instance.gameManager.status.ShowStatus();
                            return;
                        case 2:
                            Console.Clear();
                            Manager.Instance.gameManager.battle.ShowBattle(true);
                            return;
                        case 3:
                            Console.Clear();
                            Manager.Instance.gameManager.inventory.ShowInventory();
                            return;
                        case 4:
                            Console.Clear();
                            Manager.Instance.questManager.ShowQuests();
                            return;
                        case 5:
                            Console.Clear();
                            Manager.Instance.gameManager.rest.ShowRestUI();
                            return;
                        case 6:
                            Console.Clear();
                            Manager.Instance.gameManager.shop.ShowShopProducts();
                            return;

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
