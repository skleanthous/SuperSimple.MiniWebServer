using SuperSimple.MiniWebServer.MiddleWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimple.MiniWebServer
{
    public static class DynamicMiddlewareSetup
    {
        public static IMiddlewareSetup AddDynamicController(this IMiddlewareSetup middlewareSetup)
        {
            middlewareSetup.AddMiddleWare(new DynamicControllerMiddleware());

            return middlewareSetup;
        }

        public static IMiddlewareSetup AddCustomController(this IMiddlewareSetup middlewareSetup,
            string route, Func<Environment, Task> handler)
        {
            middlewareSetup.AddCustomController(async e =>
            {
                if (e.RequestPath.Equals(route, StringComparison.OrdinalIgnoreCase))
                {
                    await handler(e);
                    return MiddlewareInvocationEnum.StopChain;
                }

                return MiddlewareInvocationEnum.ContinueToNext;
            });

            return middlewareSetup;
        }

        public static IMiddlewareSetup AddCustomController(this IMiddlewareSetup middlewareSetup, 
            Func<Environment, Task<MiddlewareInvocationEnum>> handler)
        {
            middlewareSetup.AddMiddleWare(new CustomMiddleware(handler));

            return middlewareSetup;
        }
    }
}
