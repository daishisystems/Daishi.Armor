#region Includes

using System.Collections.Generic;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public class TimeStampClaimValidatorFactory : ClaimValidatorFactory<TimeStampClaimValidator, int> {
        public override TimeStampClaimValidator CreateClaimValidator(IEnumerable<Claim> claims) {
            return new TimeStampClaimValidator(claims);
        }
    }
}