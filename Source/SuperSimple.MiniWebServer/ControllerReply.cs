namespace SuperSimple.MiniWebServer
{
    public class ControllerReply
    {
        public int StatucCode { get; }
        public object Content { get; }

        public ControllerReply(object content)
            :this(content, 200)
        { }

        public ControllerReply(object content, int statusCode)
        {
            Content = content;
            StatucCode = statusCode;
        }
    }
}
