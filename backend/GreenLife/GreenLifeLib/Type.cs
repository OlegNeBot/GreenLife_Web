namespace GreenLifeLib
{
    /// <summary>
    /// Тип чеклистов и привычек.
    /// </summary>
    public class Type
    {
        #region [Props]

        public int Id { get; set; }
        public string? TypeName { get; set; }

        #endregion

        #region [Rels]

        public List<Habit> Habit { get; set; } = new();

        public List<CheckList> CheckList { get; set; } = new();

        #endregion

    }
}
