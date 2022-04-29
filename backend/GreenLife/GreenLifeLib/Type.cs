using System.Collections.Generic;
using System.Linq;

namespace GreenLifeLib
{
    public class Type
    {
        #region [Props]

        public int Id { get; set; }
        public string NameType { get; set; }

        #endregion

        #region [Rels]

        public List<Habit> Habit { get; set; }

        public List<CheckList> CheckList { get; set; }

        #endregion

        #region [Methods]

        public List<Habit> GetHabitsByType(int id)
        {
            using (Context db = new())
            {
                var habits = db.Habit.Where(p => p.Type.Id == id).ToList();
                return habits;
            }
        }

        public CheckList GetChecklistByType(int id)
        {
            using (Context db = new())
            {
                var checklist = db.CheckList.Where(p => p.Type.Id == id).First();
                return checklist;
            }
        }

        #endregion
    }
}

