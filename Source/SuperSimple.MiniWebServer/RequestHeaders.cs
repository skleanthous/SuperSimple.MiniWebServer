using System;
using System.Collections;
using System.Collections.Generic;

namespace SuperSimple.MiniWebServer
{
    public class RequestHeaders : WrappedDictionary<string, string[]>
    {
        internal RequestHeaders(IDictionary<string, string[]> original)
            :base(original)
        { }
    }
}