using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimple.MiniWebServer.MiddleWare.Dynamo
{
    public class Dynamo : IMiddleware
    {
        private const string SET_REPLY_HEADER_NAME = "Set-Reply";

        private IDynamoRouter Router { get; set; }

        public async Task<MiddlewareInvocationEnum> Invoke(Environment environment)
        {
            if (IsSetupCall(environment))
            {
                await Router.Setup(environment);
            }
            else
            {
                //Find route handler
                var method = environment.RequestMethod;
                var resource = environment.RequestPath;

                var controller = Router.GetFrom(resource, method);

                //If route handler found
                //Handle request
                if (controller != null) await controller.Handle(environment);
            }
            return MiddlewareInvocationEnum.StopChain;
        }

        private bool IsSetupCall(Environment environment)
        {
            string[] isReplyHeader;
            bool isSetup;
            return environment.RequestHeaders.TryGetValue(SET_REPLY_HEADER_NAME, out isReplyHeader)
                &&  bool.TryParse(isReplyHeader.FirstOrDefault(), out isSetup) && isSetup;
        }
    }
}
