namespace GreenLifeLib
{
    /// <summary>
    /// Вопрос анкеты.
    /// </summary>
    public class Question
    {
        #region [Props]

        public int Id { get; set; }
        public string QuestionText { get; set; } = null!;

        #endregion

        #region [Rels]

        public List<Answer> Answers { get; set; } = new();

        #endregion
    }
}
