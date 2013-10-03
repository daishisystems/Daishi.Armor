#region Includes

using System.Collections.Generic;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public class UserIdClaimValidator : ClaimValidator<string> {
        public UserIdClaimValidator(IEnumerable<Claim> claims) : base(claims) {}

        public override bool Validate(string expected) {
            Claim claim;
            return Validate("UserId", out claim) && expected.Equals(claim.Value);
        }
    }
}