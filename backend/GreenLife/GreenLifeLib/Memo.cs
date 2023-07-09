namespace GreenLifeLib
{
    /// <summary>
    /// Памятка.
    /// </summary>
    public class Memo
    {
        #region [Props]
        // TODO: Добавить возможность хранения в БД картинки, а не ссылки на нее.
        public int Id { get; set; }
        public string? MemoName { get; set; }
        public string? MemoRef { get; set; }

        #endregion
    }
}
