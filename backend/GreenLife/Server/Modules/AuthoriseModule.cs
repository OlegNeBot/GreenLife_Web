﻿using GreenLifeLib;
using Microsoft.EntityFrameworkCore;
using Nancy;
using Nancy.ModelBinding;
using System.Text.Json;

namespace Server
{
    public class AuthoriseModule : NancyModule
    {
        public AuthoriseModule()
        {
            #region [Requests]

            Post("/signin", async (x) =>
            {
                //Getting the data from request
                x = this.Bind<Account>();
                string email = x.Email;
                string password = x.Password;

                //Searching for account with this email and password
                Account account = null!;
                using (Context db = new())
                {
                   account = await db.Account.Where(p => (p.Email.Equals(email) && p.Password.Equals(password))).FirstOrDefaultAsync();
                }

                Response response = new();
                response.Headers["Access-Control-Allow-Origin"] = "*";
                response.Headers["Access-Control-Allow-Method"] = "POST";

                //If found - creates the tokens and responses 200
                //If not - 404
                if (account != null)
                {

                    response.StatusCode = HttpStatusCode.OK;

                    //Add an achievement with "id" = 2 -> app entry.
                    AccountAction.NewAction(account.Id, 2);

                    //Tokens creating
                    WebToken webToken = new(account.Id);
                    string[] tokens = new string[] {webToken.AccessToken, webToken.RefreshToken };
                    response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                    response.Headers["Access-Control-Expose-Headers"] = "Token";
                    response.Headers["Content-Type"] = "application/json";

                    return response;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound;

                    return response;
                }
            });

            Post("/signup", async (data) => 
            {
                //Getting the register data
                data = this.Bind<Account>();

                string email = data.Email;
                string password = data.Password;
                string name = data.Name;
                string regDate = DateTime.UtcNow.ToShortDateString();

                if (!Account.IsEmailAvailable(email))
                {
                    Response response = new();

                    response.Headers["Access-Control-Allow-Origin"] = "*";
                    response.Headers["Access-Control-Allow-Method"] = "POST";

                    response.StatusCode = HttpStatusCode.BadRequest;

                    return response;
                }

                //Creating an account with its checklists, habit performances etc.
                Account.CreateAccount(email, password, name, regDate);

                using (Context db = new())
                {
                    Account account = await db.Account.Where(p => p.Email.Equals(email)).FirstAsync();

                    //Add an achievement with "id" = 2 -> app entry.
                    AccountAction.NewAction(account.Id, 2);

                    Response response = new();
                    response.Headers["Access-Control-Allow-Origin"] = "*";
                    response.Headers["Access-Control-Allow-Method"] = "POST";

                    response.StatusCode = HttpStatusCode.OK;

                    //Tokens creating
                    WebToken webToken = new(account.Id);
                    string[] tokens = new string[] { webToken.AccessToken, webToken.RefreshToken };
                    response.Headers["Token"] = JsonSerializer.Serialize(tokens);

                    response.Headers["Access-Control-Expose-Headers"] = "Token";
                    response.Headers["Content-Type"] = "application/json";

                    return response;
                }
            });

            #endregion
        }
    }
}
