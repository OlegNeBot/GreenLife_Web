using System.Text.Json;
using GreenLifeLib;
using Microsoft.EntityFrameworkCore;
using Nancy;
using System.IO;
using NPOI.XSSF.UserModel;

namespace Server.Modules
{
    public class AccountModule : NancyModule
    {
        public AccountModule()
        {
            #region [Requests]

            Get("/account", async (x) =>
            {
                string json = Request.Headers["Token"].First();
                string[] tokens = JsonSerializer.Deserialize<string[]>(json)!;
                WebToken webToken = new(tokens[0], tokens[1]);
                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                Account account = new();
                using (Context db = new())
                {
                    account = await db.Account.Where(p => p.Id == webToken.AccountId)
                                             .FirstAsync();
                }

                Response response = new();

                response.Headers["Access-Control-Allow-Origin"] = "*";
                response.Headers["Access-Control-Allow-Method"] = "GET";

                response.Headers["Token"] = JsonSerializer.Serialize(tokens);
                response.Headers["Account"] = JsonSerializer.Serialize(account);

                response.Headers["Access-Control-Expose-Headers"] = "Token, Account";
                response.Headers["Content-Type"] = "application/json";

                response.StatusCode = HttpStatusCode.OK;

                return response;
            });

            Get("/report", async (x) => 
            {
                string json = Request.Headers["Token"].First();
                string[] tokens = JsonSerializer.Deserialize<string[]>(json)!;
                WebToken webToken = new(tokens[0], tokens[1]);
                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                Account account;
                int habits = 0;
                int checklists = 0;
                using (Context db = new())
                {
                    account = await db.Account.Where(p => p.Id == webToken.AccountId)
                                              .Include(p => p.HabitPerformance)
                                              .Include(p => p.CheckList)
                                              .FirstAsync();

                    foreach (HabitPerformance hp in account.HabitPerformance)
                    {
                        if (hp.Executed)
                        {
                            habits++;
                        }
                    }
                    foreach (CheckList chl in account.CheckList)
                    {
                        if (chl.ExecutionStatus)
                        {
                            checklists++;
                        }
                    }    
                }

                Response response = new();

                response.Headers["Access-Control-Allow-Origin"] = "*";
                response.Headers["Access-Control-Allow-Method"] = "GET";

                response.Headers["Token"] = JsonSerializer.Serialize(tokens);
                response.Headers["Account"] = JsonSerializer.Serialize(account);
                response.Headers["Habits"] = JsonSerializer.Serialize(habits);
                response.Headers["Checklists"] = JsonSerializer.Serialize(checklists);

                response.Headers["Access-Control-Expose-Headers"] = "Token, Account, Habits, Checklists";
                response.Headers["Content-Type"] = "application/json";

                response.StatusCode = HttpStatusCode.OK;

                return response;
            });

            #endregion
        }
    }
}
