using System.Collections.Generic;

namespace GreenLifeLib
{
    public class User
    {
        #region [Props]

        public int Id { get; set; }

        #endregion

        #region [Rels]

        public StartPage StartPage { get; set; }

        public Account Account { get; set; }

        public List<CheckList> CheckList { get; set; }

        public List<HabitPerformance> HabitPerformance { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        #endregion

    }
}

