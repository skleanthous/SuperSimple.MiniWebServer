using System;
using TechTalk.SpecFlow;


namespace SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions
{
    using SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions.Helpers;

    [Binding]
    public class Lifetime
    {
        private IDisposable ServerHandle
        {
            get { return ScenarioContext.Current[nameof(ServerHandle)] as IDisposable; }
            set { ScenarioContext.Current[nameof(ServerHandle)] = value; }
        }

        private ControllerFunctionHelper ControllerFunctionHelper { get; set; }
        private ControllerFunctionWithReplyHelper ControllerFunctionWithReplyHelper { get; set; }

        public Lifetime(ControllerFunctionHelper helper, ControllerFunctionWithReplyHelper withReplyHelper)
        {
            ControllerFunctionHelper = helper;
            ControllerFunctionWithReplyHelper = withReplyHelper;
        }

        [BeforeScenario]
        public void ScenarioSetup()
        {
            var serverStarter = Configuration.Start()
                .SetHostAddress(System.Uri.UriSchemeHttp, "localhost", 8182)
                .WithMiddleware()
                .AddDynamicController()
                .AddControllerFunction(ControllerFunctionHelper.CanHandleCall, ControllerFunctionHelper.HandleCall)
                .AddControllerFunction(ControllerFunctionWithReplyHelper.CanHandleCall, ControllerFunctionWithReplyHelper.HandleCall)
                .Build();

            ServerHandle = serverStarter();
        }

        [AfterScenario]
        public void ScenarioTeardown()
        {
            ServerHandle?.Dispose();
            ServerHandle = null;
        }
    }
}
