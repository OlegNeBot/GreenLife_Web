using Microsoft.EntityFrameworkCore;

namespace GreenLifeLib
{
    /// <summary>
    /// Действие аккаунта.
    /// </summary>
    public class AccountAction
    {
        #region [Props]

        public int Id { get; set; }
        public string? ActionDate { get; set; }

        #endregion

        #region [Rels]

        [System.Text.Json.Serialization.JsonIgnore]
        public int AccountId { get; set; }

        public Account? Account { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public int ActionId { get; set; }

        public Action? Action { get; set; }

        #endregion

        #region [Methods]

        // TODO: Исправить метод.

        /// <summary>
        /// Adds an action to account. Action gains by its id.
        /// </summary>
        /// <param name="account">Account that performs an action.</param>
        /// <param name="actionId">Action id.</param>
        public static async void NewAction(int accountId, int actionId)
        {
            using (var db = new Context())
            {
                var account = await db.Account
                                        .Where(p => p.Id == accountId)
                                        .FirstAsync();
                var action = await db.Action
                                        .Where(p => p.Id == actionId)
                                        .FirstAsync();
                AccountAction accountAction = new()
                {
                    ActionId = action.Id,
                    Action = action,
                    Account = account,
                    AccountId = account.Id,
                    ActionDate = DateTime.UtcNow.ToShortDateString()

                };
                await db.AccountAction.AddAsync(accountAction);
                await db.SaveChangesAsync();
            }
        }

        #endregion
    }
}
