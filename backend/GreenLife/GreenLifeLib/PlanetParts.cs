namespace GreenLifeLib
{
    public abstract class PlanetParts
    {
        #region [Props]

        public abstract int Id { get; set; }
        public abstract string Name { get; set; }
        public abstract string Content { get; set; }

        #endregion

        #region [Methods]

        internal abstract void GetAll();

        #endregion
    }
}
