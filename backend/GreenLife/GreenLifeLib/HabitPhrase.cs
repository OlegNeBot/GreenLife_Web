using System.Linq;

namespace GreenLifeLib
{
    public class HabitPhrase
    {
        #region [Props]

        public int Id { get; set; }
        public string PhraseText { get; set; }

        #endregion

        #region [Rels]

        public int HabitId { get; set; }
        public Habit Habit { get; set; }

        #endregion

        #region [Methods]

        public static HabitPhrase GetHabitPhrase(int id)
        {
            using (Context db = new())
            {
                var phrase = db.HabitPhrase.Where(p => p.Habit.Id == id).First();
                return phrase;
            }
        }

        #endregion
    }
}

