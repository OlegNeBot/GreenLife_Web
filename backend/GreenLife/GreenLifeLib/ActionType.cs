namespace GreenLifeLib
{
    public class ActionType
    {
        #region [Props]

        public int Id { get; set; }

        public string TypeName { get; set; }

        #endregion

        #region [Rels]

        public List<Action> Action { get; set; } = new();

        #endregion
    }
}
