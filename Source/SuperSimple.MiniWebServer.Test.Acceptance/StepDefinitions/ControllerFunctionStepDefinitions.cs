using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using SuperSimple.MiniWebServer.Test.Acceptance.StepDefinitions.Helpers;
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
        public HttpClientHelper HttpClientHelper { get; }

        public ControllerFunctionStepDefinitions(ControllerFunctionHelper helper, HttpClientHelper clientHelper)
        {
            ControllerFunctionHelper = helper;
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
