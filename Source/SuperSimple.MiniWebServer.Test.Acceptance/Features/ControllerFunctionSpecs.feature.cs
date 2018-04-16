﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.0.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SuperSimple.MiniWebServer.Test.Acceptance.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ControllerFunctionSpecsFeature : Xunit.IClassFixture<ControllerFunctionSpecsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "ControllerFunctionSpecs.feature"
#line hidden
        
        public ControllerFunctionSpecsFeature(ControllerFunctionSpecsFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ControllerFunctionSpecs", "\tIn order to be able to adapt the responses\r\n\tAs a tester\r\n\tI want to respond to " +
                    "requests dynamically by inspecting the request", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Controller function gets hit if path and method matches")]
        [Xunit.TraitAttribute("FeatureTitle", "ControllerFunctionSpecs")]
        [Xunit.TraitAttribute("Description", "Controller function gets hit if path and method matches")]
        [Xunit.TraitAttribute("Category", "Acceptance.ControllerFunction")]
        public virtual void ControllerFunctionGetsHitIfPathAndMethodMatches()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Controller function gets hit if path and method matches", new string[] {
                        "Acceptance.ControllerFunction"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Value"});
            table1.AddRow(new string[] {
                        "MyName",
                        "5"});
#line 8
 testRunner.Given("a controller function on GET - /MyResource/ResourceId that returns", ((string)(null)), table1, "Given ");
#line 11
 testRunner.When("I attempt a get on resource /MyResource/ResourceId", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 12
 testRunner.Then("the controller function should be called", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Controller function returns susccessfully")]
        [Xunit.TraitAttribute("FeatureTitle", "ControllerFunctionSpecs")]
        [Xunit.TraitAttribute("Description", "Controller function returns susccessfully")]
        [Xunit.TraitAttribute("Category", "Acceptance.ControllerFunction")]
        public virtual void ControllerFunctionReturnsSusccessfully()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Controller function returns susccessfully", new string[] {
                        "Acceptance.ControllerFunction"});
#line 15
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Value"});
            table2.AddRow(new string[] {
                        "MyName",
                        "5"});
#line 16
 testRunner.Given("a controller function on GET - /MyResource/ResourceId that returns", ((string)(null)), table2, "Given ");
#line 19
 testRunner.When("I attempt a get on resource /MyResource/ResourceId", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Value"});
            table3.AddRow(new string[] {
                        "MyName",
                        "5"});
#line 20
 testRunner.Then("the reply should return", ((string)(null)), table3, "Then ");
#line 23
 testRunner.Then("the reply should have a status code of 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Controller function returns custom status code with content")]
        [Xunit.TraitAttribute("FeatureTitle", "ControllerFunctionSpecs")]
        [Xunit.TraitAttribute("Description", "Controller function returns custom status code with content")]
        [Xunit.TraitAttribute("Category", "Acceptance.ControllerFunction")]
        public virtual void ControllerFunctionReturnsCustomStatusCodeWithContent()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Controller function returns custom status code with content", new string[] {
                        "Acceptance.ControllerFunction"});
#line 26
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Value"});
            table4.AddRow(new string[] {
                        "MyName",
                        "5"});
#line 27
 testRunner.Given("a controller function on GET - /MyResource/ResourceId that returns status code 20" +
                    "1 and", ((string)(null)), table4, "Given ");
#line 30
 testRunner.When("I attempt a get on resource /MyResource/ResourceId", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 31
 testRunner.Then("the reply should have a status code of 201", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Value"});
            table5.AddRow(new string[] {
                        "MyName",
                        "5"});
#line 32
 testRunner.And("the reply should return", ((string)(null)), table5, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Controller function returns HttpContent")]
        [Xunit.TraitAttribute("FeatureTitle", "ControllerFunctionSpecs")]
        [Xunit.TraitAttribute("Description", "Controller function returns HttpContent")]
        [Xunit.TraitAttribute("Category", "Acceptance.ControllerFunction")]
        public virtual void ControllerFunctionReturnsHttpContent()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Controller function returns HttpContent", new string[] {
                        "Acceptance.ControllerFunction"});
#line 37
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Value"});
            table6.AddRow(new string[] {
                        "MyName",
                        "5"});
#line 38
 testRunner.Given("a controller function on GET - /MyResource/ResourceId that returns as HttpContent" +
                    "", ((string)(null)), table6, "Given ");
#line 41
 testRunner.When("I attempt a get on resource /MyResource/ResourceId", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Value"});
            table7.AddRow(new string[] {
                        "MyName",
                        "5"});
#line 42
 testRunner.Then("the reply should return", ((string)(null)), table7, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                ControllerFunctionSpecsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ControllerFunctionSpecsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
