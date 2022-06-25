namespace GreenLifeLib
{
    public class ActionType
    {
        #region [Props]

        [System.Text.Json.Serialization.JsonIgnore]
        public int Id { get; set; }

        public string TypeName { get; set; }

        #endregion

        #region [Rels]

        [System.Text.Json.Serialization.JsonIgnore]
        public List<Action> Action { get; set; } = new();

        #endregion
    }
}
