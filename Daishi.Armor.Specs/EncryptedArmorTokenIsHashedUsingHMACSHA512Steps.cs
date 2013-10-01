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
    public class EncryptedArmorTokenIsHashedUsingHMACSHA512Steps {
        private ArmorToken originalArmorToken;
        private ArmorToken deserialisedArmorToken;
        private readonly byte[] encryptionKey = new byte[32];
        private readonly byte[] hashingKey = new byte[64];
        private string encryptedArmorToken;
        private string hashedArmorToken;

        [Given(@"I have supplied an encrypted ArmorToken for hash using HMACSHA(.*)")]
        public void GivenIHaveSuppliedAnEncryptedArmorTokenForHashUsingHMACSHA(int p0) {
            originalArmorToken = new ArmorToken("user.name@company.com", "myPlatform", 0, new[] {new Claim("Dummy", "Claim")});

            var armorTokenSerialisor = new ArmorTokenSerialisor(originalArmorToken);
            armorTokenSerialisor.Execute();

            using (var provider = new RNGCryptoServiceProvider()) provider.GetBytes(encryptionKey);

            var armorTokenEncryptor = new ArmorTokenEncryptor(EncryptionMode.Rijndael, encryptionKey, armorTokenSerialisor.SerialisedArmorToken);
            armorTokenEncryptor.Execute();

            encryptedArmorToken = armorTokenEncryptor.EncryptedArmorToken;
        }

        [Given(@"I have hashed the encrypted ArmorToken using HMACSHA(.*)")]
        public void GivenIHaveHashedTheEncryptedArmorTokenUsingHMACSHA(int p0) {
            using (var provider = new RNGCryptoServiceProvider()) provider.GetBytes(hashingKey);

            var armorTokenHasher = new ArmorTokenHasher(HashingMode.HMACSHA512, hashingKey, Convert.FromBase64String(encryptedArmorToken));
            armorTokenHasher.Execute();

            hashedArmorToken = armorTokenHasher.HashedArmorToken;
        }

        [When(@"I verify the hashed ArmorToken's HMACSHA(.*) signature")]
        public void WhenIVerifyTheHashedArmorTokenSHMACSHASignature(int p0) {
            var signatureParser = new SignatureParser(HashingMode.HMACSHA512, Convert.FromBase64String(hashedArmorToken));
            signatureParser.Execute();

            byte[] hash;
            using (var hmac = new HMACSHA512(hashingKey)) hash = hmac.ComputeHash(signatureParser.Signature.Message);

            Assert.AreEqual(signatureParser.Signature.Hash, hash);

            var armorTokenDecryptor = new ArmorTokenDecryptor(EncryptionMode.Rijndael, encryptionKey, signatureParser.Signature.Message);
            armorTokenDecryptor.Execute();

            var decrypted = armorTokenDecryptor.DecryptedArmorToken;

            var armorTokenDeserialisor = new ArmorTokenDeserialisor(decrypted);
            armorTokenDeserialisor.Execute();

            deserialisedArmorToken = armorTokenDeserialisor.DeserialisedArmorToken;
        }

        [Then(@"The HMACSHA(.*) hash signature will be valid")]
        public void ThenTheHMACSHAHashSignatureWillBeValid(int p0) {
            foreach (var expectedClaim in originalArmorToken.Claims.Where(c => !c.Type.Equals("TimeStamp"))) {
                Claim claim;

                Assert.NotNull(claim = deserialisedArmorToken.Claims.SingleOrDefault(c => c.Type.Equals(expectedClaim.Type)));
                Assert.AreEqual(expectedClaim.Value, claim.Value);
            }
        }
    }
}