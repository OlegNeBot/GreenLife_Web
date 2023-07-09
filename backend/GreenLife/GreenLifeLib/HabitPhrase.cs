namespace GreenLifeLib
{
    /// <summary>
    /// Фраза при выполнении привычки.
    /// </summary>
    public class HabitPhrase
    {
        #region [Props]

        [System.Text.Json.Serialization.JsonIgnore]
        public int Id { get; set; }
        public string? PhraseText { get; set; }

        #endregion

        #region [Rels]

        [System.Text.Json.Serialization.JsonIgnore]
        public int HabitId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Habit? Habit { get; set; }

        #endregion
    }
}
