#region Includes

using System.Collections.Generic;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public class UserIdClaimValidatorFactory : ClaimValidatorFactory {
        private readonly string userId;

        public UserIdClaimValidatorFactory(string userId) {
            this.userId = userId;
        }

        public override ClaimValidator CreateClaimValidator(IEnumerable<Claim> claims) {
            return new UserIdClaimValidator(claims, userId);
        }
    }
}