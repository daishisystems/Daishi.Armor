#region Includes

using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Armor.Specs {
    [Binding]
    public class ArmorTokenIsValidatedSteps {
        private string serialisedArmorToken;
        private string encryptedArmorToken;
        private string hashedArmorToken;

        private readonly byte[] encryptionKey = new byte[32];
        private readonly byte[] hashingKey = new byte[32];

        private ArmorTokenValidator armorTokenValidator;

        [Given(@"I have supplied a valid ArmorToken")]
        public void GivenIHaveSuppliedAValidArmorToken() {
            var armorToken = new ArmorToken("user.name@company.com", "myPlatform", 0, new[] {new Claim("Dummy", "Claim")});
            var armorTokenSerialisor = new ArmorTokenSerialisor(armorToken);

            armorTokenSerialisor.Execute();
            serialisedArmorToken = armorTokenSerialisor.SerialisedArmorToken;
        }

        [Given(@"I have encrypted the valid ArmorToken")]
        public void GivenIHaveEncryptedTheValidArmorToken() {
            using (var provider = new RNGCryptoServiceProvider()) provider.GetBytes(encryptionKey);

            var encryptionMechanismFactory = new RijndaelEncryptionMechanismFactory(encryptionKey, Encoding.UTF8.GetBytes(serialisedArmorToken));
            var armorTokenEncryptor = new ArmorTokenEncryptor(encryptionMechanismFactory);

            armorTokenEncryptor.Execute();
            encryptedArmorToken = armorTokenEncryptor.Output;
        }

        [Given(@"I have hashed the valid ArmorToken")]
        public void GivenIHaveHashedTheValidArmorToken() {
            using (var provider = new RNGCryptoServiceProvider()) provider.GetBytes(hashingKey);

            var hashingMechanismFactory = new HMACSHA512HashingMechanismFactory(hashingKey, Convert.FromBase64String(encryptedArmorToken));
            var armorTokenHasher = new ArmorTokenHasher(hashingMechanismFactory);

            armorTokenHasher.Execute();
            hashedArmorToken = armorTokenHasher.HashedArmorToken;
        }

        [When(@"I validate the valid ArmorToken")]
        public void WhenIValidateTheValidArmorToken() {
            var step3 = new SerialisedArmorTokenValidationStep(new EmptyEncryptedArmorTokenValidationStep());
            var step2 = new EncryptedArmorTokenValidationStep(step3, new RijndaelDecryptionMechanismFactory(encryptionKey));
            var step1 = new HashedArmorTokenValidationStep(step2, new HashedArmorTokenParser(HashingMode.HMACSHA512), new HMACSHA512ArmorTokenHasherFactory(hashingKey));

            armorTokenValidator = new ArmorTokenValidator(Convert.FromBase64String(hashedArmorToken), new ArmorTokenValidationStep[] {step1, step2});
            armorTokenValidator.Execute();
        }

        [Then(@"I should return a valid result")]
        public void ThenIShouldReturnAValidResult() {
            Assert.IsTrue(armorTokenValidator.ValidationStepResult.IsValid);
        }
    }
}