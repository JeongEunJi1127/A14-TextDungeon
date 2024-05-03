namespace A14_TextDungeon
{
    public class Monster
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public float AttackPower { get; private set; }
        public float Defense { get; private set; }
        public float HP { get; private set; }
        public bool IsDead { get; private set; }

        public Monster(string name, int level, float attackPower, float defense, float hp, bool isDead)
        {
            Name = name;
            Level = level;
            AttackPower = attackPower;
            Defense = defense;
            HP = hp;
            IsDead = isDead;
        }

        public float AttackDamage(float damage)
        {
            damage = (float)Math.Ceiling(AttackPower- Manager.Instance.gameManager.user.Defense);
            return damage;
        }

        public void TakeDamage(float damage)
        {
            int hitDamage = 0;
            hitDamage = (int)(damage - Defense);
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
        }

        public void Die()
        {            
            IsDead = true;
            if(Manager.Instance.questManager.quests[0].IsAccepted && Name == "미니언")
            {
                Manager.Instance.questManager.quests[0].UpdateProgress(1); // 미니언이 죽을 때마다 CurrentCount 증가
            }
        }

        public void Berserk()
        {
            HP = 100;
            AttackPower += 20;
        }
    }
}
