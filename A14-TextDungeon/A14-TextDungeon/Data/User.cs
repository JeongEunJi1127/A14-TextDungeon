using A14_TextDungeon.Manager;
using A14_TextDungeon.Scene;

namespace A14_TextDungeon.Data
{
    public class User
    {

        public int Level { get; private set; }
        public int Gold { get;  set; }
        public float AttackPower { get; private set; }
        public float Defense { get; private set; }
        public float MaxHP {  get; private set; }
        public float HP { get; set; }
        public float MaxMP {  get; private set; }
        
        public float MP { get; set; }

        public string Name { get; private set; }
        public string Job { get; private set; }
        public bool IsDead { get; private set; }
        public int Exp {  get; private set; }
        public int MaxExp {  get; private set; }

        public User(string name, int level, string job, float attackPower, float defense, float maxHp, int maxMp, int gold, bool isDead)
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
            Exp = 0;
            MaxExp = CalculateMaxExp();
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
                AttackPower += 0.5f;
                Defense += 1f;
            }
        }
    }
}
