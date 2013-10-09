#region Includes

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

#endregion

/*      
        todo: Add commands to encapsulate logic below
*/

namespace Daishi.Armor.Sample {
    public class ArmorAuthorizeAttribute : AuthorizeAttribute {
        protected override bool IsAuthorized(HttpActionContext actionContext) {
            var principal = (ClaimsIdentity) Thread.CurrentPrincipal.Identity;
            var userId = principal.Claims.Single(c => c.Type.Equals("UserId")).Value;
            var platform = principal.Claims.Single(c => c.Type.Equals("Platform")).Value;

            var armorHeaderParser = new ArmorHeaderParser(actionContext.Request.Headers);
            armorHeaderParser.Execute();

            if (!armorHeaderParser.ArmorTokenHeader.IsValid) return false;

            var encryptionKey = Convert.FromBase64String("0nA6gWIoNXeeFjJFo1qi1ZlL7NI/4a6YbL8RnqMTC1A=");
            var hashingKey = Convert.FromBase64String("0nA6gWIoNXeeFjJFo1qi1ZlL7NI/4a6YbL8RnqMTC1A=");

            var secureArmorTokenValidator = new SecureArmorTokenValidator(armorHeaderParser.ArmorTokenHeader.ArmorToken, encryptionKey, hashingKey, userId, 10000000000);
            secureArmorTokenValidator.Execute();

            if (!secureArmorTokenValidator.ArmorTokenValidationStepResult.IsValid) return false;

            var nonceGenerator = new NonceGenerator();
            nonceGenerator.Execute();

            var armorToken = new ArmorToken(userId, platform, nonceGenerator.Nonce, new[] {new Claim("Another", "Claim")});

            var armorTokenConstructor = new ArmorTokenConstructor();
            var standardSecureArmorTokenBuilder = new StandardSecureArmorTokenBuilder(armorToken, encryptionKey, hashingKey);
            armorTokenConstructor.Construct(standardSecureArmorTokenBuilder);

            var secureArmorToken = standardSecureArmorTokenBuilder.SecureArmorToken;
            HttpContext.Current.Response.AppendHeader("ARMOR", secureArmorToken);

            return secureArmorTokenValidator.ArmorTokenValidationStepResult.IsValid;
        }
    }
}