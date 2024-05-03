namespace A14_TextDungeon.Data
{
    public class Quest
    {
        public string Name { get;  set; }
        public string Description { get;  set; }
        public int TargetCount { get;  set; }
        public int CurrentCount { get;  set; }
        public bool IsAccepted { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsWaiting { get; set; }
        public List<string> Rewards { get;  set; }

        public Quest(string name, string description, int targetCount, bool isAccepted, bool isCompleted, bool isWaiting, List<string> rewards)
        {
            Name = name;
            Description = description;
            TargetCount = targetCount;
            CurrentCount = 0;
            IsCompleted = isCompleted;
            IsAccepted = isAccepted;
            IsWaiting = isWaiting;
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
            Manager.Instance.fileManager.SaveData();
        }
    }
}