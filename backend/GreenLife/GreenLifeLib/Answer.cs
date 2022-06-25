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
    }
}
