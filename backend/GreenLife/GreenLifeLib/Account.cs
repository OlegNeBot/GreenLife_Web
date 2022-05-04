using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GreenLifeLib
{
    public class Account
    {
        #region [Props]

        public int Id { get; set; }
        public string Login { get; set; }

        private string _password = null;
        public string Password
        {
            get { return _password; }
            //set { _password = ToHash(value); }
            set { _password = value; }
        }
        public string Name { get; private set; }
        public string FamilyName { get; private set; }
        public enum Sex
        {
            Мужской,
            Женский,
            Другой
        }
        public Sex UserSex { get; set; }
        public DateTime DateOfBirth { get; private set; }
        public DateTime RegDate { get; private set; }
        public int ScoreSum { get; set; }

        #endregion

        #region [Rels]

        public int UserId { get; set; }
        public User User { get; set; }
        public List<UserAnswer> UserAnswer { get; set; }

        #endregion

        #region [Constructors]

        public Account()
        {

        }

        public Account(string login, string password, string name, string fname, string sex, DateTime? dOB, DateTime reg, int userId)
        {
            Login = login;
            Password = password;
            Name = name;
            FamilyName = fname;
            UserSex = ToSex(sex);
            DateOfBirth = (DateTime)dOB;
            RegDate = reg;
            ScoreSum = 0;
            UserId = userId;
        }

        #endregion

        #region [Methods]

        public static void AddAccount(Account acc)
        {
            using (Context db = new())
            {
                db.Account.Add(acc);
                db.SaveChanges();
            }
        }

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

        public static bool IsLoginAvailable(string login)
        {
            using (Context db = new())
            {
                var _accounts = db.Account;
                foreach (Account _acc in _accounts)
                {
                    if (_acc.Login.ToLower().Equals(login.ToLower()))
                        return false;
                }
                return true;
            }
        }

        private static Sex ToSex(string sex)
        {
            if (sex.Equals(Sex.Мужской.ToString()))
                return Sex.Мужской;
            else if (sex.Equals(Sex.Женский.ToString()))
                return Sex.Женский;
            else return Sex.Другой;
        }

        #endregion
    }
}
