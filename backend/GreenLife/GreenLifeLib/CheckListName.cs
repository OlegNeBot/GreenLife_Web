using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLifeLib
{
    public class CheckListName
    {
        #region [Props]

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion

        #region [Rels]

        public List<CheckList> CheckList { get; set; } = new();

        #endregion
    }
}
