namespace GreenLifeLib
{
    /// <summary>
    /// Тип выполненного пользователем действия.
    /// </summary>
    public class ActionType
    {
        // TODO: Подумать о необходимости.
        #region [Props]

        [System.Text.Json.Serialization.JsonIgnore]
        public int Id { get; set; }

        public string? TypeName { get; set; }

        #endregion

        #region [Rels]

        [System.Text.Json.Serialization.JsonIgnore]
        public List<Action> Action { get; set; } = new();

        #endregion
    }
}
