using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimple.MiniWebServer.MiddleWare.Dynamo
{
    internal class DynamoRouter : IDynamoRouter
    {
        private readonly Dictionary<string, IController> controllers;

        public DynamoRouter()
        {
            controllers = new Dictionary<string, IController>();
        }

        public IController GetFrom(string route, string method)
        {
            IController returnVal;
            //TODO: Micro-optimization here if needed.
            string key = $"R:{route}-M:{method}";

            if (controllers.TryGetValue(key, out returnVal)) return returnVal;

            return null;
        }

        public Task Setup(Environment environment)
        {
            throw new NotImplementedException();
        }
    }
}
