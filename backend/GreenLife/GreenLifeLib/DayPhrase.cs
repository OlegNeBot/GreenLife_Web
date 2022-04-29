using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenLifeLib
{
    public class DayPhrase
    {
        #region [Props]

        public int Id { get; set; }
        public string PhraseText { get; set; }

        #endregion

        #region [Methods]

        public static string GetRandomPhrase()
        {
            using (Context db = new())
            {
                var cnt = db.DayPhrase.Count();
                Random rnd = new();
                int num = rnd.Next(cnt);
                var phrase = db.DayPhrase.Where(p => p.Id == num + 1).First();
                return phrase.PhraseText;
            }
        }

        #endregion
    }
}

