#region Designer generated code

#region Includes

using TechTalk.SpecFlow;

#endregion

#pragma warning disable

namespace Daishi.Armor.Specs {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("ArmorTokenIsGenerated")]
    public partial class ArmorTokenIsGeneratedFeature {
        private static TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "ArmorTokenIsGenerated.feature"
#line hidden

        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup() {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ArmorTokenIsGenerated", "In order to prove that an ArmorToken is generated\r\nAs an ArmorTokenGenerator\r\nI w" +
                                                                                                                                        "ant to generate an ArmorToken", ProgrammingLanguage.CSharp, ((string[]) (null)));
            testRunner.OnFeatureStart(featureInfo);
        }

        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown() {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }

        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize() {}

        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown() {
            testRunner.OnScenarioEnd();
        }

        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo) {
            testRunner.OnScenarioStart(scenarioInfo);
        }

        public virtual void ScenarioCleanup() {
            testRunner.CollectScenarioErrors();
        }

        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Generate an ArmorToken")]
        [NUnit.Framework.CategoryAttribute("ArmorTokenGenerator")]
        public virtual void GenerateAnArmorToken() {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Generate an ArmorToken", new string[] {
                "ArmorTokenGenerator"
            });
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I have supplied a raw ArmorToken for generation", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 9
            testRunner.When("I have generated the ArmorToken", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 10
            testRunner.Then("I should be able to successfully validate the generated ArmorToken", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion