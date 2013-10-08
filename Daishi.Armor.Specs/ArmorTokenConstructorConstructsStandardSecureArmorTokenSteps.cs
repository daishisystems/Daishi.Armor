#region Includes

using System;
using System.Security.Claims;
using NUnit.Framework;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Armor.Specs {
    [Binding]
    public class ArmorTokenConstructorConstructsStandardSecureArmorTokenSteps {
        private ArmorTokenConstructor armorTokenConstructor;

        private byte[] encryptionKey;
        private byte[] hashingKey;

        private string secureArmorToken;

        [Given(@"I have supplied a StandardArmorTokenConstructor")]
        public void GivenIHaveSuppliedAStandardArmorTokenConstructor() {
            armorTokenConstructor = new ArmorTokenConstructor();
        }

        [When(@"I invoke the StandardArmorTokenConstructor")]
        public void WhenIInvokeTheStandardArmorTokenConstructor() {
            var armorToken = new ArmorToken("user.name@company.com", "myPlatform", 0, new[] {new Claim("Dummy", "Claim")});

            encryptionKey = Convert.FromBase64String("0nA6gWIoNXeeFjJFo1qi1ZlL7NI/4a6YbL8RnqMTC1A=");
            hashingKey = Convert.FromBase64String("0nA6gWIoNXeeFjJFo1qi1ZlL7NI/4a6YbL8RnqMTC1A=");

            var builder = new StandardSecureArmorTokenBuilder(armorToken, encryptionKey, hashingKey);
            armorTokenConstructor.Construct(builder);

            secureArmorToken = builder.SecureArmorToken;
        }

        [Then(@"The result should yield a valid, standard, secure ArmorToken")]
        public void ThenTheResultShouldYieldAValidStandardSecureArmorToken() {
            var step4 = new ClaimsArmorTokenValidationStep(new EmptyEncryptedArmorTokenValidationStep(), new UserIdClaimValidatorFactory("user.name@company.com"), new TimeStampClaimValidatorFactory(300000));
            var step3 = new SerialisedArmorTokenValidationStep(new ArmorTokenDeserialisor(), step4);
            var step2 = new EncryptedArmorTokenValidationStep(step3, new RijndaelDecryptionMechanismFactory(encryptionKey));
            var step1 = new HashedArmorTokenValidationStep(step2, new HashedArmorTokenParser(HashingMode.HMACSHA512), new HMACSHA512ArmorTokenHasherFactory(hashingKey));

            var armorTokenValidator = new ArmorTokenValidator(Convert.FromBase64String(secureArmorToken), step1);
            armorTokenValidator.Execute();

            Assert.IsTrue(armorTokenValidator.ArmorTokenValidationStepResult.IsValid);
        }
    }
}