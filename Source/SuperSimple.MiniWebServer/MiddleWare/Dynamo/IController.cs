using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimple.MiniWebServer.MiddleWare.Dynamo
{
    internal interface IController
    {
        Task Handle(Environment environment);
    }
}
