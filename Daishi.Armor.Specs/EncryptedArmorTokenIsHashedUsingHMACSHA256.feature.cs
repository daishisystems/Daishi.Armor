#region Designer generated code

#region Includes

using TechTalk.SpecFlow;

#endregion

#pragma warning disable

namespace Daishi.Armor.Specs {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("EncryptedArmorTokenIsHashedUsingHMACSHA256")]
    public partial class EncryptedArmorTokenIsHashedUsingHMACSHA256Feature {
        private static TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "EncryptedArmorTokenIsHashedUsingHMACSHA256.feature"
#line hidden

        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup() {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "EncryptedArmorTokenIsHashedUsingHMACSHA256", "In order to prove that an encrypted ArmorToken is hashed using HMACSHA256\r\nAs an " +
                                                                                                                                                             "ArmorTokenHasher\r\nI want to hash an encryptedArmotToken using HMACSHA256", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("Hash an encrypted ArmorToken using HMACSHA256")]
        [NUnit.Framework.CategoryAttribute("armorTokenHasher")]
        public virtual void HashAnEncryptedArmorTokenUsingHMACSHA256() {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Hash an encrypted ArmorToken using HMACSHA256", new string[] {
                "armorTokenHasher"
            });
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I have supplied an encrypted ArmorToken for hash using HMACSHA256", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 9
            testRunner.And("I have hashed the encrypted ArmorToken using HMACSHA256", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 10
            testRunner.When("I verify the hashed ArmorToken\'s HMACSHA256 signature", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 11
            testRunner.Then("The HMACSHA256 hash signature will be valid", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion