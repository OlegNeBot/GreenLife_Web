using GreenLifeLib;
using Microsoft.EntityFrameworkCore;
using Nancy;
using System.Text.Json;

namespace Server.Modules
{
    public class HistoryModule : NancyModule
    {
        public HistoryModule()
        {
            #region [Requests]

            Get("/achievements", async (x) =>
            {
                string json = Request.Headers["Token"].First();
                string[] tokens = JsonSerializer.Deserialize<string[]>(json)!;
                WebToken webToken = new(tokens[0], tokens[1]);
                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                List<AccountAction> accountActions = new();

                using (Context db = new())
                {
                    accountActions = await db.AccountAction.Where(p => p.AccountId == webToken.AccountId && p.Action.ActionTypeId == 2)
                                                             .Include(p => p.Action)
                                                             .ThenInclude(d => d.ActionType)
                                                             .Include(p => p.Account)
                                                             .Union(db.AccountAction.Where(p => p.AccountId == webToken.AccountId && p.Action.ActionTypeId == 1)
                                                                                    .Include(p => p.Action)
                                                                                    .ThenInclude(d => d.ActionType)
                                                                                    .Include(p => p.Account)
                                                                                    .OrderByDescending(p => p.ActionDate)
                                                                                    .Take(10))
                                                             .ToListAsync();
                }

                Response response = new();

                response.Headers["Access-Control-Allow-Origin"] = "*";
                response.Headers["Access-Control-Allow-Method"] = "GET";

                response.Headers["Token"] = JsonSerializer.Serialize(tokens);
                response.Headers["Actions"] = JsonSerializer.Serialize(accountActions);

                response.Headers["Access-Control-Expose-Headers"] = "Token, Actions";
                response.Headers["Content-Type"] = "application/json";

                response.StatusCode = HttpStatusCode.OK;

                return response;
            });

            #endregion
        }
    }
}
