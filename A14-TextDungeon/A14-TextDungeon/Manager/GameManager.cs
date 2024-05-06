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
            Manager.Instance.shopManager.AddProduct(new ShopProduct("수련자 갑옷", "수련에 도움을 주는 갑옷.", Item.ItemType.Armor, 5, 1000));
            Manager.Instance.shopManager.AddProduct(new ShopProduct("대마법사의 지팡이", "마법사 박재균이 사용했던 방패. 강력한 방어력을 제공한다. 진유록 마왕의 저주를 무효화 시킨다.", Item.ItemType.Weapon, 9, 1500));
            Manager.Instance.shopManager.AddProduct(new ShopProduct("위대한 전사의 갑옷", "전사 이인호의 갑옷. 이 갑옷을 착용하면 왠지 깃허브를 잘 다룰 수 있게 될 것만 같다.", Item.ItemType.Armor, 15, 2000));
            Manager.Instance.shopManager.AddProduct(new ShopProduct("타락한 검사의 검", "타락한 검사가 사용했던 검. 이 검을 사용하면 왠지 TIL을 써야할 것만 같은 기분이 든다.", Item.ItemType.Weapon, 2, 600));
            Manager.Instance.shopManager.AddProduct(new ShopProduct("빛의 성검", "성직자 정은지가 사용했던 빛의 성검. 이 검을 휘두르면 모든 코딩 오류가 해결될 것 같은 기분이 든다.", Item.ItemType.Weapon, 5, 1500));
            Manager.Instance.shopManager.AddProduct(new ShopProduct("윤세나의 창", "용사 윤세나가 진유록 마왕을 무찌를 때 사용했던 창.", Item.ItemType.Weapon, 7, 2000));
            #endregion

            Manager.Instance.questManager.AddQuest(new Quest("[마왕의 부활: 암흑군단 미니언 처치하라]", "-진유록 마왕의 부활-\n수세기 동안 잠들어 있던 [진유록 마왕]이 갑자기 부활했습니다.\n진유록 마왕은 그의 수하인 미니언들과 함께 세계를 침공하고 있어요.\n윤세나 용사의 동료인 당신께서 암흑군단 미니언을 처치해주시지 않겠습니까? ", 3, false, false, false,new List<string> { "타락한 전사의 갑옷 x 1", "5G" }));
            Manager.Instance.questManager.AddQuest(new Quest("[마왕의 TIL 저주: 장비를 장착해보자]", "-진유록 마왕의 TIL 저주-\n[진유록 마왕]은 극악무도한 TIL 저주로 악명을 떨치고 있습니다.\n그의 저주와 맞서기 위해선 장비를 장착해야합니다.\n당신을 수호해줄 장비를 장착해보세요.", 1, false, false, false, new List<string> { "성직자의 포션 x 1", "5G" }));
            Manager.Instance.questManager.AddQuest(new Quest("[용사의 동료: 더욱 더 강해지기!]", "-윤세나 용사의 전설-\n윤세나 용사는 과거에 진유록 마왕을 물리치고 세상을 구했던 전설적인 용사입니다.\n그녀의 동료로 합류하기 위해 강해지십시오!\n레벨업을 하여 당신의 강인한 용기와 힘을 증명하세요.", 1, false, false, false, new List<string> { "용사의 증표 x 1", "5G" }));
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

