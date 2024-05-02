namespace A14_TextDungeon
{
    public class Skill { 
    
        public string Name {  get; private set; }
        public string Description { get; private set; }
        public int MP { get; private set; }

        public Skill(string name, string description, int mp)
        {
            Name = name;
            Description = description;
            MP = mp;
        }
    }
}
