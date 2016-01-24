using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimple.MiniWebServer.MiddleWare
{
    internal class DynamicControllerMiddleware : IMiddleware
    {
        private const string SET_REPLY_HEADER_NAME = "Set-Reply";

        private Dictionary<string, byte[]> Replies { get; set; }

        public DynamicControllerMiddleware()
        {
            Replies = new Dictionary<string, byte[]>();
        }

        public async Task<MiddlewareInvocationEnum> Invoke(Environment environment)
        {
            string[] setReplyHeader;
            bool shouldSetReply = false;

            if (environment.RequestHeaders.TryGetValue(SET_REPLY_HEADER_NAME, out setReplyHeader)
                && !string.IsNullOrWhiteSpace(setReplyHeader[0])
                && bool.TryParse(setReplyHeader[0], out shouldSetReply)
                && shouldSetReply)
            {
                Replies[environment.RequestPath] = await ReadAllBytes(environment.RequestBody);

                return MiddlewareInvocationEnum.StopChain;
            }
            else
            {
                byte[] data;

                if (Replies.TryGetValue(environment.RequestPath, out data))
                {
                    await environment.ResponseBody.WriteAsync(data, 0, data.Length);

                    environment.ResponseHeaders.ContentLength = data.Length;
                    environment.ResponseHeaders.ContentType = "application/json";

                    return MiddlewareInvocationEnum.StopChain;
                }
                else
                    return MiddlewareInvocationEnum.ContinueToNext;
            }
        }

        private static async Task<byte[]> ReadAllBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await ms.WriteAsync(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
