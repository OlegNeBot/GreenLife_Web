using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLifeLib
{
    public class AccountAction
    {
        #region [Props]

        public int Id { get; set; }
        public DateTime ActionDate { get; set; }

        #endregion

        #region [Rels]

        public int AccountId { get; set; }
        public Account? Account { get; set; }

        public int ActionId { get; set; }
        public Action? Action { get; set; }

        #endregion

        #region [Methods]

        /// <summary>
        /// Adds an action to account. Action gains by its id.
        /// </summary>
        /// <param name="account">Account that performs an action.</param>
        /// <param name="actionId">Action id.</param>
        public static async void NewAction(Account account, int actionId)
        {
            using (Context db = new())
            {
                Action action = db.Action.Where(p => p.Id == actionId).First();
                AccountAction accountAction =  new() { ActionId = action.Id, AccountId = account.Id, ActionDate = DateTime.UtcNow };
                await db.AccountAction.AddAsync(accountAction);
                await db.SaveChangesAsync();
            }
        }

        #endregion
    }
}
