namespace GreenLifeLib
{
    /// <summary>
    /// Название чек-листа.
    /// </summary>
    public class CheckListName
    {
        #region [Props]

        [System.Text.Json.Serialization.JsonIgnore]
        public int Id { get; set; }

        public string? Name { get; set; } = null!;

        #endregion

        #region [Rels]

        [System.Text.Json.Serialization.JsonIgnore]
        public List<CheckList> CheckList { get; set; } = new();

        #endregion
    }
}
