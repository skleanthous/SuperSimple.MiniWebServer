using Microsoft.Owin.Host.HttpListener;
using Newtonsoft.Json;

namespace SuperSimple.MiniWebServer.Host.Console
{
    using Console = System.Console;
    class Program
    {
        static void Main(string[] args)
        {
            var serverStarter = Configuration.Start()
                .SetHostAddress(System.Uri.UriSchemeHttp, "localhost", 8182)
                .WithMiddleware()
                .AddDynamicController()
                .Build();

            using (var server = serverStarter())
            {
                Console.WriteLine("Host started");
                Console.ReadKey();
            }
        }
    }
}
