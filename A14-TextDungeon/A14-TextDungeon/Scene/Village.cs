using A14_TextDungeon.Manager;

namespace A14_TextDungeon.Scene
{
    public class Village
    { 
        public static void ShowVillage()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine($"2. 전투 시작 (현재 진행 : {BattleManager.stageNum}층)");
            Console.WriteLine("3. 인벤 토리\n");
            Console.WriteLine("5. 회복\n");

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
                            Inventory.ShowInventory();
                            break;
                        case 5:
                            Console.Clear();
                            Rest.ShowRestUI();
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
