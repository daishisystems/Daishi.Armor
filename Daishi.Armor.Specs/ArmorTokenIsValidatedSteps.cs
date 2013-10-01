#region Includes

using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
        public void WhenIValidateTheValidArmorToken() {}

        [Then(@"I should return a valid result")]
        public void ThenIShouldReturnAValidResult() {
            ScenarioContext.Current.Pending();
        }
    }
}