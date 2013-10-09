#region Includes

using System;
using System.Security.Claims;
using NUnit.Framework;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Armor.Specs {
    [Binding]
    public class SecureArmorTokenValidatorValidatesSecureArmorTokenSteps {
        private ArmorTokenConstructor armorTokenConstructor;
        private ArmorTokenValidationStepResult armorTokenValidationStepResult;

        private byte[] encryptionKey;
        private byte[] hashingKey;

        private string secureArmorToken;

        [Given(@"I have provided a secure ArmorToken")]
        public void GivenIHaveProvidedASecureArmorToken() {
            var armorToken = new ArmorToken("user.name@company.com", "myPlatform", 0, new[] {new Claim("Dummy", "Claim")});

            encryptionKey = Convert.FromBase64String("0nA6gWIoNXeeFjJFo1qi1ZlL7NI/4a6YbL8RnqMTC1A=");
            hashingKey = Convert.FromBase64String("0nA6gWIoNXeeFjJFo1qi1ZlL7NI/4a6YbL8RnqMTC1A=");

            armorTokenConstructor = new ArmorTokenConstructor();
            var builder = new StandardSecureArmorTokenBuilder(armorToken, encryptionKey, hashingKey);
            armorTokenConstructor.Construct(builder);

            secureArmorToken = builder.SecureArmorToken;
        }

        [When(@"I validate the secure ArmorToken")]
        public void WhenIValidateTheSecureArmorToken() {
            var standardArmorTokenValidator = new SecureArmorTokenValidator(secureArmorToken, encryptionKey, hashingKey, "user.name@company.com", 300000);
            standardArmorTokenValidator.Execute();

            armorTokenValidationStepResult = standardArmorTokenValidator.ArmorTokenValidationStepResult;
        }

        [Then(@"The validated ArmorToken should be valid\.")]
        public void ThenTheValidatedArmorTokenShouldBeValid_() {
            Assert.IsTrue(armorTokenValidationStepResult.IsValid);
        }
    }
}