using A14_TextDungeon.Data;
using A14_TextDungeon.Manager;
using A14_TextDungeon.UI;

namespace A14_TextDungeon.Scene
{
    internal class Rest
    {
        public static int hpPotionCount = 0;
        public static int mpPotionCount = 0;
        public static void ShowRestUI()
        {
            
            RefreshUI();
        }

        public static void RefreshUI()
        {
            hpPotionCount = 0;
            mpPotionCount = 0;
            Console.WriteLine("\n[회복]\n");
            Console.WriteLine("1000G를 내거나 보유중인 포션을 이용해 회복을 할 수 있습니다.\n");
            CheckPotionAmount();
            Console.WriteLine("[소지금]\n");
            Console.WriteLine($"{GameManager.user.Gold}G");
            Console.WriteLine("\n[보유중인 포션]\n");
            Console.WriteLine($"HP포션 : {hpPotionCount} MP포션 : {mpPotionCount}\n");
            Console.WriteLine($"HP: {GameManager.user.HP} / {GameManager.user.MaxHP} ");
            Console.WriteLine($"MP: {GameManager.user.MP} / {GameManager.user.MaxMP} ");
            RestInput.HealInput();

        }

        public static void CheckPotionAmount()
        {
            if(Inventory.items != null)
            {
                for (int i = 0; i < Inventory.items.Count; i++)
                {
                    if (Inventory.items[i].ItemType == ItemType.HPPotion)
                    {
                        hpPotionCount++;
                    }
                    else if (Inventory.items[i].ItemType == ItemType.MPPotion)
                    {
                        mpPotionCount++;
                    }
                }
            }            
        }

        
    }
}
