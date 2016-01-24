using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimple.MiniWebServer
{
    public class Environment : WrappedDictionary<string, object>, IDictionary<string, object>
    {
        private RequestHeaders requestHeaders;
        private ResponseHeaders responseHeaders;

        public Stream RequestBody { get { return this["owin.RequestBody"] as Stream; } }
        public RequestHeaders RequestHeaders
        {
            get
            {
                if (requestHeaders == null)
                    requestHeaders = new RequestHeaders((IDictionary<string, string[]>)this["owin.RequestHeaders"]);

                return requestHeaders;
            }
        }
        public string RequestMethod { get { return (string)this["owin.RequestMethod"]; } }
        public string RequestPath { get { return (string)this["owin.RequestPath"]; } }
        public string RequestPathBase { get { return (string)this["owin.RequestPathBase"]; } }
        public string RequestQueryString { get {return (string)this["owin.RequestQueryString"];} }

        public Stream ResponseBody { get { return (Stream)this["owin.ResponseBody"]; } }
        public ResponseHeaders ResponseHeaders
        {
            get
            {
                if (responseHeaders == null)
                    responseHeaders = new ResponseHeaders((IDictionary<string, string[]>)this["owin.ResponseHeaders"]);

                return responseHeaders;
            }
        }
        public int ResponseStatusCode
        {
            get { return (int)this["owin.ResponseStatusCode"]; }
            set { this["owin.ResponseStatusCode"] = value; }
        }

        internal Environment(IDictionary<string, object> environment)
            : base(environment)
        { }

        public async Task WriteResponseWithCount(string response)
        {
            var data = Encoding.UTF8.GetBytes(response);

            await ResponseBody.WriteAsync(data, 0, data.Length);
            ResponseHeaders.ContentLength = data.Length;
        }
    }
}
