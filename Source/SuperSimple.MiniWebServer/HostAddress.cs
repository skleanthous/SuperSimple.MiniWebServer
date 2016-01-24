using System.Collections.Generic;

namespace SuperSimple.MiniWebServer
{
    public class HostAddress : WrappedDictionary<string, object>
    {
        private const string SCHEME = "scheme";
        public string Scheme
        {
            get { return this[SCHEME] as string; }
            set { this[SCHEME] = value; }
        }

        private const string PORT = "port";
        public int Port
        {
            get { return int.Parse((string)this[PORT]); }
            set { this[PORT] = value.ToString(); }
        }

        private const string HOST = "host";
        public string Domain
        {
            get { return this[HOST] as string; }
            set { this[HOST] = value; }
        }
        private const string PATH = "path";
            public string Path
        {
            get { return this[PATH] as string; }
            set { this[PATH] = value; }
        }

        public HostAddress()
            : base(new Dictionary<string, object>())
        { }
    }
}