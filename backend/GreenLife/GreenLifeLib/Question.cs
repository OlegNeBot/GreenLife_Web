using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLifeLib
{
    public class Question
    {
        #region [Props]

        public int Id { get; set; }
        public string QuestionText { get; set; } = null!;

        #endregion

        #region [Rels]

        public List<Answer> Answers { get; set; } = new();

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
