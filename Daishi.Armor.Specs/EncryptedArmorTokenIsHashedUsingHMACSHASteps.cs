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
    public class EncryptedArmorTokenIsHashedUsingHMACSHASteps {
        private ArmorToken originalArmorToken;
        private ArmorToken deserialisedArmorToken;

        private readonly byte[] encryptionKey = new byte[32];
        private readonly byte[] hashingKey = new byte[32];

        private string encryptedArmorToken;
        private string hashedArmorToken;

        private HashingMode hashingMode;

        [Given(@"I have supplied an encrypted ArmorToken for hash using HMACSHA(.*)")]
        public void GivenIHaveSuppliedAnEncryptedArmorTokenForHashUsingHMACSHA(int p0) {
            originalArmorToken = new ArmorToken("user.name@company.com", "myPlatform", 0, new[] {new Claim("Dummy", "Claim")});

            var armorTokenSerialisor = new ArmorTokenSerialisor(originalArmorToken);
            armorTokenSerialisor.Execute();

            using (var provider = new RNGCryptoServiceProvider()) provider.GetBytes(encryptionKey);
            var encryptionMechanismFactory = new RijndaelEncryptionMechanismFactory(encryptionKey, Encoding.UTF8.GetBytes(armorTokenSerialisor.SerialisedArmorToken));

            var armorTokenEncryptor = new ArmorTokenEncryptor(encryptionMechanismFactory);
            armorTokenEncryptor.Execute();

            encryptedArmorToken = armorTokenEncryptor.Output;
        }

        [Given(@"I have hashed the encrypted ArmorToken using HMACSHA(.*)")]
        public void GivenIHaveHashedTheEncryptedArmorTokenUsingHMACSHA(int p0) {
            HashingMechanismFactory hashingMechanismFactory;

            switch (p0) {
                case 256:
                    hashingMechanismFactory = new HMACSHA256HashingMechanismFactory(hashingKey, Convert.FromBase64String(encryptedArmorToken));
                    break;
                case 512:
                    hashingMechanismFactory = new HMACSHA512HashingMechanismFactory(hashingKey, Convert.FromBase64String(encryptedArmorToken));
                    break;
                default:
                    throw new NotImplementedException("Invalid Hashing Mode.");
            }

            using (var provider = new RNGCryptoServiceProvider()) provider.GetBytes(hashingKey);

            var armorTokenHasher = new ArmorTokenHasher(hashingMechanismFactory);
            armorTokenHasher.Execute();

            hashedArmorToken = armorTokenHasher.HashedArmorToken;
        }

        [When(@"I verify the hashed ArmorToken's HMACSHA(.*) signature")]
        public void WhenIVerifyTheHashedArmorTokenSHMACSHASignature(int p0) {
            switch (p0) {
                case 256:
                    hashingMode = HashingMode.HMACSHA256;
                    break;
                case 512:
                    hashingMode = HashingMode.HMACSHA512;
                    break;
                default:
                    throw new NotImplementedException("Invalid Hashing Mode.");
            }

            var signatureParser = new SignatureParser(hashingMode, Convert.FromBase64String(hashedArmorToken));
            signatureParser.Execute();

            byte[] hash;

            switch (p0) {
                case 256:
                    using (var hmac = new HMACSHA256(hashingKey)) hash = hmac.ComputeHash(signatureParser.Signature.Message);
                    break;
                case 512:
                    using (var hmac = new HMACSHA512(hashingKey)) hash = hmac.ComputeHash(signatureParser.Signature.Message);
                    break;
                default:
                    throw new NotImplementedException("Invalid Hashing Mode.");
            }

            Assert.AreEqual(signatureParser.Signature.Hash, hash);
            var encryptionMechanismFactory = new RijndaelDecryptionMechanismFactory(encryptionKey, signatureParser.Signature.Message);

            var armorTokenDecryptor = new ArmorTokenEncryptor(encryptionMechanismFactory);
            armorTokenDecryptor.Execute();

            var decrypted = armorTokenDecryptor.Output;

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