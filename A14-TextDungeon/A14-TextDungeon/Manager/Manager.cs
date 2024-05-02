namespace A14_TextDungeon
{
    public class Manager
    {
        private static Manager instance;
        public static Manager Instance
        {
            get
            {
                if (instance == null) instance = new Manager();
                return instance;
            }
        }
        
        public GameManager gameManager = new GameManager();
        public FileManager fileManager = new FileManager();
        public BattleManager battleManager = new BattleManager();
        public UserDataManager userDataManager = new UserDataManager();
        public QuestManager questManager = new QuestManager();
        public InventoryManager inventoryManager = new InventoryManager();
    }
}
