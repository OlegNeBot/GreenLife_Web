using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GreenLifeLib
{
    public class Answer
    {
        #region [Props]

        public int Id { get; set; }
        public string AnswerText { get; set; }

        #endregion

        #region [Rels]

        public Question Question { get; set; }
        public List<UserAnswer> UserAnswer { get; set; }

        #endregion

        #region [Methods]

        public static List<Answer> GetAnswersByQuestion(int id)
        {
            using (Context db = new())
            {
                var answers = db.Answer.Include(p => p.Question).Where(p => p.Question.Id == id).ToList();
                return answers;
            }
        }

        #endregion
    }
}

