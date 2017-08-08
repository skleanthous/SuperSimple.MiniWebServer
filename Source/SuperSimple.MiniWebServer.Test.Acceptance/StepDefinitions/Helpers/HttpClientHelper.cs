namespace SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions.Helpers
{
    using System;
    using System.Net.Http;
    using TechTalk.SpecFlow;

    public class HttpClientHelper : IDisposable
    {
        private readonly HttpClient client = new HttpClient();

        private readonly ScenarioContext context;
        public HttpResponseMessage LastCallResponse
        {
            get => context[nameof(HttpClientHelper)] as HttpResponseMessage;
            private set => context[nameof(HttpClientHelper)] = value;
        }

        public HttpClientHelper()
        {
            client.BaseAddress = new Uri("http://localhost:8182");
            this.context = ScenarioContext.Current;
        }

        public void Post(string resource, string header, string headerValue, string payload)
        {
            var content = new StringContent(payload);
            content.Headers.Add(header, headerValue);

            DoCall(content, resource, "Post");
        }
        public void PostAndSetReply(string resource, string header, string headerValue, string payload)
        {
            var content = new StringContent(payload);
            content.Headers.Add(header, headerValue);

            DoCall(content, resource, "Post", true);
        }

        public HttpResponseMessage GetAsMessage(string resource) => Send("GET", resource);

        public HttpResponseMessage Send(string method, string resource, HttpContent content = null)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), resource);
            request.Content = content;

            LastCallResponse = client.SendAsync(request).Result;
            return LastCallResponse;
        }

        private void DoCall(HttpContent content, string resource, string method, bool setReply = false)
        {
            if(setReply) content.Headers.Add("Set-Reply", "true");

            var reply = Send(method, resource, content);

            reply.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
