using A14_TextDungeon.Data;

namespace A14_TextDungeon
{
    public class GameManager
    {
        public Battle battle = new Battle();
        public Status status = new Status();
        public Village village = new Village();
        public Boss boss = new Boss();
        public Rest rest = new Rest();
        public Inventory inventory = new Inventory();
        public Shop shop = new Shop();

        public User user;

        public Skill[] warriorSkills = new Skill[]
        {
                new Skill ("알파 스트라이크", "공격력 * 2 로 하나의 적을 공격합니다.", 10),
                new Skill ("더블 스트라이크", "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.", 15)
        };

        public Skill[] rogueSkills = new Skill[]
        {         
           new Skill ("기습", "공격력+10의 피해로 하나의 적을 공격합니다.", 10),
           new Skill ("도둑질", "공격력 * 1.5 만큼의 피해를 주고 피해량 만큼의 골드를 획득합니다.", 20)
        };

        public User[] jobStat = new User[]
        {
            new User("전사", 1 , User.UserJob.Warrior, 10 , 5, 100, 50 , 1500,1,  false),
            new User("도적", 1 , User.UserJob.Rogue, 15 , 3, 60, 70 , 1500,1, false)
        };
        public List<Skill[]> skillList = new List<Skill[]>();

        public void Init()
        {
            #region 상점 아이템 추가   
            Manager.Instance.shopManager.AddProduct(new ShopProduct("HP 물약", "HP를 30 회복 할 수 있는 물약입니다.", Item.ItemType.HPPotion, 30, 200));
            Manager.Instance.shopManager.AddProduct(new ShopProduct("MP 물약", "MP를 30 회복 할 수 있는 물약입니다.", Item.ItemType.MPPotion, 30, 200));
            Manager.Instance.shopManager.AddProduct(new ShopProduct("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", Item.ItemType.Armor, 5, 1000));
            Manager.Instance.shopManager.AddProduct(new ShopProduct("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", Item.ItemType.Armor, 9, 1500));           
            Manager.Instance.shopManager.AddProduct(new ShopProduct("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", Item.ItemType.Weapon, 2, 600));
            Manager.Instance.shopManager.AddProduct(new ShopProduct("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", Item.ItemType.Weapon, 5, 1500));           
            #endregion

            Manager.Instance.questManager.AddQuest(new Quest("[마을을 위협하는 미니언 처치]", "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!", 1, false, false, false,new List<string> { "쓸만한 방패 x 1", "5G" }));
            Manager.Instance.questManager.AddQuest(new Quest("[장비를 장착해보자!]", "???: 이봐, 먼 길을 떠나려는 그대여.\n자네가 향하려는 길은 아주 험하고 위험한 길이라네. 뭐라도 걸치는 게 어떤가?\n목숨이 아깝지 않다면 말이야..", 1, false, false, false, new List<string> { "여신의 축복 x 1", "5G" }));
            Manager.Instance.questManager.AddQuest(new Quest("[더욱 더 강해지기!]", "당신의 강함을 증명해 보세요.\n수많은 모험가들이 도전했고 또한 실패했지만, 어쩌면 당신은 다를지도 모르죠.\n행운을 빌어요.", 1, false, false, false, new List<string> { "강화된 마체테 x 1", "5G" }));
        }

        static void Main(string[] args)
        {
            Manager.Instance.gameManager.skillList.Add(Manager.Instance.gameManager.warriorSkills);
            Manager.Instance.gameManager.skillList.Add(Manager.Instance.gameManager.rogueSkills);

            Manager.Instance.fileManager.LoadData();
            Manager.Instance.gameManager.village.ShowVillage();
        }         
    }
}

