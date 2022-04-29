using System.Collections.Generic;
using System.Linq;

namespace GreenLifeLib
{
    public class CheckList
    {
        #region [Fields]

        public string[] _checklistNames = new string[] { "Название 1", "Название 2", "Название 3", "Название 4", "Название 5" };

        #endregion

        #region [Props]

        public int Id { get; set; }
        public bool ExecStatus { get; set; }
        public string CheckListName { get; set; }

        #endregion

        #region [Rels]

        public int TypeId { get; set; }
        public Type Type { get; set; }

        public List<Habit> Habit { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        #endregion

        #region [Methods]

        public static List<CheckList> GetCheckLists(int id)
        {
            using (Context db = new())
            {
                var checkLists = db.CheckList.Where(p => p.UserId == id).ToList();
                return checkLists;
            }
        }

        #endregion
    }
}

