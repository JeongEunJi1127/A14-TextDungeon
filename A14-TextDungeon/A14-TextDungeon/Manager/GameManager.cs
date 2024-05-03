using System.Reflection.Emit;

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
            new User("전사", 1 , User.UserJob.Warrior, 10 , 5, 100, 50 , 1500, false),
            new User("도적", 1 , User.UserJob.Rogue, 15 , 3, 60, 70 , 1500, false)
        };
        public List<Skill[]> skillList = new List<Skill[]>();

        // 저장해야 할 파일 초기화 - 유저 정보, 인벤토리 정보, 상점 정보 등?
        public void Init()
        {
            skillList.Add(warriorSkills);
            skillList.Add(rogueSkills);
        }

         static void Main(string[] args)
        {
            Manager.Instance.userDataManager.SetName();
        }         
    }
}
