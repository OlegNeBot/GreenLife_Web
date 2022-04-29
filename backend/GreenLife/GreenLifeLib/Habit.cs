using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GreenLifeLib
{
    public class Habit
    {
        #region [Props]

        public int Id { get; set; }
        public int Score { get; set; }
        public string HabitName { get; set; }
        public int NumsNeeded { get; set; }
        public string ExecProperty { get; set; }

        #endregion

        #region [Rels]

        public HabitPhrase HabitPhrase { get; set; }

        public int TypeId { get; set; }
        public Type Type { get; set; }

        public int CheckListId { get; set; }
        public List<CheckList> CheckList { get; set; }

        public List<HabitPerformance> HabitPerformance { get; set; }

        #endregion

        #region [Methods]
        public static List<Habit> GetHabitsOfCheckList(int id)
        {
            using (Context db = new())
            {
                var chList = db.CheckList.Include(p => p.Habit).Where(p => p.Id == id).First();
                List<Habit> habits = new();
                foreach (Habit h in chList.Habit)
                {
                    habits.Add(h);
                }
                return habits;
            }
        }

        #endregion
    }
}

