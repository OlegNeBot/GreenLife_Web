using Microsoft.EntityFrameworkCore;


namespace GreenLifeLib
{
    public class Habit
    {
        #region [Props]

        public int Id { get; set; }
        public int Score { get; set; }
        public string HabitName { get; set; }
        public int Total { get; set; }

        #endregion

        #region [Rels]

        public HabitPhrase HabitPhrase { get; set; }

        public int TypeId { get; set; }
        public Type Type { get; set; }

        public List<CheckList> CheckList { get; set; } = new();

        public List<HabitPerformance> HabitPerformance { get; set; } = new();

        #endregion

        #region [Methods]
        /// <summary>
        /// Gets all Habits of CheckList by CheckList id.
        /// </summary>
        /// <param name="id">CheckList id.</param>
        /// <returns>List of CheckList Habits.</returns>
        public static List<Habit> GetHabitsOfCheckList(int id)
        {
            using (Context db = new())
            {
                var checkList = db.CheckList.Include(p => p.Habit).Where(p => p.Id == id).First();
                List<Habit> habits = new();
                foreach (Habit habit in checkList.Habit)
                {
                    habits.Add(habit);
                }
                return habits;
            }
        }

        #endregion
    }
}
