#region Includes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;

#endregion

namespace Daishi.Armor.Sample {
    public class ArmorAuthorizeAttribute : AuthorizeAttribute {
        protected override bool IsAuthorized(HttpActionContext actionContext) {
            var principal = (ClaimsIdentity) Thread.CurrentPrincipal.Identity;
            var userId = principal.Claims.Single(c => c.Type.Equals("UserId")).Value;

            IEnumerable<string> headers;
            actionContext.Request.Headers.TryGetValues("Authorization", out headers);

            var armorTokenHeader = headers.Single(h => h.StartsWith("ARMOR")).Replace("ARMOR ", string.Empty);

            var encryptionKey = Convert.FromBase64String("0nA6gWIoNXeeFjJFo1qi1ZlL7NI/4a6YbL8RnqMTC1A=");
            var hashingKey = Convert.FromBase64String("0nA6gWIoNXeeFjJFo1qi1ZlL7NI/4a6YbL8RnqMTC1A=");

            var validateClaims = new ClaimsArmorTokenValidationStep(new EmptyEncryptedArmorTokenValidationStep(), new UserIdClaimValidatorFactory(userId), new TimeStampClaimValidatorFactory(300000));
            var deserialise = new SerialisedArmorTokenValidationStep(new ArmorTokenDeserialisor(), validateClaims);
            var decrypt = new EncryptedArmorTokenValidationStep(deserialise, new RijndaelDecryptionMechanismFactory(encryptionKey));
            var validateSignature = new HashedArmorTokenValidationStep(decrypt, new HashedArmorTokenParser(HashingMode.HMACSHA512), new HMACSHA512ArmorTokenHasherFactory(hashingKey));

            var armorTokenValidator = new ArmorTokenValidator(Convert.FromBase64String(armorTokenHeader), validateSignature);
            armorTokenValidator.Execute();

            if (!armorTokenValidator.ArmorTokenValidationStepResult.IsValid) return false;
            var armorToken = new ArmorToken("paul.mooney@hmhco.com", "HMH", 0, new[] {new Claim("Dummy", "Claim")});

            var step3 = new HashArmorTokenGenerationStep(new HMACSHA512HashingMechanismFactory(hashingKey), new EmptyArmorTokenGenerationStep());
            var step2 = new EncryptArmorTokenGenerationStep(new RijndaelEncryptionMechanismFactory(encryptionKey), step3);
            var step1 = new SerialiseArmorTokenGenerationStep(new ArmorTokenSerialisor(), step2);
            var armorTokenGenerator = new ArmorTokenGenerator(armorToken, step1);

            armorTokenGenerator.Execute();
            var hashedArmorToken = armorTokenGenerator.ArmorToken;

            var response = actionContext.Request.CreateResponse(HttpStatusCode.OK);

            response.Headers.Add("ARMOR", hashedArmorToken);
            actionContext.Response = response;

            return armorTokenValidator.ArmorTokenValidationStepResult.IsValid;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext) {
            var response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            response.ReasonPhrase = "ARMOR rejected the request.";

            actionContext.Response = response;
        }
    }
}