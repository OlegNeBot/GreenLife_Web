using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GreenLifeLib
{
    public class UserAnswer
    {
        #region [Props]

        public int Id { get; set; }

        #endregion

        #region [Rels]

        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public Account Account { get; set; }

        #endregion

        #region [Methods]

        public static List<UserAnswer> GetUserAnswers(int id)
        {
            using (Context db = new())
            {
                var answers = db.UserAnswer.Include(p => p.Account).Where(p => p.Account.Id == id).ToList();
                return answers;
            }
        }

        public static void AddUserAnswer(UserAnswer answ)
        {
            using (Context db = new())
            {
                db.Add(answ);
                db.SaveChanges();
            }
        }

        #endregion
    }
}


