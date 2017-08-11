namespace SuperSimple.MiniWebServer.Response
{
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Environment = SuperSimple.MiniWebServer.Environment;

    internal class ObjectResponse : IResponse
    {
        private readonly object @return;
        private readonly int statusCode;

        public ObjectResponse(object @return, int statusCode)
        {
            this.@return = @return;
            this.statusCode = statusCode;
        }

        public async Task WriteContentTo(Environment environment)
        {
            var serialized = JsonConvert.SerializeObject(@return);
            var bytes = Encoding.UTF8.GetBytes(serialized);

            var context = (environment["System.Net.HttpListenerContext"] as System.Net.HttpListenerContext);
            context.Response.SendChunked = false;
            context.Response.ContentLength64 = bytes.LongLength;
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.StatusCode = statusCode;
            environment.ResponseStatusCode = statusCode;

            await environment.ResponseBody.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
