using System.Collections.Generic;
using System.Linq;

namespace GreenLifeLib
{
    public class Color : PlanetParts
    {
        #region [Fields]

        private List<Color> Colors;

        #endregion

        #region [Props]

        public override int Id { get; set; }
        public override string Name { get; set; }
        public override string Content { get; set; }

        #endregion

        #region [Rels]

        public int PlanetId { get; set; }
        public List<Planet> Planet { get; set; }

        #endregion

        #region [Methods]

        internal override void GetAll()
        {
            using (Context db = new())
            {
                Colors = db.Color.ToList();
            }
        }

        public static List<Color> GetColors()
        {
            Color clr = new();
            clr.GetAll();
            return clr.Colors;
        }

        public static List<Color> GetColorsByPlanet(int id)
        {
            using (Context db = new())
            {
                /* var allRels = db.PlanetColors.Where(p => p.Planet.Id == id).ToList();
                 List<Color> Colors = new();
                 foreach (PlanetColors rel in allRels)
                 {
                     var color = db.Color.Where(e => e.Id == rel.ColorId).First();
                     Colors.Add(color);
                 }
                 return Colors;*/
                var colors = db.Color.Where(p => p.PlanetId == id).ToList();
                return colors;
            }
        }

        #endregion
    }
}
