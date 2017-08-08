namespace SuperSimple.MiniWebServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SuperSimple.MiniWebServer.MiddleWare;

    public static class FunctionalExtensionSetup
    {
        public static IMiddlewareSetup AddControllerFunction(this IMiddlewareSetup middlewareSetup, string path, Func<Request, object> controllerFunc)
        {
            var controllerFunctionMiddleware = new ControllerFunctionMiddleware(req => req.RequestPath.Equals(path, StringComparison.InvariantCultureIgnoreCase), req => new Response(controllerFunc(req)));
            middlewareSetup.AddMiddleWare(controllerFunctionMiddleware);

            return middlewareSetup;
        }

        public static IMiddlewareSetup AddControllerFunction(this IMiddlewareSetup middlewareSetup, Func<Request, bool> controllerFunctionCanHandleRequest, Func<Request, object> controllerFunc)
        {
            var controllerFunctionMiddleware = new ControllerFunctionMiddleware(controllerFunctionCanHandleRequest, req => new Response(controllerFunc(req)));
            middlewareSetup.AddMiddleWare(controllerFunctionMiddleware);

            return middlewareSetup;
        }
    }
}
