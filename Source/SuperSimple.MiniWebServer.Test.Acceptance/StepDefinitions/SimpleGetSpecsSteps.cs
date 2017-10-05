namespace SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions
{
    using FluentAssertions;
    using SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions.Helpers;
    using System;
    using System.Linq;
    using System.Net;
    using TechTalk.SpecFlow;

    [Binding]
    public class SimpleGetSpecsSteps
    {
        private string payload;

        private HttpClientHelper Helper { get; }

        public SimpleGetSpecsSteps(HttpClientHelper helper)
        {
            payload = Guid.NewGuid().ToString();
            Helper = helper;
        }

        [Given(@"I post to resource (.*) with header (.*):(.*)")]
        [When(@"I post to resource (.*) with header (.*):(.*)")]
        public void GivenIPostToResourceWithHeader(string resource, string header, string headerValue)
        {
            Helper.Post(resource, header, headerValue, payload);
        }

        [Given(@"I set resource (.*) with header (.*):(.*)")]
        public void GivenISetResourceWithHeader(string resource, string header, string headerValue)
        {
            Helper.PostAndSetReply(resource, header, headerValue, payload);
        }

        [When(@"I attempt a (.*) on resource (.*)")]
        public void WhenIAttemptAnActionOnResourceMyResourceResourceId(string method, string resource)
        {
            Helper.Send(method, resource);
        }

        [Then(@"I should get back exactly what I set up")]
        public void ThenIShouldGetBackExactlyWhatISetUp()
        {
            Helper.LastCallResponse.IsSuccessStatusCode.Should().BeTrue();
            Helper.LastCallResponse.Content.ReadAsStringAsync().Result.Trim().Should().Be(payload);
        }

        [Then(@"a get on (.*) should return status 404")]
        public void ThenAGetOnMyResourceResourceIdShouldReturnStatus(string resource)
        {
            var response = Helper.GetAsMessage(resource);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Then(@"a get on (.*) should get back exactly what I set up")]
        public void ThenAGetOnMyResourceResourceIdShouldGetBackExactlyWhatISetUp(string resource)
        {
            var response = Helper.GetAsMessage(resource);

            response.IsSuccessStatusCode.Should().BeTrue();
            response.Content.ReadAsStringAsync().Result.Trim().Should().Be(payload);
        }

        [Then(@"the the reply should have a content type of (.*)")]
        public void ThenTheTheReplyShouldHaveAContentTypeOf(string expectedContentType)
        {
            var contentHeaders = Helper.LastCallResponse.Content.Headers.GetValues("Content-Type");
            contentHeaders.Should().NotBeNullOrEmpty();
            contentHeaders.First().Should().Be(expectedContentType);
        }
    }
}
