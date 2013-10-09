#region Designer generated code

#region Includes

using TechTalk.SpecFlow;

#endregion

#pragma warning disable

namespace Daishi.Armor.Specs {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("SecureArmorTokenValidatorValidatesSecureArmorToken")]
    public partial class SecureArmorTokenValidatorValidatesSecureArmorTokenFeature {
        private static TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "SecureArmorTokenValidatorValidatesSecureArmorToken.feature"
#line hidden

        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup() {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "SecureArmorTokenValidatorValidatesSecureArmorToken", "In order to prove that an SecureArmorTokenValidator validates a secure ArmorToken" +
                                                                                                                                                                     "\r\nAs a SecureArmorTokenValidator\r\nI want to validate a secure ArmorToken", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("Validate a secure ArmorToken")]
        [NUnit.Framework.CategoryAttribute("SecureArmorTokenValidator")]
        public virtual void ValidateASecureArmorToken() {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Validate a secure ArmorToken", new string[] {
                "SecureArmorTokenValidator"
            });
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I have provided a secure ArmorToken", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 9
            testRunner.When("I validate the secure ArmorToken", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 10
            testRunner.Then("The validated ArmorToken should be valid.", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion