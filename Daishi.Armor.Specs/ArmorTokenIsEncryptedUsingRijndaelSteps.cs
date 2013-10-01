#region Includes

using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Armor.Specs {
    [Binding]
    public class ArmorTokenIsEncryptedUsingRijndaelSteps {
        private ArmorToken originalArmorToken;
        private string serialisedArmorToken;
        private readonly byte[] key = new byte[32];
        private string encryptedArmorToken;
        private string decryptedArmorToken;
        private ArmorToken deserialisedArmorToken;

        [Given(@"I have supplied an ArmorToken for encryption")]
        public void GivenIHaveSuppliedAnArmorTokenForEncryption() {
            originalArmorToken = new ArmorToken("user.name@company.com", "myPlatform", 0, new[] {new Claim("Dummy", "Claim")});
        }

        [Given(@"I have serialised the ArmorToken")]
        public void GivenIHaveSerialisedTheArmorToken() {
            var serialisor = new ArmorTokenSerialisor(originalArmorToken);
            serialisor.Execute();

            serialisedArmorToken = serialisor.SerialisedArmorToken;
        }

        [Given(@"I have encrypted the token using Rijndael encryption")]
        public void GivenIHaveEncryptedTheTokenUsingRijndaelEncryption() {
            using (var provider = new RNGCryptoServiceProvider()) provider.GetBytes(key);
            var encryptionMechanismFactory = new RijndaelEncryptionMechanismFactory(key, Encoding.UTF8.GetBytes(serialisedArmorToken));

            var encryptor = new ArmorTokenEncryptor(encryptionMechanismFactory);
            encryptor.Execute();

            encryptedArmorToken = encryptor.Output;
        }

        [When(@"I decrypt the token using Rijndael encryption")]
        public void WhenIDecryptTheTokenUsingRijndaelEncryption() {
            var encryptionMechanismFactory = new RijndaelDecryptionMechanismFactory(key, Convert.FromBase64String(encryptedArmorToken));

            var decryptor = new ArmorTokenEncryptor(encryptionMechanismFactory);
            decryptor.Execute();

            decryptedArmorToken = decryptor.Output;
        }

        [When(@"I parse the decrypted token")]
        public void WhenIParseTheDecryptedToken() {
            var deserialisor = new ArmorTokenDeserialisor(decryptedArmorToken);
            deserialisor.Execute();

            deserialisedArmorToken = deserialisor.DeserialisedArmorToken;
        }

        [Then(@"the parsed token should equal the original")]
        public void ThenTheParsedTokenShouldEqualTheOriginal() {
            foreach (var expectedClaim in originalArmorToken.Claims.Where(c => !c.Type.Equals("TimeStamp"))) {
                Claim claim;

                Assert.NotNull(claim = deserialisedArmorToken.Claims.SingleOrDefault(c => c.Type.Equals(expectedClaim.Type)));
                Assert.AreEqual(expectedClaim.Value, claim.Value);
            }
        }
    }
}