namespace SuperSimple.MiniWebServer
{
    using System.Collections.Generic;

    public class RequestHeaders : WrappedDictionary<string, string[]>
    {
        internal RequestHeaders(IDictionary<string, string[]> original)
            :base(original)
        { }
    }
}