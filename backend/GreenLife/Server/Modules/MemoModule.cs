using System.Text.Json;
using GreenLifeLib;
using Microsoft.EntityFrameworkCore;
using Nancy;

namespace Server.Modules
{
    public class MemoModule : NancyModule
    {
        public MemoModule()
        {
            #region [Requests]

            Get("/memos", async (x) =>
            {
                string json = Request.Headers["Token"].First();
                string[] tokens = JsonSerializer.Deserialize<string[]>(json)!;
                WebToken webToken = new(tokens[0], tokens[1]);
                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                List<Memo> memos = new();

                using (Context db = new())
                {
                    memos = await db.Memo.ToListAsync();
                }

                Response response = new();

                response.Headers["Access-Control-Allow-Origin"] = "*";
                response.Headers["Access-Control-Allow-Method"] = "GET";

                response.Headers["Token"] = JsonSerializer.Serialize(tokens);
                response.Headers["Memos"] = JsonSerializer.Serialize(memos);

                response.Headers["Access-Control-Expose-Headers"] = "Token, Memos";
                response.Headers["Content-Type"] = "application/json";

                response.StatusCode = HttpStatusCode.OK;

                return response;
            });

            #endregion
        }
    }
}
