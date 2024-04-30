using A14_TextDungeon.Manager;
using A14_TextDungeon.Scene;

namespace A14_TextDungeon.Data
{
    public class User
    {

        public int Level { get; private set; }
        public int Gold { get; private set; }
        public float AttackPower { get; private set; }
        public float Defense { get; private set; }
        public float HP { get; set; }
        public int MP { get; set; }

        public string Name { get; private set; }
        public string Job { get; private set; }
        public bool IsDead { get; private set; }

        public User(string name, int level, string job, float attackPower, float defense, float hp, int mp, int gold, bool isDead)
        {
            Name = name;
            Level = level;
            Job = job;
            AttackPower = attackPower;
            Defense = defense;
            HP = hp;
            MP = mp;
            Gold = gold;
            IsDead = isDead;
        }

        public float AttackDamage(float damage)
        {
            int range = (int)MathF.Ceiling((damage / 10));
            Random random = new Random();
            damage = random.Next((int)(damage - range), (int)(damage + range + 1));

            return damage;
        }

        public void TakeDamage(float damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                HP = 0;
                Die();
            }
        }

        public void LevelUp()
        {
            Console.WriteLine($"[레벨업!]\n현재 플레이어 레벨 : {Level}");
        }

        public void Die()
        {
            IsDead = true;
        }

       
        public enum UserJob
        {
            Warrior = 1,
            Rogue = 2,
        }

        
    }
}
