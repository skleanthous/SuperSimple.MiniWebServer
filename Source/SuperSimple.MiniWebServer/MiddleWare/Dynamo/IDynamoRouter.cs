using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimple.MiniWebServer.MiddleWare.Dynamo
{
    internal interface IDynamoRouter
    {
        IController GetFrom(string route, string method);

        Task Setup(Environment environment);
    }
}
