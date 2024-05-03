using static A14_TextDungeon.Item;

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

        public List<Skill[]> skillList = new List<Skill[]>();

        // 저장해야 할 파일 초기화 - 유저 정보, 인벤토리 정보, 상점 정보 등?
        public void Init()
        {
            skillList.Add(warriorSkills);
            skillList.Add(rogueSkills);
#region 상점 아이템 추가
            Manager.Instance.shopManager.AddProduct(new ShopProduct(new Item("수련자 갑옷", 5, ItemType.Armor, "수련에 도움을 주는 갑옷입니다."), 1000));
            Manager.Instance.shopManager.AddProduct(new ShopProduct(new Item("무쇠갑옷", 9, ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다."), 1500));
            Manager.Instance.shopManager.AddProduct(new ShopProduct(new Item("스파르타의 갑옷", 15, ItemType.Armor, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."), 2000));
            Manager.Instance.shopManager.AddProduct(new ShopProduct(new Item("낡은 검", 2, ItemType.Weapon, "쉽게 볼 수 있는 낡은 검 입니다."), 600));
            Manager.Instance.shopManager.AddProduct(new ShopProduct(new Item("청동 도끼", 5, ItemType.Weapon, "어디선가 사용됐던거 같은 도끼입니다."), 1500));
            Manager.Instance.shopManager.AddProduct(new ShopProduct(new Item("스파르타의 창", 7, ItemType.Weapon, "스파르타의 전사들이 사용했다는 전설의 창입니다."), 2000));
#endregion
        }

        static void Main(string[] args)
        {
            Manager.Instance.userDataManager.SetName();
        }         
    }
}

