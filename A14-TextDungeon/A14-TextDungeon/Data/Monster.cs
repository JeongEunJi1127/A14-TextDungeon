﻿namespace A14_TextDungeon.Data
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
            //데미지가 오차값 적용이 되지 않은 상태로 왔다면..
            int range = (int)MathF.Ceiling((damage / 10));
            Random random = new Random();
            damage = random.Next((int)(damage - range), (int)(damage + range + 1));

            HP -= damage;
            if (HP <= 0)
            {
                HP = 0;
                Die();
            }
        }

        public void Die()
        {
            Console.WriteLine($"\n{Name} die\n");
            IsDead = true;
        }
    }
}
