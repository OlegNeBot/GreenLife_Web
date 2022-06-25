namespace GreenLifeLib
{
    public class Token
    {
        #region [Constructor]

        public Token(int accountId, string token)
        { 
            AccountId = accountId;
            UserToken = token;
        }

        public Token()
        { 
            
        }

        #endregion

        #region [Props]

        public int Id { get; set; }

        public string UserToken { get; set; } = null!;

        #endregion

        #region [Rels]

        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;

        #endregion
    }
}
