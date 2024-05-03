namespace A14_TextDungeon
{
    public class Quest
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int TargetCount { get; private set; }
        public int CurrentCount { get; private set; }
        public bool IsAccepted {get; set;}
        public bool IsCompleted { get; set; }
        public string[] Rewards { get; private set; }

        public Quest(string name, string description, int targetCount, bool IsAccepted, bool IsCompleted, string[] rewards)
        {
            Name = name;
            Description = description;
            TargetCount = targetCount;
            CurrentCount = 0;
            IsCompleted = false;
            IsAccepted = false;
            Rewards = rewards;
        }

        public void UpdateProgress(int amount)
        {
            CurrentCount += amount;
            if (CurrentCount >= TargetCount)
            {
                IsCompleted = true;
                Manager.Instance.questManager.QuestClear(0);
            }
        }

    }
}
