using System.Collections.Generic;
using System.Linq;

namespace GreenLifeLib
{
    public class Memo
    {
        #region [Props]

        public int Id { get; set; }
        public string MemoName { get; set; }
        public string MemoRef { get; set; }

        #endregion

        #region [Methods]

        public static List<Memo> GetMemos()
        {
            using (Context db = new())
            {
                var memos = db.Memo.ToList();
                return memos;
            }
        }

        #endregion
    }
}

