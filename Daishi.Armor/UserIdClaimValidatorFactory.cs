#region Includes

using System.Collections.Generic;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public class UserIdClaimValidatorFactory : ClaimValidatorFactory<UserIdClaimValidator, string> {
        public override UserIdClaimValidator CreateClaimValidator(IEnumerable<Claim> claims) {
            return new UserIdClaimValidator(claims);
        }
    }
}