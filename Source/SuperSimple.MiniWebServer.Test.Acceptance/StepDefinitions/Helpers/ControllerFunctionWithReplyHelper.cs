namespace SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions.Helpers
{
    using System;

    public class ControllerFunctionWithReplyHelper
    {
        public Func<Request, bool> ValidateCanHandleFunc { get; set; } = _ => false;
        public Func<Request, ControllerReply> HandleCallFunc { get; set; }

        public ControllerReply HandleCall(Request req) => HandleCallFunc(req);
        public bool CanHandleCall(Request req) => ValidateCanHandleFunc(req);
    }
}
