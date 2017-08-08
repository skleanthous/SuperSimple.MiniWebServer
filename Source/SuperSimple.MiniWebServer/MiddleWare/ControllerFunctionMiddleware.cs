namespace SuperSimple.MiniWebServer.MiddleWare
{
    using System;
    using System.Threading.Tasks;
    using Environment = SuperSimple.MiniWebServer.Environment;
    using Controller = System.Func<Request, Response>;

    internal class ControllerFunctionMiddleware : IMiddleware
    {
        public Func<Request, bool> CanHandleRequest { get; }
        public Controller ControllerFunction { get; }

        public ControllerFunctionMiddleware(Request acceptedRequest, Controller controller)
        {
            acceptedRequest = acceptedRequest ?? throw new ArgumentNullException(nameof(acceptedRequest));
            CanHandleRequest = req => acceptedRequest == req;

            ControllerFunction = controller ?? throw new ArgumentNullException(nameof(controller));
        }

        public ControllerFunctionMiddleware(Func<Request, bool> canHandle, Controller controller)
        {
            CanHandleRequest = canHandle ?? throw new ArgumentNullException(nameof(canHandle));
            ControllerFunction = controller ?? throw new ArgumentNullException(nameof(controller));
        }

        public async Task<MiddlewareInvocationEnum> Invoke(Environment environment)
        {
            var request = await Request.FromEnvironment(environment);

            if(!CanHandleRequest(request))
                return MiddlewareInvocationEnum.ContinueToNext;

            var reply = ControllerFunction(request);

            var context = (environment["System.Net.HttpListenerContext"] as System.Net.HttpListenerContext);
            if(context == null) throw new Exception("HttpListenerContext is not in context.");
            context.Response.SendChunked = false;
            context.Response.ContentLength64 = reply.ContentBytes.Length;
            context.Response.ContentEncoding = reply.ContentEncoding;
            context.Response.ContentType = reply.ContentType;

            await environment.ResponseBody.WriteAsync(reply.ContentBytes, 0, reply.ContentBytes.Length);
            await environment.ResponseBody.FlushAsync();

            return MiddlewareInvocationEnum.StopChain;
        }
    }
}