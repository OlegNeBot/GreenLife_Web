using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GreenLifeLib
{
    public class StartPage
    {
        #region [Props]

        public int Id { get; set; }

        #endregion

        #region [Rels]

        public int PlanetId { get; set; }
        public Planet Planet { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        #endregion

        #region [Methods]

        public static StartPage GetPageByUser(int id)
        {
            using (Context db = new())
            {
                var page = db.StartPage.Where(p => p.User.Id == id).First();
                return page;
            }
        }

        #endregion
    }
}

