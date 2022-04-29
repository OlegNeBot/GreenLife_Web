using System.Collections.Generic;
using System.Linq;

namespace GreenLifeLib
{
    public class Question
    {
        #region [Props]

        public int Id { get; set; }
        public string QuestText { get; set; }

        #endregion

        #region [Rels]

        public List<Answer> Answer { get; set; }
        public List<UserAnswer> UserAnswer { get; set; }

        #endregion

        #region [Methods]

        public static List<Question> GetQuestions()
        {
            using (Context db = new())
            {
                var questions = db.Question.ToList();
                return questions;
            }
        }

        #endregion
    }
}

