namespace SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions
{
    using SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions.Helpers;
    using System;
    using TechTalk.SpecFlow;

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
            var fc = ControllerFunctionHelper;
            Func<Request, bool> canHandleObject = (Request req) => fc.CanHandleCall(req) && fc.HandleCallFunc != null;
            Func<Request, bool> canHandleHttpContent = (Request req) => fc.CanHandleCall(req) && fc.HandleHttpContentCallFunc != null;
            Func<Request, bool> canHandleHttpContentAndStatus = (Request req) => fc.CanHandleCall(req) && fc.HandleHttpContentAndStatusCallFunc != null;

            var serverStarter = Configuration.Start()
                .SetHostAddress(System.Uri.UriSchemeHttp, "localhost", 8182)
                .WithMiddleware()
                .AddDynamicController()
                .AddControllerFunction(canHandleObject, req => fc.HandleCallFunc(req))
                .AddControllerFunctionThatReturnsHttpContent(canHandleHttpContent, req => fc.HandleHttpContentCallFunc(req))
                .AddControllerFunctionThatReturnsHttpContent(canHandleHttpContentAndStatus, req => fc.HandleHttpContentAndStatusCallFunc(req))
                .AddControllerFunction(ControllerFunctionWithReplyHelper.CanHandleCall, ControllerFunctionWithReplyHelper.HandleCall)
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
