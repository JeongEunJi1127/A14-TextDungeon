namespace A14_TextDungeon
{
    public class User
    {
        public enum UserJob
        {
            Warrior = 1,
            Rogue = 2,
        }

        public int Level { get; private set; }
        public int Gold { get; set; }
        public float AttackPower { get; private set; }
        public float Defense { get; private set; }
        public float MaxHP {  get; private set; }
        public float HP { get; set; }
        public float MaxMP {  get; private set; }
        
        public float MP { get; set; }

        public string Name { get; private set; }
        public UserJob Job { get; private set; }
        public bool IsDead { get; private set; }
        public int Exp {  get; private set; }
        public int MaxExp {  get; private set; }
        public int StageNum { get;  set; }
        public User(string name, int level, UserJob job, float attackPower, float defense, float maxHp, float maxMp, int gold, int stageNum, bool isDead)
        {
            Name = name;
            Level = level;
            Job = job;
            AttackPower = attackPower;
            Defense = defense;
            MaxHP = maxHp;
            HP = maxHp;
            MaxMP = maxMp;
            MP = maxMp;
            Gold = gold;
            IsDead = isDead;
            StageNum = stageNum;
            Exp = 0;
            MaxExp = CalculateMaxExp();
        }       

        public float AttackDamage(float damage)
        {
            float hitDamage = 0;
            float weaponAttackDamage = 0;
            for(int i = 0; i < Manager.Instance.inventoryManager.items.Count; i++) 
            {
                if (Manager.Instance.inventoryManager.items[i].IsEquippd && Manager.Instance.inventoryManager.items[i].Itemtype == Item.ItemType.Weapon)
                {
                    weaponAttackDamage += Manager.Instance.inventoryManager.items[i].ItemStat;
                }
            }

            hitDamage = damage * (1 +(weaponAttackDamage/10));
            return hitDamage;
        }

        public void TakeDamage(float damage)
        {
            float hitDamage = 0;
            hitDamage = damage;
            if(hitDamage <= 0)
            {
                hitDamage = 1;
            }
            HP -= hitDamage;
            if (HP <= 0)
            {
                HP = 0;
                Die();
            }
            Manager.Instance.fileManager.SaveData();
        }

        public void AddGold(int amount)
        {
            Gold += amount;
        }

        public void Die()
        {
            IsDead = true;
            Manager.Instance.fileManager.SaveData();
        }

        //최대 경험치 계산
        private int CalculateMaxExp()
        {
            switch (Level)
            {
                case 1:
                    return 10;
                case 2:
                    return 35;
                case 3:
                    return 65;
                case 4:
                    return 100;
                default:
                    return 0;
            }
        }

        public void LevelUP(int giveExp)
        {
            int temp;
            Exp += giveExp;
            if(Exp >= MaxExp)
            {
                Console.WriteLine("레벨업!");
                Level++;
                temp = Exp - MaxExp;
                Exp = temp;
                MaxExp = CalculateMaxExp();
                AttackPower += 3f;
                Defense += 2f;
                Manager.Instance.questManager.QuestClear(2);
            }
            Manager.Instance.fileManager.SaveData();
        }
    }
}
