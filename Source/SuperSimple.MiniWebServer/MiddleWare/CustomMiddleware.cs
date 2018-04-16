namespace SuperSimple.MiniWebServer.MiddleWare
{
    using System;
    using System.Threading.Tasks;
    using Environment = SuperSimple.MiniWebServer.Environment;

    //TODO: Test this...
    public class CustomMiddleware : IMiddleware
    {
        private Func<Environment, Task<MiddlewareInvocationEnum>> Handler { get; set; }

        public CustomMiddleware(Func<Environment, Task<MiddlewareInvocationEnum>> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            this.Handler = handler;
        }

        public CustomMiddleware(Func<Environment, MiddlewareInvocationEnum> handler)
            : this(e => Task.FromResult(handler(e)))
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));
        }

        public async Task<MiddlewareInvocationEnum> Invoke(Environment environment)
        {
            return await Handler(environment);
        }
    }
}
