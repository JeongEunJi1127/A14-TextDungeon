namespace A14_TextDungeon.Data
{
    public class Monster
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public float AttackPower { get; private set; }
        public float HP { get; private set; }
        public bool IsDead { get; private set; }
        public Monster(string name, int level, float attackPower, float hp, bool isDead)
        {
            Name = name;
            Level = level;
            AttackPower = attackPower;
            HP = hp;
            IsDead = isDead;
        }
    }
}
