#region Designer generated code

#region Includes

using TechTalk.SpecFlow;

#endregion

#pragma warning disable

namespace Daishi.Armor.Specs {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("ArmorTokenConstructorConstructsStandardSecureArmorToken")]
    public partial class ArmorTokenConstructorConstructsStandardSecureArmorTokenFeature {
        private static TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "ArmorTokenConstructorConstructsStandardSecureArmorToken.feature"
#line hidden

        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup() {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ArmorTokenConstructorConstructsStandardSecureArmorToken", "In order to prove that an ArmorTokenConstructor constructs a standard, secure Arm" +
                                                                                                                                                                          "orToken\r\nAs an ArmorTokenConstructor\r\nI want to construct a standard, secure Arm" +
                                                                                                                                                                          "orToken", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("Construct a standard, secure ArmorToken")]
        [NUnit.Framework.CategoryAttribute("ArmorTokenConstructor")]
        public virtual void ConstructAStandardSecureArmorToken() {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Construct a standard, secure ArmorToken", new string[] {
                "ArmorTokenConstructor"
            });
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I have supplied a StandardArmorTokenConstructor", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 9
            testRunner.When("I invoke the StandardArmorTokenConstructor", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 10
            testRunner.Then("The result should yield a valid, standard, secure ArmorToken", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion