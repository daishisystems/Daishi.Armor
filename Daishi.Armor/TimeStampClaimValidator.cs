#region Includes

using System;
using System.Collections.Generic;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public class TimeStampClaimValidator : ClaimValidator<int> {
        public TimeStampClaimValidator(IEnumerable<Claim> claims) : base(claims) {}

        public override bool Validate(int timeout) {
            Claim claim;
            if (!Validate("TimeStamp", out claim)) {
                return false;
            }

            var timeStamp = Convert.ToDateTime(claim.Value);
            var difference = DateTime.UtcNow.Subtract(timeStamp).TotalMilliseconds;

            return timeout > difference;
        }
    }
}