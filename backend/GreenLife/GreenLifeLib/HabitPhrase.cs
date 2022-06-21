using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Gets the habit phrase by habit id.
        /// </summary>
        /// <param name="id">Habit id.</param>
        /// <returns>Habit phrase.</returns>
        public static HabitPhrase GetHabitPhrase(int id)
        {
            using (Context db = new())
            {
                var phrase = db.HabitPhrase.Where(p => p.Habit.Id == id).FirstOrDefault();
                return phrase;
            }
        }

        #endregion
    }
}
