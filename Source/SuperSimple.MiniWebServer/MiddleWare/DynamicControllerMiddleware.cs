namespace SuperSimple.MiniWebServer.MiddleWare
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Environment = SuperSimple.MiniWebServer.Environment;
    using Reply = System.Tuple<int, string, byte[]>;

    internal class DynamicControllerMiddleware : IMiddleware
    {
        private const string SET_REPLY_HEADER_NAME = "Set-Reply";
        private const string SET_CONTENT_TYPE_HEADER_NAME = "Set-Content-Type";
        private const string SET_STATUS_CODE_HEADER_NAME = "Set-Status-Code";
        private const string CLEAR_REPLY_HEADER_NAME = "Clear-Reply";
        private const string CLEAR_ALL_VALUE = "all";
        private const string CLEAR_THIS_VALUE = "this";
        private const string DEFAULT_CONTENT_TYPE = "application/json";

        // url -> (status code, content type, content)
        private Dictionary<string, Reply> Replies { get; set; }

        public DynamicControllerMiddleware()
        {
            Replies = new Dictionary<string, Reply>();
        }

        public async Task<MiddlewareInvocationEnum> Invoke(Environment environment)
        {
            if (environment.RequestHeaders.TryGetValue(SET_REPLY_HEADER_NAME, out string[] setReplyHeader)
                && !string.IsNullOrWhiteSpace(setReplyHeader[0])
                && bool.TryParse(setReplyHeader[0], out bool shouldSetReply)
                && shouldSetReply)
            {
                var content = await environment.RequestBody.ReadAllBytes();

                Replies[environment.RequestPath] = new Reply(
                    GetSetStatusCodeOrDefault(environment.RequestHeaders),
                    GetSetContentTypeOrDefault(environment.RequestHeaders),
                    content);

                return MiddlewareInvocationEnum.StopChain;
            }

            if (environment.RequestHeaders.TryGetValue(CLEAR_REPLY_HEADER_NAME, out setReplyHeader)
                && !string.IsNullOrWhiteSpace(setReplyHeader[0]))
            {
                if (setReplyHeader[0].Equals(CLEAR_ALL_VALUE, StringComparison.CurrentCultureIgnoreCase))
                {
                    Replies.Clear();
                }
                else if (setReplyHeader[0].Equals(CLEAR_THIS_VALUE, StringComparison.CurrentCultureIgnoreCase))
                {
                    Replies.Remove(environment.RequestPath);
                }

                return MiddlewareInvocationEnum.StopChain;
            }
            else
            {
                try
                {
                    if (Replies.TryGetValue(environment.RequestPath, out Reply data))
                    {
                        var statusCode = data.Item1;
                        var contentType = data.Item2;
                        var content = data.Item3;

                        var context = (environment["System.Net.HttpListenerContext"] as System.Net.HttpListenerContext);
                        context.Response.SendChunked = false;
                        context.Response.ContentLength64 = content.Length;
                        context.Response.ContentEncoding = Encoding.UTF8;
                        context.Response.ContentType = contentType;

                        environment.ResponseStatusCode = (int)statusCode;
                        await environment.ResponseBody.WriteAsync(content, 0, content.Length);
                        await environment.ResponseBody.FlushAsync();

                        return MiddlewareInvocationEnum.StopChain;
                    }
                    else
                    {
                        return MiddlewareInvocationEnum.ContinueToNext;
                    }
                }
                catch (Exception)
                {
                    environment.ResponseStatusCode = (int)HttpStatusCode.InternalServerError;
                    return MiddlewareInvocationEnum.StopChain;
                }
            }
        }

        private static string GetSetContentTypeOrDefault(RequestHeaders headers)
        {
            if (headers.TryGetValue(SET_CONTENT_TYPE_HEADER_NAME, out string[] setContentTypeHeaders)
                && !string.IsNullOrWhiteSpace(setContentTypeHeaders[0]))
            {
                return setContentTypeHeaders[0];
            }
            else
            {
                return DEFAULT_CONTENT_TYPE;
            }
        }

        private static int GetSetStatusCodeOrDefault(RequestHeaders headers)
        {
            if (headers.TryGetValue(SET_STATUS_CODE_HEADER_NAME, out string[] setStatusHeaders)
                && !string.IsNullOrWhiteSpace(setStatusHeaders[0]))
            {
                if (int.TryParse(setStatusHeaders[0], out int status))
                {
                    return status;
                }
            }

            return (int)HttpStatusCode.OK;
        }
    }
}
