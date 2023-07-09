using System.Security.Cryptography;
using System.Text;

namespace GreenLifeLib
{
    /// <summary>
    /// Аккаунт.
    /// </summary>
    public class Account
    {
        #region [Props]

        public int Id { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? RegDate { get; set; }
        public string? Email { get; set; }
        public int ScoreSum { get; set; }

        #endregion

        #region [Rels]

        [System.Text.Json.Serialization.JsonIgnore]
        public int RoleId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Role? Role { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public List<CheckList> CheckList { get; set; } = new();

        [System.Text.Json.Serialization.JsonIgnore]
        public List<HabitPerformance> HabitPerformance { get; set; } = new();

        [System.Text.Json.Serialization.JsonIgnore]
        public List<AccountAction> AccountAction { get; set; } = new();

        [System.Text.Json.Serialization.JsonIgnore]
        public Token? Token { get; set; }

        #endregion

        #region [Constructors]
        // TODO: Разобраться в конструкторах.

        public Account()
        {

        }

        public Account(string email, string password, string name, string regDate)
        {
            Password = password;
            Name = name;
            RegDate = regDate;
            Email = email;
            RoleId = 1;
        }

        #endregion

        #region [Methods]

        // TODO: Исправить методы.

        /// <summary>
        /// Creates an account when user registers.
        /// </summary>
        /// <param name="email">Account email.</param>
        /// <param name="password">Account password.</param>
        /// <param name="name">Account name.</param>
        /// <param name="regDate">Account register date.</param>
        public static void CreateAccount(string email, string password, string name, string regDate)
        {
            //Creating a new account
            Account account = new(email, password, name, regDate);
            AddAccount(account);
            using (Context db = new())
            {
                //Then setting an action -> achievement for creating an account
                GreenLifeLib.AccountAction.NewAction(account.Id, 4);

                //Then creating checklists, habit performances etc.
                GreenLifeLib.CheckList.CreateAccountCheckLists(account);
            }
        }

        /// <summary>
        /// Adds an Account to database.
        /// </summary>
        /// <param name="acc">Account that should be added.</param>
        public static void AddAccount(Account acc)
        {
            using (Context db = new())
            {
                db.Account.Add(acc);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Checks the email availableness. If available - returns "true", if not - "false".
        /// </summary>
        /// <param name="email">Email that should be checked.</param>
        /// <returns>Returns "true" if email is available and "false" if not.</returns>
        public static bool IsEmailAvailable(string email)
        {
            using (Context db = new())
            {
                var account = db.Account.Where(p => p.Email.Equals(email)).FirstOrDefault();
                if (account != null)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Transforms the password into hash string.
        /// </summary>
        /// <param name="pass">Password needed to be in hash.</param>
        /// <returns>Hashed password.</returns>
        public static string ToHash(string pass)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] _computed = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));
                var _builder = new StringBuilder();
                for (int i = 0; i < _computed.Length; i++)
                    _builder.Append(_computed[i].ToString("x2"));
                return _builder.ToString();
            }
        }

        #endregion
    }
}
