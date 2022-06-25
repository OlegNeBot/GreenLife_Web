namespace GreenLifeLib
{
    public class Action
    {
        #region [Props]

        [System.Text.Json.Serialization.JsonIgnore]
        public int Id { get; set; }

        public string ActionName { get; set; }

        #endregion

        #region [Rels]

        [System.Text.Json.Serialization.JsonIgnore]
        public int ActionTypeId { get; set; }

        public ActionType ActionType { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public List<AccountAction> AccountAction { get; set; } = new();

        #endregion

        #region [Constructors]

        public Action() { }

        #endregion
    }
}
