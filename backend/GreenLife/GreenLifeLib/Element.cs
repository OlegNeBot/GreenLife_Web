using System.Collections.Generic;
using System.Linq;

namespace GreenLifeLib
{
    public class Element : PlanetParts
    {
        #region [Fields]

        private List<Element> Elements;

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
                Elements = db.Element.ToList();
            }
        }

        public static List<Element> GetElements()
        {
            Element elm = new();
            elm.GetAll();
            return elm.Elements;
        }

        public static List<Element> GetElementsByPlanet(int id)
        {
            using (Context db = new())
            {
                var elements = db.Element.Where(p => p.PlanetId == id).ToList();
                return elements;
            }
        }

        #endregion
    }
}

