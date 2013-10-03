#region Includes

using System.Collections.Generic;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public class UserIdClaimValidator : ClaimValidator {
        private readonly string userId;

        public UserIdClaimValidator(IEnumerable<Claim> claims, string userId) : base(claims) {
            this.userId = userId;
        }

        public override void Execute() {
            Claim claim;
            IsValid = Validate("UserId", out claim) && userId.Equals(claim.Value);
        }
    }
}