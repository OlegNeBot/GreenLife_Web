using GreenLifeLib;
using Microsoft.EntityFrameworkCore;
using Nancy;
using System.Text.Json;

namespace Server.Modules
{
    public class CheckListModule : NancyModule
    {
        public CheckListModule()
        {
            #region [Requests]

            Get("/checklists", async (x) =>
            {
                string json = Request.Headers["Token"].First();
                string[] tokens = JsonSerializer.Deserialize<string[]>(json)!;
                WebToken webToken = new(tokens[0], tokens[1]);
                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                List<CheckList> checklists = new();

                using (Context db = new())
                {
                    checklists = await db.CheckList.Where(p => p.AccountId == webToken.AccountId)
                                                   .Include(p => p.CheckListName)
                                                   .ToListAsync();
                }

                Response response = new();

                response.Headers["Access-Control-Allow-Origin"] = "*";
                response.Headers["Access-Control-Allow-Method"] = "GET";

                response.Headers["Token"] = JsonSerializer.Serialize(tokens);
                response.Headers["CheckLists"] = JsonSerializer.Serialize(checklists);

                response.Headers["Access-Control-Expose-Headers"] = "Token, CheckLists";
                response.Headers["Content-Type"] = "application/json";

                response.StatusCode = HttpStatusCode.OK;

                return response;
            });

            Get("/checklist/{id}", async (x) =>
            {
                string json = Request.Headers["Token"].First();
                string[] tokens = JsonSerializer.Deserialize<string[]>(json)!;
                WebToken webToken = new(tokens[0], tokens[1]);
                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                int id = x.id;
                List<Habit> habits = new();
                CheckList checklist;

                using (Context db = new())
                {
                    checklist = await db.CheckList.Where(p => p.Id == id)
                                                  .Include(p => p.Habit)
                                                  .FirstAsync();

                    foreach (Habit habit in checklist.Habit)
                    {
                        HabitPerformance hp = await db.HabitPerformance.Where(p => p.HabitId == habit.Id && p.AccountId == webToken.AccountId).FirstAsync();
                    }
                    habits.AddRange(checklist.Habit);
                }

                Response response = new();

                response.Headers["Access-Control-Allow-Origin"] = "*";
                response.Headers["Access-Control-Allow-Method"] = "GET";

                response.Headers["Token"] = JsonSerializer.Serialize(tokens);
                response.Headers["Habits"] = JsonSerializer.Serialize(habits);

                response.Headers["Access-Control-Expose-Headers"] = "Token, Habits";
                response.Headers["Content-Type"] = "application/json";

                response.StatusCode = HttpStatusCode.OK;

                return response;
            });

            #endregion
        }
    }
}
