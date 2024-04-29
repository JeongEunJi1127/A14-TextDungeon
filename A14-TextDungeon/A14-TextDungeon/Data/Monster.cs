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

        public void TakeDamage(float damage)
        {
            // 데미지가 오차값 적용이 되지 않은 상태로 왔다면..
            


            HP -= damage;
            if (HP < 0)
            {
                Die();
            }
        }

        public void Die()
        {
            IsDead = true;
        }
    }
}
