namespace SuperSimple.MiniWebServer
{
    using System;

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

        public static IEnvironmentSetup SetBaseUrl(this IEnvironmentSetup envSetup,
            Uri baseUrl)
        {
            envSetup.Properties.HostAddresses = new[] {new HostAddress()
            {
                Scheme = baseUrl.Scheme,
                Domain = baseUrl.Host,
                Port = baseUrl.Port,
                Path = baseUrl.AbsolutePath,
            } };

            return envSetup;
        }

        //TODO: Allow for loading from config
    }
}
