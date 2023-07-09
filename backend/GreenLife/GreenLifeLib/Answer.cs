namespace GreenLifeLib
{
    /// <summary>
    /// Ответ пользователя на анкету.
    /// </summary>
    public class Answer
    {
        #region [Props]

        public int Id { get; set; }
        public string? AnswerText { get; set; }

        #endregion

        #region [Rels]

        public int QuestionId { get; set; }
        public Question? Question { get; set; }

        #endregion
    }
}
