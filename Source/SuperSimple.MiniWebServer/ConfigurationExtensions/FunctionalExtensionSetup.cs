namespace SuperSimple.MiniWebServer
{
    using System;
    using SuperSimple.MiniWebServer.MiddleWare.ControllerFunction;
    using SuperSimple.MiniWebServer.Response;

    public static class FunctionalExtensionSetup
    {
        public static IMiddlewareSetup AddControllerFunction(this IMiddlewareSetup middlewareSetup, string path, Func<Request, object> controllerFunc)
            => middlewareSetup.AddControllerFunction( req => req.RequestPath.Equals(path, StringComparison.InvariantCultureIgnoreCase), controllerFunc);

        public static IMiddlewareSetup AddControllerFunction(this IMiddlewareSetup middlewareSetup, Func<Request, bool> controllerFunctionCanHandleRequest, Func<Request, object> controllerFunc)
        {
            var controllerFunctionMiddleware = new ControllerFunctionMiddleware(controllerFunctionCanHandleRequest,
                req =>
                {
                    var response = controllerFunc(req);

                    return ResponseFactory.From(response);
                });
            middlewareSetup.AddMiddleWare(controllerFunctionMiddleware);

            return middlewareSetup;
        }
    }
}
