namespace SuperSimple.MiniWebServer.Response
{
    using System.Threading.Tasks;
    using Environment = SuperSimple.MiniWebServer.Environment;

    internal class NoContentResponse : IResponse
    {
        private readonly Task done = Task.FromResult(0);
        private readonly int statusCode;

        public NoContentResponse(int statusCode)
        {
            this.statusCode = statusCode;
        }

        public Task WriteContentTo(Environment environment)
        {
            var context = (environment["System.Net.HttpListenerContext"] as System.Net.HttpListenerContext);
            context.Response.SendChunked = false;
            context.Response.ContentLength64 = 0;
            context.Response.StatusCode = statusCode;
            environment.ResponseStatusCode = statusCode;

            return done;
        }
    }
}
