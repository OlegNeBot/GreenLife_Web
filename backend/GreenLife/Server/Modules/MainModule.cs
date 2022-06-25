using Nancy;
using System.Text.Json;
using GreenLifeLib;
using Microsoft.EntityFrameworkCore;

namespace Server.Modules
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            #region [Requests]

            Get("/main", async (x) =>
            {
                string json = Request.Headers["Token"].First();
                string[] tokens = JsonSerializer.Deserialize<string[]>(json)!;
                WebToken webToken = new(tokens[0], tokens[1]);
                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                Account account;

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

            #endregion
        }
    }
}
