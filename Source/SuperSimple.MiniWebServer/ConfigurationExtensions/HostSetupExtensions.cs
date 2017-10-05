namespace SuperSimple.MiniWebServer
{
    public static class HostSetupExtensions
    {
        public static IEnvironmentSetup SetHostAddress(this IEnvironmentSetup envSetup, 
            string scheme, string domain, int port)
        {
            envSetup.Properties.HostAddresses = new[] {new HostAddress()
            {
                Scheme = scheme,
                Domain = domain,
                Port = port,
                Path = "",
            } };

            return envSetup;
        }


        //TODO: Allow for loading from config
    }
}
