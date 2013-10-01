#region Designer generated code

#region Includes

using TechTalk.SpecFlow;

#endregion

#pragma warning disable

namespace Daishi.Armor.Specs {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("EncryptedArmorTokenIsHashedUsingHMACSHA512")]
    public partial class EncryptedArmorTokenIsHashedUsingHMACSHA512Feature {
        private static TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "EncryptedArmorTokenIsHashedUsingHMACSHA512.feature"
#line hidden

        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup() {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "EncryptedArmorTokenIsHashedUsingHMACSHA512", "In order to prove that an encrypted ArmorToken is hashed using HMACSHA512\r\nAs an " +
                                                                                                                                                             "ArmorTokenHasher\r\nI want to hash an encryptedArmotToken using HMACSHA512", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("Hash an encrypted ArmorToken using HMACSHA512")]
        [NUnit.Framework.CategoryAttribute("armorTokenHasher")]
        public virtual void HashAnEncryptedArmorTokenUsingHMACSHA512() {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Hash an encrypted ArmorToken using HMACSHA512", new string[] {
                "armorTokenHasher"
            });
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I have supplied an encrypted ArmorToken for hash using HMACSHA512", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 9
            testRunner.And("I have hashed the encrypted ArmorToken using HMACSHA512", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 10
            testRunner.When("I verify the hashed ArmorToken\'s HMACSHA512 signature", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 11
            testRunner.Then("The HMACSHA512 hash signature will be valid", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion