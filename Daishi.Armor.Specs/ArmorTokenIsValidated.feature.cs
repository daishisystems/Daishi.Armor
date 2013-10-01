#region Designer generated code

#region Includes

using TechTalk.SpecFlow;

#endregion

#pragma warning disable

namespace Daishi.Armor.Specs {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("ArmorTokenIsValidated")]
    public partial class ArmorTokenIsValidatedFeature {
        private static TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "ArmorTokenIsValidated.feature"
#line hidden

        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup() {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ArmorTokenIsValidated", "In order to prove that an ArmorToken is validated\r\nAs an ArmorTokenValidator\r\nI w" +
                                                                                                                                        "ant to validate an ArmorToken", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("Validate an ArmorToken")]
        [NUnit.Framework.CategoryAttribute("armorTokenValidator")]
        public virtual void ValidateAnArmorToken() {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate an ArmorToken", new string[] {
                "armorTokenValidator"
            });
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I have supplied a valid ArmorToken", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 9
            testRunner.And("I have encrypted the valid ArmorToken", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 10
            testRunner.And("I have hashed the valid ArmorToken", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 11
            testRunner.When("I validate the valid ArmorToken", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 12
            testRunner.Then("I should return a valid result", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion