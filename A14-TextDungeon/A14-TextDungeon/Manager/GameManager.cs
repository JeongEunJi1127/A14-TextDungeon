using A14_TextDungeon.Scene;
using static System.Formats.Asn1.AsnWriter;

namespace A14_TextDungeon.Manager
{
    internal class GameManager
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. 상태  보기");
            Console.WriteLine("2. 전투 시작\n");

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
                            Console.WriteLine("\n상태창\n");
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("\nbattle\n");
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
