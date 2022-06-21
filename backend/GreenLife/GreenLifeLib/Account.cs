using System.Security.Cryptography;
using System.Text;

namespace GreenLifeLib
{
    public class Account
    {
        #region [Props]

        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime RegDate { get; set; }
        public string Email { get; set; }
        public int ScoreSum { get; set; }

        #endregion

        #region [Rels]

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public List<CheckList> CheckList { get; set; } = new();

        public List<HabitPerformance> HabitPerformance { get; set; } = new();

        public List<AccountAction> AccountAction { get; set; } = new();

        public Token Token { get; set; }

        #endregion

        #region [Constructors]

        public Account()
        {

        }

        public Account(string email, string password, string name, DateTime regDate)
        {
            Password = password;
            Name = name;
            RegDate = regDate;
            Email = email;
            RoleId = 1;
        }

        #endregion

        #region [Methods]
        /// <summary>
        /// Creates an account when user registers.
        /// </summary>
        /// <param name="email">Account email.</param>
        /// <param name="password">Account password.</param>
        /// <param name="name">Account name.</param>
        /// <param name="regDate">Account register date.</param>
        public static async void CreateAccount(string email, string password, string name, DateTime regDate)
        {
            //Creating a new account
            Account account = new(email, password, name, regDate);
            await using (Context db = new())
            {
                AddAccount(account);
                //Then setting an action -> achievement for creating an account
                GreenLifeLib.AccountAction.NewAction(account, 4);

                Account acc = db.Account.Where(p => p.Email.Equals(email)).First();
                //Then creating checklists, habit performances etc.
                GreenLifeLib.CheckList.CreateAccountCheckLists(account);

            }
        }

        /// <summary>
        /// Adds an Account to database.
        /// </summary>
        /// <param name="acc">Account that should be added.</param>
        public static async void AddAccount(Account acc)
        {
            using (Context db = new())
            {
                await db.Account.AddAsync(acc);
                await db.SaveChangesAsync();
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
