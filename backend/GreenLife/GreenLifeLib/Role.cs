namespace GreenLifeLib
{
    public class Role
    {
        #region [Props]

        public int Id { get; set; }
        public string RoleName { get; set; }

        #endregion

        #region [Rels]

        public List<Account> Account { get; set; } = new();

        #endregion
    }
}
