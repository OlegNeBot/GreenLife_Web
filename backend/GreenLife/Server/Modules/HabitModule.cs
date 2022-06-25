using GreenLifeLib;
using Microsoft.EntityFrameworkCore;
using Nancy;
using System.Text.Json;

namespace Server
{
    public class HabitModule : NancyModule
    {
        public HabitModule() 
        {
            #region [Requests]

            Get("/habits", async (x) =>
            {
                string json = Request.Headers["Token"].First();
                string[] tokens = JsonSerializer.Deserialize<string[]>(json)!;
                WebToken webToken = new(tokens[0], tokens[1]);
                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                List<Habit> habits = new();
                List<CheckList> checklists = new();

                using (Context db = new())
                {
                    checklists = await db.CheckList.Where(p => p.AccountId == webToken.AccountId)
                                                   .Include(p => p.Habit)
                                                   .ToListAsync();

                    foreach (CheckList checkList in checklists)
                    {
                        foreach (Habit habit in checkList.Habit)
                        {
                            HabitPerformance hp = await db.HabitPerformance.Where(p => p.HabitId == habit.Id && p.AccountId == webToken.AccountId).FirstAsync();
                        }
                        habits.AddRange(checkList.Habit);
                    }
                }

                Response response = new();

                response.StatusCode = HttpStatusCode.OK;

                response.Headers["Access-Control-Allow-Origin"] = "*";
                response.Headers["Access-Control-Allow-Method"] = "GET";

                response.Headers["Token"] = JsonSerializer.Serialize(tokens);
                response.Headers["Habits"] = JsonSerializer.Serialize(habits);

                response.Headers["Access-Control-Expose-Headers"] = "Token, Habits";
                response.Headers["Content-Type"] = "application/json";
                return response;
            });

            Put("/perform/{id}", async (x) =>
            {
                string json = Request.Headers["Token"].First();
                string[] tokens = JsonSerializer.Deserialize<string[]>(json)!;
                WebToken webToken = new(tokens[0], tokens[1]);
                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                int habitId = x.id;
                HabitPerformance habitPerformance;
                using (Context db = new())
                {
                    habitPerformance = await db.HabitPerformance.Where(p => p.AccountId == webToken.AccountId)
                                                          .Where(p => p.HabitId == habitId)
                                                          .Include(p => p.Habit)
                                                          .ThenInclude(d => d.HabitPhrase)
                                                          .FirstAsync();
                    habitPerformance.NewExecution();
                }

                Response response = new();

                response.Headers["Access-Control-Allow-Origin"] = "*";
                response.Headers["Access-Control-Allow-Method"] = "PUT";

                response.Headers["Token"] = JsonSerializer.Serialize(tokens);
                response.Headers["Phrase"] = JsonSerializer.Serialize(habitPerformance.Habit.HabitPhrase);

                response.Headers["Access-Control-Expose-Headers"] = "Token, Phrase";
                response.Headers["Content-Type"] = "application/json";

                response.StatusCode = HttpStatusCode.OK;

                return response;
            });

            Get("/certificate/{id}", async (x) => 
            {
                string json = Request.Headers["Token"].First();
                string[] tokens = JsonSerializer.Deserialize<string[]>(json)!;
                WebToken webToken = new(tokens[0], tokens[1]);
                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                int habitId = x.id;

                Habit habit;
                Account account;
                using (Context db = new())
                {
                    habit = await db.Habit.Where(p => p.Id == habitId).FirstAsync();
                    account = await db.Account.Where(p => p.Id == webToken.AccountId).FirstAsync();
                }

                string hName = habit.HabitName;

                Response response = new();

                response.Headers["Access-Control-Allow-Origin"] = "*";
                response.Headers["Access-Control-Allow-Method"] = "GET";

                response.Headers["Token"] = JsonSerializer.Serialize(tokens);
                response.Headers["Account"] = JsonSerializer.Serialize(account.Name);
                response.Headers["Habit"] = JsonSerializer.Serialize(hName);

                response.Headers["Access-Control-Expose-Headers"] = "Token, Account, Habit";
                response.Headers["Content-Type"] = "application/json";

                response.StatusCode = HttpStatusCode.OK;

                return response;
            });

            #endregion
        }
    }
}
