#region Includes

using System.Collections.Generic;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public class TimeStampClaimValidatorFactory : ClaimValidatorFactory {
        private readonly double timeout;

        public TimeStampClaimValidatorFactory(double timeout) {
            this.timeout = timeout;
        }

        public override ClaimValidator CreateClaimValidator(IEnumerable<Claim> claims) {
            return new TimeStampClaimValidator(claims, timeout);
        }
    }
}