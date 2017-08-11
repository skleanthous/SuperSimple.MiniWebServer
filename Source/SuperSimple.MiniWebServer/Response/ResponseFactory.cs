namespace SuperSimple.MiniWebServer.Response
{
    using System.Net.Http;

    internal static class ResponseFactory
    {
        public static IResponse From(object @return)
        {
            const int defaultStatus = 200;

            switch (@return)
            {
                case ControllerReply reply:
                    return From(reply.Content, reply.StatucCode);
                default:
                    return From(@return, defaultStatus);
            }
        }

        private static IResponse From(object reply, int statusCode)
        {
            switch (reply)
            {
                case null:
                    return new NoContentResponse(statusCode);
                case HttpContent content:
                    return new HttpContentRsponse(content, statusCode);
                default:
                    return new ObjectResponse(reply, statusCode);
            }
        }
    }
}
