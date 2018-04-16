namespace SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions.Helpers;
    using System.Net.Http;
    using System.Text;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;


    [Binding]
    public sealed class ControllerFunctionStepDefinitions
    {
        private class Data
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        private bool hasBeenCalled = false;

        public ControllerFunctionHelper ControllerFunctionHelper { get; }
        public ControllerFunctionWithReplyHelper ControllerFunctionWithReplyHelper { get; }
        public HttpClientHelper HttpClientHelper { get; }

        public ControllerFunctionStepDefinitions(ControllerFunctionHelper helper, ControllerFunctionWithReplyHelper helperWithReply, HttpClientHelper clientHelper)
        {
            ControllerFunctionHelper = helper;
            ControllerFunctionWithReplyHelper = helperWithReply;
            HttpClientHelper = clientHelper;
        }

        [Given(@"a controller function on (.*) - (.*) that returns")]
        public void GivenAControllerFunctionOnMethodWithResourceThatReturns(string method, string resource, Table table)
        {
            var dataToReturn = new Data();
            table.FillInstance(dataToReturn);

            ControllerFunctionHelper.ValidateCanHandleFunc = req => req == new Request(method, resource);
            ControllerFunctionHelper.HandleCallFunc = _ =>
            {
                hasBeenCalled = true;
                return dataToReturn;
            };
        }

        [Given(@"a controller function on (.*) - (.*) that returns status code (.*) and")]
        public void GivenAControllerFunctionOnMethodWithResourceThatReturnsStatusCodeAnd(string method, string resource, int statusCode, Table table)
        {
            var dataToReturn = new Data();
            table.FillInstance(dataToReturn);

            ControllerFunctionWithReplyHelper.ValidateCanHandleFunc = req => req == new Request(method, resource);
            ControllerFunctionWithReplyHelper.HandleCallFunc = _ =>
            {
                hasBeenCalled = true;
                return new ControllerReply(dataToReturn, statusCode);
            };
        }

        [Given(@"a controller function on (.*) - (.*) that returns as HttpContent")]
        public void GivenAControllerFunctionOnMethodWithResourceThatReturnsAsHttpContent(string method, string resource, Table table)
        {
            var dataToReturn = new Data();
            table.FillInstance(dataToReturn);

            ControllerFunctionHelper.ValidateCanHandleFunc = req => req == new Request(method, resource);
            ControllerFunctionHelper.HandleCallFunc = _ =>
            {
                hasBeenCalled = true;

                return new StringContent(JsonConvert.SerializeObject(dataToReturn), Encoding.UTF8, "application/json");
            };
        }

        [Then(@"the reply should have a status code of (.*)")]
        public void ThenTheReplyShouldHaveAStatusCodeOf(int statusCode)
        {
            ((int) HttpClientHelper.LastCallResponse.StatusCode).Should().Be(statusCode);
        }

        [Then(@"the controller function should be called")]
        public void ThenTheControllerFunctionShouldBeCalled()
        {
            hasBeenCalled.Should().BeTrue();
        }

        [Then(@"the reply should return")]
        public void ThenTheReplyShouldReturn(Table table)
        {
            var expected = new Data();
            table.FillInstance(expected);

            var lastContent = HttpClientHelper.LastCallResponse.Content;
            lastContent.Should().NotBeNull();
            var lastStringContent = lastContent.ReadAsStringAsync().Result;
            lastStringContent.Should().NotBeNullOrEmpty();

            var actual = JsonConvert.DeserializeObject<Data>(lastStringContent);
            actual.Name.Should().Be(expected.Name);
            actual.Value.Should().Be(expected.Value);
        }
    }
}
