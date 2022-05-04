using GreenLifeLib;
using Microsoft.EntityFrameworkCore;
using Nancy;
using Nancy.ModelBinding;

namespace Server
{
    public class AuthoriseModule : NancyModule
    {
        public AuthoriseModule()
        {
            Post("/auth", async (x) => 
            {
                x = this.Bind<Account>();
                Account acc;
                //TODO: Add JWT
                string login = x.Login;
                string password = Account.ToHash(x.Password);
                //TODO: Пересмотреть хэширование пароля
                using (Context db = new())
                { 
                    acc = await db.Account.FirstOrDefaultAsync(l => l.Login.Equals(login) && l.Password.Equals(password));
                }

                var response = new Response();

                if (acc != null)
                {

                    response.StatusCode = HttpStatusCode.OK;
                    response.Headers["Access-Control-Allow-Origin"] = "*";
                    response.Headers["Access-Control-Allow-Method"] = "POST";

                    string myToken = "lalala.somestring.secretword";
                    response.Headers["Token"] = myToken;
                    response.Headers["Access-Control-Expose-Headers"] = "Token, Account";
                    response.Headers["Content-Type"] = "application/json";

                    response.Headers["Account"] = System.Text.Json.JsonSerializer.Serialize(acc);
                    return response;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
            });

            Post("/reg", async (data) => 
            {
                data = this.Bind<Account>();

                string login = data.Login;
                string pass = data.Password;
                string name = data.Name;
                string fname = data.FamilyName;
                string sex = data.Sex;
                DateTime birth = data.DateOfBirth;
                DateTime regDate = data.RegDate;
                //TODO: Переделать БД, убрать User.
                int uId = data.UserId;

                Account acc = new(login, pass, name, fname, sex, birth, regDate, uId);

                using (Context db = new())
                {
                    db.Add(acc);
                    await db.SaveChangesAsync();
                    //TODO: Сделать создание чек-листов, привычек и т.д. под Аккаунт
                }
                Response res = new();
                res.StatusCode = HttpStatusCode.OK;

                return res;
            });
        }
    }
}
