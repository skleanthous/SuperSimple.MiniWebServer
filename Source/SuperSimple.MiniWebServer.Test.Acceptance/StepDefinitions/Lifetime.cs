using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;


namespace SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions
{
    [Binding]
    public class Lifetime
    {
        private IDisposable ServerHandle
        {
            get { return ScenarioContext.Current[nameof(ServerHandle)] as IDisposable; }
            set { ScenarioContext.Current[nameof(ServerHandle)] = value; }
        }

        [BeforeScenario]
        public void ScenarioSetup()
        {
            var serverStarter = Configuration.Start()
                .SetHostAddress(System.Uri.UriSchemeHttp, "localhost", 8182)
                .WithMiddleware()
                .AddDynamicController()
                .Build();

            ServerHandle = serverStarter();
        }

        [AfterScenario]
        public void ScenarioTeardown()
        {
            ServerHandle.Dispose();
            ServerHandle = null;
        }
    }
}
