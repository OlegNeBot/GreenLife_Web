using GreenLifeLib;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;

namespace AspServer
{
    /// <summary>
    /// Creates, checks JWTToken
    /// </summary>
    internal class WebToken
    {
        #region [Fields]

        //Secret for token
        string secret = "I_11&A2mm*Mm9_AnN11aPP_1e3";

        #endregion

        #region [Constructors]

        /// <summary>
        /// Creates tokens for account with account id.
        /// </summary>
        /// <param name="id">Account id.</param>
        public WebToken(int id)
        {
            var tokens = CreateToken(id);
            AccessToken = tokens[0];
            RefreshToken = tokens[1];
        }

        /// <summary>
        /// Checks the tokens, updates them if needed and sets an account id.
        /// </summary>
        /// <param name="accessToken">Access token</param>
        /// <param name="refreshToken">Refresh token.</param>
        public WebToken(string accessToken, string refreshToken)
        {
            int id = CheckToken(ref accessToken, ref refreshToken);
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            AccountId = id;
        }

        #endregion

        #region [Props]

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int AccountId { get; set; }

        #endregion

        #region [Methods]

        /// <summary>
        /// Creates two tokens: access and refresh with account id then pushes refresh token into Database.
        /// </summary>
        /// <param name="id">Account id.</param>
        /// <returns>String array with two values: access token and refresh token.</returns>
        private string[] CreateToken(int id)
        {
            //Creating access and refresh tokens
            var accessToken = JwtBuilder.Create()
                                  .WithAlgorithm(new HMACSHA256Algorithm())
                                  .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                                  .AddClaim("id", id)
                                  .WithSecret(secret)
                                  .Encode();
            var refreshToken = JwtBuilder.Create()
                                  .WithAlgorithm(new HMACSHA256Algorithm())
                                  .AddClaim("exp", DateTimeOffset.UtcNow.AddDays(3).ToUnixTimeSeconds())
                                  .AddClaim("id", id)
                                  .WithSecret(secret)
                                  .Encode();

            //Searching for token in database; if found -> updates it, else adds a new token.
            using var db = new Context();

            var token = db.Token
                        .Where(p => p.AccountId == id)
                        .FirstOrDefault();

            if (token == null)
            {
                Token newToken = new(id, refreshToken);

                db.Token.Add(newToken);
            }
            else
            {
                token.UserToken = refreshToken;

                db.Token.Update(token);
            }

            db.SaveChanges();

            //Creating an array with tokens.
            return new string[] { accessToken, refreshToken };
        }

        /// <summary>
        /// Checking for token validity and expiration date. If refresh token is "rotten" then 
        /// updates it. Tokens are with "ref" modifier. Returns an account id value.
        /// </summary>
        /// <param name="accessToken">Access token needed to be checked.</param>
        /// <param name="refreshToken">Refresh token to check validity.</param>
        /// <returns>An account id.</returns>
        private int CheckToken(ref string accessToken, ref string refreshToken)
        {
            //Trying to decode access code
            //If expired - decoding refresh token
            try
            {
                var json = JwtBuilder.Create()
                                     .WithAlgorithm(new HMACSHA256Algorithm())
                                     .WithSecret(secret)
                                     .MustVerifySignature()
                                     .Decode<IDictionary<string, object>>(accessToken);
                object id;
                json.TryGetValue("id", out id!);

                return int.Parse(id.ToString()!);
            }
            catch (TokenNotYetValidException)
            {
                return 0;
            }
            catch (TokenExpiredException)
            {
                //Trying to decode refresh token
                //If expired - creates a new pair of tokens
                try
                {
                    var json = JwtBuilder.Create()
                                         .WithAlgorithm(new HMACSHA256Algorithm())
                                         .WithSecret(secret)
                                         .MustVerifySignature()
                                         .Decode<IDictionary<string, object>>(refreshToken);

                    var rToken = refreshToken;

                    using var db = new Context();

                    var token = db.Token
                                    .Where(p => p.UserToken.Equals(rToken))
                                    .FirstOrDefault();

                    //If token valid - creates a new tokens.
                    if (token != null)
                    {
                        var id = token.AccountId;

                        var tokens = CreateToken(id);

                        accessToken = tokens[0];
                        refreshToken = tokens[1];

                        return id;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (TokenNotYetValidException)
                {
                    return 0;
                }
                catch (TokenExpiredException)
                {
                    //Checking for token validity
                    //If valid - creates a new pair of tokens and returns an account id
                    var rToken = refreshToken;

                    using var db = new Context();

                    var token = db.Token
                                    .Where(p => p.UserToken.Equals(rToken))
                                    .FirstOrDefault();

                    if (token != null)
                    {
                        var id = token.AccountId;

                        var tokens = CreateToken(id);

                        accessToken = tokens[0];
                        refreshToken = tokens[1];

                        return id;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (SignatureVerificationException)
                {
                    return 0;
                }
            }
            catch (SignatureVerificationException)
            {
                return 0;
            }
        }

        #endregion
    }
}
