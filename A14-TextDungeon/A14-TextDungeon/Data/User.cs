namespace A14_TextDungeon.Data
{
    public class User
    {

        public int Level { get; private set; }
        public int Gold { get; private set; }
        public float AttackPower { get; private set; }
        public float Defense { get; private set; }
        public float HP { get; private set; }

        public string Name { get; private set; }
        public string Job { get; private set; }

        public User(string name, int level, string job, float attackPower, float defense, float hp, int gold)
        {
            Name = name;
            Level = level;
            Job = job;
            AttackPower = attackPower;
            Defense = defense;
            HP = hp;
            Gold = gold;
        }

        public void LevelUp()
        {
            Console.WriteLine($"[레벨업!]\n현재 플레이어 레벨 : {Level}");
        }

        public bool Die()
        {
            if (HP <= 0)
            {
                HP = 0;
                return true;
            }
            return false;
        }
    }
}
