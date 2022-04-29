using System.Collections.Generic;
using System.Linq;

namespace GreenLifeLib
{
    public class Planet
    {
        #region [Props]

        public int Id { get; set; }
        public string PlanetRef { get; set; }

        #endregion

        #region [Rels]

        public int StartPageId { get; set; }
        public StartPage StartPage { get; set; }

        public List<Color> Color { get; set; }

        public List<Element> Element { get; set; }

        #endregion

        #region [Methods]

        public static Planet GetPlanet(int id)
        {
            using (Context db = new())
            {
                var planet = db.Planet.Where(p => p.StartPageId == id).First();
                return planet;
            }
        }

        #endregion
    }
}

