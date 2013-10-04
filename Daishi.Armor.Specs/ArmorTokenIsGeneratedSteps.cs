#region Includes

using System;
using System.Security.Claims;
using System.Security.Cryptography;
using NUnit.Framework;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Armor.Specs {
    [Binding]
    public class ArmorTokenIsGeneratedSteps {
        private ArmorToken armorToken;
        private readonly byte[] encryptionKey = new byte[32];
        private readonly byte[] hashingKey = new byte[32];
        private string hashedArmorToken;

        [Given(@"I have supplied a raw ArmorToken for generation")]
        public void GivenIHaveSuppliedARawArmorTokenForGeneration() {
            armorToken = new ArmorToken("user.name@company.com", "myPlatform", 0, new[] {new Claim("Dummy", "Claim")});
        }

        [When(@"I have generated the ArmorToken")]
        public void WhenIHaveGeneratedTheArmorToken() {
            using (var provider = new RNGCryptoServiceProvider()) {
                provider.GetBytes(encryptionKey);
                provider.GetBytes(hashingKey);
            }

            var step3 = new HashArmorTokenGenerationStep(new HMACSHA512HashingMechanismFactory(hashingKey), new EmptyArmorTokenGenerationStep());
            var step2 = new EncryptArmorTokenGenerationStep(new RijndaelEncryptionMechanismFactory(encryptionKey), step3);
            var step1 = new SerialiseArmorTokenGenerationStep(new ArmorTokenSerialisor(), step2);
            var armorTokenGenerator = new ArmorTokenGenerator(armorToken, step1);

            armorTokenGenerator.Execute();
            hashedArmorToken = (string) armorTokenGenerator.ArmorTokenGenerationStepResult.Output;
        }

        [Then(@"I should be able to successfully validate the generated ArmorToken")]
        public void ThenIShouldBeAbleToSuccessfullyValidateTheGeneratedArmorToken() {
            var step4 = new ClaimsArmorTokenValidationStep(new EmptyEncryptedArmorTokenValidationStep(), new UserIdClaimValidatorFactory("user.name@company.com"), new TimeStampClaimValidatorFactory(300000));
            var step3 = new SerialisedArmorTokenValidationStep(new ArmorTokenDeserialisor(), step4);
            var step2 = new EncryptedArmorTokenValidationStep(step3, new RijndaelDecryptionMechanismFactory(encryptionKey));
            var step1 = new HashedArmorTokenValidationStep(step2, new HashedArmorTokenParser(HashingMode.HMACSHA512), new HMACSHA512ArmorTokenHasherFactory(hashingKey));

            var armorTokenValidator = new ArmorTokenValidator(Convert.FromBase64String(hashedArmorToken), step1);
            armorTokenValidator.Execute();

            Assert.IsTrue(armorTokenValidator.ArmorTokenValidationStepResult.IsValid);
        }
    }
}