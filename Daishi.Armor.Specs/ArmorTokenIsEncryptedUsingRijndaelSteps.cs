#region Includes

using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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

            var encryptor = new ArmorTokenEncryptor(EncryptionMode.Rijndael, key, serialisedArmorToken);
            encryptor.Execute();

            encryptedArmorToken = encryptor.EncryptedArmorToken;
        }

        [When(@"I decrypt the token using Rijndael encryption")]
        public void WhenIDecryptTheTokenUsingRijndaelEncryption() {
            var decryptor = new ArmorTokenDecryptor(EncryptionMode.Rijndael, key, Convert.FromBase64String(encryptedArmorToken));
            decryptor.Execute();

            decryptedArmorToken = decryptor.DecryptedArmorToken;
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