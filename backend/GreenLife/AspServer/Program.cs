using GreenLifeLib;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Principal;
using System.Text.Json;

namespace AspServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors();

            var app = builder.Build();

            app.UseCors(builder => builder.AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .WithExposedHeaders("Token", "Accept", "Content-Type",
                                            "Referer", "Sec-Ch-Ua", "Sec-Ch-Ua-Mobile",
                                            "Sec-Ch-Ua-Platform", "User-Agent"));

            app.MapGet("/main", async (HttpContext context) => 
            {
                var json = context.Request.Headers["Token"].First();
                var tokens = JsonSerializer.Deserialize<string[]>(json)!;
                var webToken = new WebToken(tokens[0], tokens[1]);

                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                using var db = new Context();

                var account = await db.Account
                                            .Where(p => p.Id == webToken.AccountId)
                                             .FirstAsync();

                context.Response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = (int)HttpStatusCode.OK;

                return JsonSerializer.Serialize(account);
            });

            app.MapGet("/memos", async (HttpContext context) =>
            {
                var json = context.Request.Headers["Token"].First();
                var tokens = JsonSerializer.Deserialize<string[]>(json)!;
                var webToken = new WebToken(tokens[0], tokens[1]);

                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                using var db = new Context();

                var memos = await db.Memo
                                        .ToListAsync();

                context.Response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                return JsonSerializer.Serialize(memos);
            });

            app.MapGet("/achievements", async (HttpContext context) =>
            {
                var json = context.Request.Headers["Token"].First();
                var tokens = JsonSerializer.Deserialize<string[]>(json)!;
                var webToken = new WebToken(tokens[0], tokens[1]);

                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                using var db = new Context();

                var accountActions = await db.AccountAction
                                                    .Where(p => p.AccountId == webToken.AccountId && p.Action.ActionTypeId == 2)
                                                    .Include(p => p.Action)
                                                    .ThenInclude(d => d.ActionType)
                                                    .Include(p => p.Account)
                                                    .Union(db.AccountAction
                                                        .Where(p => p.AccountId == webToken.AccountId && p.Action.ActionTypeId == 1)
                                                        .Include(p => p.Action)
                                                        .ThenInclude(d => d.ActionType)
                                                        .Include(p => p.Account)
                                                        .OrderByDescending(p => p.ActionDate)
                                                        .Take(10))
                                                    .ToListAsync();

                context.Response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = (int)HttpStatusCode.OK;

                return JsonSerializer.Serialize(accountActions);
            });

            app.MapGet("/habits", async (HttpContext context) =>
            {
                var json = context.Request.Headers["Token"].First();
                var tokens = JsonSerializer.Deserialize<string[]>(json)!;
                var webToken = new WebToken(tokens[0], tokens[1]);

                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                var habits = new List<Habit>();
                using var db = new Context();

                var checklists = await db.CheckList
                                                .Where(p => p.AccountId == webToken.AccountId)
                                                .Include(p => p.Habit)
                                                .ToListAsync();

                foreach (var checkList in checklists)
                {
                    foreach (var habit in checkList.Habit)
                    {
                        // TODO: Вспомнить все: а для чего эта строчка?
                        var hp = await db.HabitPerformance
                                                    .Where(p => p.HabitId == habit.Id && p.AccountId == webToken.AccountId)
                                                    .ToListAsync();
                        habit.HabitPerformance = hp;
                    }
                    habits.AddRange(checkList.Habit);
                }

                context.Response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                return JsonSerializer.Serialize(habits);
            });

            app.MapPut("/perform/{id}", async (int id, HttpContext context) =>
            {
                var json = context.Request.Headers["Token"].First();
                var tokens = JsonSerializer.Deserialize<string[]>(json)!;
                var webToken = new WebToken(tokens[0], tokens[1]);

                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                using var db = new Context();

                var habitPerformance = await db.HabitPerformance
                                                            .Where(p => p.AccountId == webToken.AccountId && p.HabitId == id)
                                                            .Include(p => p.Habit)
                                                            .ThenInclude(d => d.HabitPhrase)
                                                            .FirstAsync();

                habitPerformance.NewExecution();

                context.Response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                return JsonSerializer.Serialize(habitPerformance.Habit.HabitPhrase);
            });

            app.MapGet("/certificate/{id}", async (int id, HttpContext context) =>
            {
                var json = context.Request.Headers["Token"].First();
                var tokens = JsonSerializer.Deserialize<string[]>(json)!;
                var webToken = new WebToken(tokens[0], tokens[1]);

                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                using var db = new Context();

                var habit = await db.Habit
                                        .Where(p => p.Id == id)
                                        .FirstAsync();

                var account = await db.Account
                                            .Where(p => p.Id == webToken.AccountId)
                                            .FirstAsync();

                context.Response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                return JsonSerializer.Serialize(new string[2] { account.Name, habit.HabitName });
            });

            app.MapGet("/checklists", async (HttpContext context) =>
            {
                var json = context.Request.Headers["Token"].First();
                var tokens = JsonSerializer.Deserialize<string[]>(json)!;
                var webToken = new WebToken(tokens[0], tokens[1]);

                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                using var db = new Context();

                var checklists = await db.CheckList
                                                .Where(p => p.AccountId == webToken.AccountId)
                                                .Include(p => p.CheckListName)
                                                .ToListAsync();

                context.Response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                return JsonSerializer.Serialize(checklists);
            });

            app.MapGet("/checklist/{id}", async (int id, HttpContext context) =>
            {
                var json = context.Request.Headers["Token"].First();
                var tokens = JsonSerializer.Deserialize<string[]>(json)!;
                var webToken = new WebToken(tokens[0], tokens[1]);

                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                using var db = new Context();

                var checklist = await db.CheckList
                                                .Where(p => p.Id == id)
                                                .Include(p => p.Habit)
                                                .FirstAsync();

                // TODO: Вспомнить все: а для чего эта штука?
                foreach (var habit in checklist.Habit)
                {
                    var hp = await db.HabitPerformance
                                                    .Where(p => p.HabitId == habit.Id && p.AccountId == webToken.AccountId)
                                                    .ToListAsync();
                    habit.HabitPerformance = hp;
                }

                context.Response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                return JsonSerializer.Serialize(checklist.Habit);
            });

            app.MapGet("/account", async (HttpContext context) => 
            {
                var json = context.Request.Headers["Token"].First();
                var tokens = JsonSerializer.Deserialize<string[]>(json)!;
                var webToken = new WebToken(tokens[0], tokens[1]);

                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                using var db = new Context();

                var account = await db.Account
                                            .Where(p => p.Id == webToken.AccountId)
                                            .FirstAsync();

                context.Response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                return JsonSerializer.Serialize(account);
            });

            app.MapGet("/report", async (HttpContext context) =>
            {
                var json = context.Request.Headers["Token"].First();
                var tokens = JsonSerializer.Deserialize<string[]>(json)!;
                var webToken = new WebToken(tokens[0], tokens[1]);

                tokens[0] = webToken.AccessToken;
                tokens[1] = webToken.RefreshToken;

                int habits = 0;
                int checklists = 0;

                using var db = new Context();

                var account = await db.Account
                                            .Where(p => p.Id == webToken.AccountId)
                                            .Include(p => p.HabitPerformance)
                                            .Include(p => p.CheckList)
                                             .FirstAsync();

                foreach (var hp in account.HabitPerformance)
                {
                    if (hp.Executed)
                    {
                        habits++;
                    }
                }

                foreach (var chl in account.CheckList)
                {
                    if (chl.ExecutionStatus)
                    {
                        checklists++;
                    }
                }

                // TODO: Сделать отдельную сущность для отчета.

                context.Response.Headers["Token"] = JsonSerializer.Serialize(tokens);
                context.Response.Headers["Account"] = JsonSerializer.Serialize(account);
                context.Response.Headers["Habits"] = JsonSerializer.Serialize(habits);
                context.Response.Headers["Checklists"] = JsonSerializer.Serialize(checklists);

                context.Response.Headers["Access-Control-Expose-Headers"] = "Token, Habits, Checklists";
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = (int)HttpStatusCode.OK;

                return context.Response;
            });

            app.MapPost("/signin", async (Account loginData, HttpContext context) =>
            {
                using var db = new Context();

                //Searching for account with this email and password
                var account = await db.Account
                                            .Where(p => (p.Email.Equals(loginData.Email) && p.Password.Equals(loginData.Password)))
                                            .FirstOrDefaultAsync();

                //If found - creates the tokens and context.Responses 200
                //If not - 404
                if (account == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    // TODO: Подумать, что он будет возвращать? Мб какое-нибудь сообщение
                    return JsonSerializer.Serialize(account);
                }

                //Add an achievement with "id" = 2 -> app entry.
                AccountAction.NewAction(account.Id, 2);

                //Tokens creating
                var webToken = new WebToken(account.Id);
                var tokens = new string[] { webToken.AccessToken, webToken.RefreshToken };

                context.Response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                return JsonSerializer.Serialize(account);
            });

            app.MapPost("/signup", async (Account regData, HttpContext context) =>
            {
                var email = regData.Email;
                var password = regData.Password;
                var name = regData.Name;
                var regDate = DateTime.UtcNow.ToShortDateString();

                if (!Account.IsEmailAvailable(email))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    return null;
                }

                //Creating an account with its checklists, habit performances etc.
                Account.CreateAccount(email, password, name, regDate);

                using var db = new Context();

                var account = await db.Account
                                            .Where(p => p.Email.Equals(email))
                                            .FirstAsync();

                //Add an achievement with "id" = 2 -> app entry.
                AccountAction.NewAction(account.Id, 2);

                //Tokens creating
                var webToken = new WebToken(account.Id);
                var tokens = new string[] { webToken.AccessToken, webToken.RefreshToken };

                context.Response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                return JsonSerializer.Serialize(account);
            });

            app.Run();
        }
    }
}