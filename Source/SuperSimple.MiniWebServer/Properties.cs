namespace SuperSimple.MiniWebServer
{
    using System.Collections.Generic;

    public class Properties : WrappedDictionary<string, object>, IDictionary<string, object>
    {
        private const string HOST_ADDRESSES = "host.Addresses";
        public HostAddress[] HostAddresses
        {
            get { return (HostAddress[])this[HOST_ADDRESSES]; }
            set { this[HOST_ADDRESSES] = value; }
        }

        public Properties()
            :base(new Dictionary<string,object>())
        { }
    }
}
