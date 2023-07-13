namespace GreenLifeLib
{
    /// <summary>
    /// JWT-токен для хранения в БД.
    /// </summary>
    public class Token
    {
        #region [Constructors]

        /// <summary>
        /// Токен по id аккаунта и refresh-токену.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="token"></param>
        public Token(int accountId, string token)
        { 
            AccountId = accountId;
            UserToken = token;
        }

        /// <summary>
        /// Пустой конструктор для EF.
        /// </summary>
        public Token() { }

        #endregion

        #region [Props]

        public int Id { get; set; }

        public string? UserToken { get; set; }

        #endregion

        #region [Rels]

        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;

        #endregion
    }
}
