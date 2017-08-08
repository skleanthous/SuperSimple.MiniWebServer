namespace SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions.Helpers
{
    using System;

    public class ControllerFunctionHelper
    {
        public Func<Request, bool> ValidateCanHandleFunc { get; set; } = _ => false;
        public Func<Request, object> HandleCallFunc { get; set; }

        public object HandleCall(Request req) => HandleCallFunc(req);
        public bool CanHandleCall(Request req) => ValidateCanHandleFunc(req);
    }
}
