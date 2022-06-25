using Nancy;

namespace Server
{
    #region [Classes]

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        #region [Methods]

        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            pipelines.EnableCORS();
        }

        #endregion
    }

    public static class NancyExtension
    {
        #region [Methods]

        public static void EnableCORS(this Nancy.Bootstrapper.IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                if (ctx.Request.Headers.Keys.Contains("Origin"))
                {
                    var origins = "" + string.Join(" ", ctx.Request.Headers["Origin"]);
                    ctx.Response.Headers["Access-Control-Allow-Origin"] = origins;

                    if (ctx.Request.Method == "OPTIONS")
                    {
                        // handle CORS preflight request

                        ctx.Response.Headers["Access-Control-Allow-Methods"] =
                            "GET, POST, PUT, DELETE, OPTIONS";

                        if (ctx.Request.Headers.Keys.Contains("Access-Control-Request-Headers"))
                        {
                            var allowedHeaders = "" + string.Join(
                                ", ", ctx.Request.Headers["Access-Control-Request-Headers"]);
                            ctx.Response.Headers["Access-Control-Allow-Headers"] = allowedHeaders;
                        }
                    }
                }
            });
        }

        #endregion
    }

    #endregion
}
