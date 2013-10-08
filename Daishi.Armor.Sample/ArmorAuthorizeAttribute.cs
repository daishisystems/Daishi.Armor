#region Includes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

#endregion

/* 
        todo: Add Validator Builder
        todo: Add commands to encapsulate logic below
*/

namespace Daishi.Armor.Sample {
    public class ArmorAuthorizeAttribute : AuthorizeAttribute {
        protected override bool IsAuthorized(HttpActionContext actionContext) {
            var principal = (ClaimsIdentity) Thread.CurrentPrincipal.Identity;
            var userId = principal.Claims.Single(c => c.Type.Equals("UserId")).Value;
            var platform = principal.Claims.Single(c => c.Type.Equals("Platform")).Value;

            IEnumerable<string> headers;
            actionContext.Request.Headers.TryGetValues("Authorization", out headers);

            var armorTokenHeader = headers.Single(h => h.StartsWith("ARMOR")).Replace("ARMOR ", string.Empty);

            var encryptionKey = Convert.FromBase64String("0nA6gWIoNXeeFjJFo1qi1ZlL7NI/4a6YbL8RnqMTC1A=");
            var hashingKey = Convert.FromBase64String("0nA6gWIoNXeeFjJFo1qi1ZlL7NI/4a6YbL8RnqMTC1A=");

            var validateClaims = new ClaimsArmorTokenValidationStep(new EmptyEncryptedArmorTokenValidationStep(), new UserIdClaimValidatorFactory(userId), new TimeStampClaimValidatorFactory(10000000000));
            var deserialise = new SerialisedArmorTokenValidationStep(new ArmorTokenDeserialisor(), validateClaims);
            var decrypt = new EncryptedArmorTokenValidationStep(deserialise, new RijndaelDecryptionMechanismFactory(encryptionKey));
            var validateSignature = new HashedArmorTokenValidationStep(decrypt, new HashedArmorTokenParser(HashingMode.HMACSHA512), new HMACSHA512ArmorTokenHasherFactory(hashingKey));

            var armorTokenValidator = new ArmorTokenValidator(Convert.FromBase64String(armorTokenHeader), validateSignature);
            armorTokenValidator.Execute();

            if (!armorTokenValidator.ArmorTokenValidationStepResult.IsValid) return false;

            var nonceGenerator = new NonceGenerator();
            nonceGenerator.Execute();

            var armorToken = new ArmorToken(userId, platform, nonceGenerator.Nonce, new[] {new Claim("Another", "Claim")});

            var armorTokenConstructor = new ArmorTokenConstructor();
            var standardSecureArmorTokenBuilder = new StandardSecureArmorTokenBuilder(armorToken, encryptionKey, hashingKey);
            armorTokenConstructor.Construct(standardSecureArmorTokenBuilder);

            var secureArmorToken = standardSecureArmorTokenBuilder.SecureArmorToken;
            HttpContext.Current.Response.AppendHeader("ARMOR", secureArmorToken);

            return armorTokenValidator.ArmorTokenValidationStepResult.IsValid;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext) {
            var response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            response.ReasonPhrase = "ARMOR rejected the request.";

            actionContext.Response = response;
        }
    }
}