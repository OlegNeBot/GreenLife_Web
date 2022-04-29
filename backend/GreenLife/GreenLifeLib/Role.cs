using System.Collections.Generic;

namespace GreenLifeLib
{
    public class Role
    {
        #region [Props]

        public int Id { get; set; }
        public string UserRole { get; set; }

        #endregion

        #region [Rels]

        public List<User> User { get; set; }

        #endregion
    }
}
