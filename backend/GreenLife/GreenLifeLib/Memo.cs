using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Method that gets all the memos from database
        /// </summary>
        /// <returns>A list of memos</returns>
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
