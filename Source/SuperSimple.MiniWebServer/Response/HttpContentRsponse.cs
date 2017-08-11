namespace SuperSimple.MiniWebServer.Response
{
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Environment = SuperSimple.MiniWebServer.Environment;

    internal class HttpContentRsponse : IResponse
    {
        private readonly HttpContent content;
        private readonly int statusCode;

        public HttpContentRsponse(HttpContent content, int statusCode)
        {
            this.content = content;
            this.statusCode = statusCode;
        }

        public Task WriteContentTo(Environment environment)
        {
            var context = (environment["System.Net.HttpListenerContext"] as System.Net.HttpListenerContext);
            context.Response.SendChunked = false;
            context.Response.ContentLength64 = content.Headers.ContentLength ?? 0;
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.StatusCode = statusCode;
            environment.ResponseStatusCode = statusCode;

            return content.CopyToAsync(environment.ResponseBody);
        }
    }
}
