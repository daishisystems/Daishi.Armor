#region Designer generated code

#region Includes

using TechTalk.SpecFlow;

#endregion

#pragma warning disable

namespace Daishi.Armor.Specs {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("ArmorTokenIsEncryptedUsingRijndael")]
    public partial class ArmorTokenIsEncryptedUsingRijndaelFeature {
        private static TechTalk.SpecFlow.ITestRunner testRunner;

#line 1 "ArmorTokenIsEncryptedUsingRijndael.feature"
#line hidden

        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup() {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            var featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ArmorTokenIsEncryptedUsingRijndael", "In order to prove that a given ArmorToken is encrypted using Rijndael encryption\r" +
                                                                                                                                                     "\nAs an ArmorTokenEncryptor\r\nI want to encrypt an ArmorToken using Rijndael encry" +
                                                                                                                                                     "ption", ProgrammingLanguage.CSharp, ((string[]) (null)));
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
        [NUnit.Framework.DescriptionAttribute("Encrypt an ArmorToken using Rijndael encryption")]
        [NUnit.Framework.CategoryAttribute("armorTokenEncryptor")]
        public virtual void EncryptAnArmorTokenUsingRijndaelEncryption() {
            var scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Encrypt an ArmorToken using Rijndael encryption", new string[] {
                "armorTokenEncryptor"
            });
#line 7
            this.ScenarioSetup(scenarioInfo);
#line 8
            testRunner.Given("I have supplied an ArmorToken for encryption", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Given ");
#line 9
            testRunner.And("I have serialised the ArmorToken", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 10
            testRunner.And("I have encrypted the token using Rijndael encryption", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 11
            testRunner.When("I decrypt the token using Rijndael encryption", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "When ");
#line 12
            testRunner.And("I parse the decrypted token", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "And ");
#line 13
            testRunner.Then("the parsed token should equal the original", ((string) (null)), ((TechTalk.SpecFlow.Table) (null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}

#pragma warning restore

#endregion