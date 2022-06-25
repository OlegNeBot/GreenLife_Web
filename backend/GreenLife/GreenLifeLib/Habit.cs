namespace GreenLifeLib
{
    public class Habit
    {
        #region [Props]

        public int Id { get; set; }
        public int Score { get; set; }
        public string HabitName { get; set; }
        public int Total { get; set; }

        #endregion

        #region [Rels]
        public HabitPhrase HabitPhrase { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public int TypeId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Type Type { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public List<CheckList> CheckList { get; set; } = new();

        public List<HabitPerformance> HabitPerformance { get; set; } = new();

        #endregion
    }
}
