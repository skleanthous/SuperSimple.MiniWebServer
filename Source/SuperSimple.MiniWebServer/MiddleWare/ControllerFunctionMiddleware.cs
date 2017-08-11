namespace SuperSimple.MiniWebServer.MiddleWare.ControllerFunction
{
    using System;
    using System.Threading.Tasks;
    using Environment = SuperSimple.MiniWebServer.Environment;
    using Controller = System.Func<Request, Response.IResponse>;

    internal class ControllerFunctionMiddleware : IMiddleware
    {
        public Func<Request, bool> CanHandleRequest { get; }
        public Controller ControllerFunction { get; }

        public ControllerFunctionMiddleware(Request acceptedRequest, Controller controller)
            : this(req => req == acceptedRequest, controller)
        {
            if(acceptedRequest == null) throw new ArgumentNullException(nameof(acceptedRequest));
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

            await ControllerFunction(request).WriteContentTo(environment);

            return MiddlewareInvocationEnum.StopChain;
        }
    }
}