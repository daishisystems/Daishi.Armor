﻿#region Includes

using System.Security.Claims;
using System.Security.Cryptography;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Armor.Specs {
    [Binding]
    public class ArmorTokenIsGeneratedSteps {
        private ArmorToken armorToken;
        private readonly byte[] encryptionKey = new byte[32];

        [Given(@"I have supplied a raw ArmorToken for generation")]
        public void GivenIHaveSuppliedARawArmorTokenForGeneration() {
            armorToken = new ArmorToken("user.name@company.com", "myPlatform", 0, new[] {new Claim("Dummy", "Claim")});
        }

        [When(@"I have generated the ArmorToken")]
        public void WhenIHaveGeneratedTheArmorToken() {
            using (var provider = new RNGCryptoServiceProvider()) provider.GetBytes(encryptionKey);

            var step2 = new EncryptArmorTokenGenerationStep(new RijndaelEncryptionMechanismFactory(encryptionKey), new EmptyArmorTokenGenerationStep());
            var step1 = new SerialiseArmorTokenGenerationStep(new ArmorTokenSerialisor(armorToken), step2);
            var armorTokenGenerator = new ArmorTokenGenerator(armorToken, step1);

            armorTokenGenerator.Execute();
        }

        [Then(@"I should be able to successfully validate the generated ArmorToken")]
        public void ThenIShouldBeAbleToSuccessfullyValidateTheGeneratedArmorToken() {
            ScenarioContext.Current.Pending();
        }
    }
}