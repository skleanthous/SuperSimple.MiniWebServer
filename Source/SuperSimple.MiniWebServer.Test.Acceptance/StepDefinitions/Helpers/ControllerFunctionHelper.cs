namespace SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions.Helpers
{
    using System;
    using System.Net.Http;
    using System.Threading;

    public class ControllerFunctionHelper
    {
        public Func<Request, bool> ValidateCanHandleFunc { get; set; } = _ => false;
        public Func<Request, object> HandleCallFunc { get; set; }
        public Func<Request, HttpContent> HandleHttpContentCallFunc { get; set; }
        public Func<Request, (HttpContent content,int status)> HandleHttpContentAndStatusCallFunc { get; set; }

        public bool CanHandleCall(Request req) => ValidateCanHandleFunc(req);

        public void ValidateCountOfNonNullHandlers(Request req)
        {
            if (!CanHandleCall(req)) return;

            int count = 0;
            if (HandleCallFunc != null) count++;
            if (HandleHttpContentCallFunc != null) count++;
            if (HandleHttpContentAndStatusCallFunc != null) count++;

            if(count != 1) throw new Exception($"If can handle request, exactly one handler must be set. Currently set handlers {count}");
        }
    }
}
