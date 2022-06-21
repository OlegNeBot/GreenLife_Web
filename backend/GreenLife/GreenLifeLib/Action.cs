using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLifeLib
{
    public class Action
    {
        #region [Props]

        public int Id { get; set; }

        public string ActionName { get; set; }

        #endregion

        #region [Rels]

        public int ActionTypeId { get; set; }
        public ActionType ActionType { get; set; }

        public List<AccountAction> AccountAction { get; set; } = new();

        #endregion
    }
}
