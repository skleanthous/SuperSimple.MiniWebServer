using Microsoft.Owin.Host.HttpListener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimple.MiniWebServer
{
    public interface IEnvironmentSetup
    {
        Properties Properties { get; }

        IMiddlewareSetup WithMiddleware();
    }

    public interface IMiddlewareSetup
    {
        IMiddlewareSetup AddMiddleWare(IMiddleware middleware);
        Func<IDisposable> Build();
    }

    public interface IBuildableServer
    {
        Func<IDisposable> Build();
    }

    public class Configuration : IMiddlewareSetup, IEnvironmentSetup, IBuildableServer
    {
        public Properties Properties { get; private set; }
        internal List<IMiddleware> Middleware { get; private set; }

        private Configuration()
        {
            Middleware = new List<IMiddleware>();
            Properties = new Properties();
        }

        public static IEnvironmentSetup Start()
        {
            return new Configuration();
        }

        public IMiddlewareSetup WithMiddleware()
        {
            return this;
        }

        public IMiddlewareSetup AddMiddleWare(IMiddleware middleware)
        {
            Middleware.Add(middleware);
            return this;
        }

        public Func<IDisposable> Build()
        {
            return () => OwinServerFactory.Create(c => RunMiddleware(Middleware.ToArray(), c), Properties);
        }

        private static async Task RunMiddleware(IMiddleware[] middleware, IDictionary<string, object> context)
        {
            var environment = new Environment(context);

            for(int i =0;i< middleware.Length;i++)
            {
                var result = await middleware[i].Invoke(environment);

                if (result == MiddlewareInvocationEnum.StopChain)
                    return;
            }
        }
    }
}
