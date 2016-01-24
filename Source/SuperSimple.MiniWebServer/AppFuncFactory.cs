using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace SuperSimple.MiniWebServer
{
    public class AppFuncFactory
    {
        public Environment Properties { get; set; }
        public Func<object, string> SerializerMethod { get; set; }

        public AppFuncFactory(Func<object, string> serializerMethod)
        {
            SerializerMethod = serializerMethod;
            Properties = new Environment();
            SetHostAddresses();
        }

        private void SetHostAddresses()
        {
            Properties["host.Addresses"] = new[]
            {
                new Dictionary<string, object>
                {
                    ["scheme"] = Uri.UriSchemeHttp,
                    ["host"] = "localhost",
                    ["port"] = "8181",
                    ["path"] = "",
                }
            };
        }

        public async Task AppFunc(IDictionary<string, object> application)
        {
            var dataObject = new KeyValuePair<int, string>(5, "ValueName");
            var dataString = await Task.Factory.FromAsync((callBack, state) => SerializerMethod.BeginInvoke(state, callBack, null), 
                SerializerMethod.EndInvoke, dataObject);
            var data = Encoding.UTF8.GetBytes(dataString);

            var stream = (Stream)application["owin.ResponseBody"];

            var headers = application["owin.ResponseHeaders"] as IDictionary<string, string[]>;
            headers["Content-Type"] = new[] { "application/json" };
            //For downloading non-json and non-html content types
            //headers["Content-Disposition"] = new[] { "attachement; filename=\"testing.json\""};
            headers["Content-Length"] = new[] { data.Length.ToString() };

            await stream.WriteAsync(data, 0, data.Length);
        }
    }
}
