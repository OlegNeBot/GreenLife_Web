using Microsoft.EntityFrameworkCore;

namespace GreenLifeLib
{
    public class Answer
    {
        #region [Props]

        public int Id { get; set; }
        public string AnswerText { get; set; } = null!;

        #endregion

        #region [Rels]

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        #endregion

        #region [Methods]

        public static List<Answer> GetAnswersByQuestion(int id)
        {
            using (Context db = new())
            {
                var question = db.Question.Where(p => p.Id == id).First();
                return question.Answers;
            }
        }

        #endregion
    }
}
