#region Includes

using System;
using System.Collections.Generic;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public class TimeStampClaimValidator : ClaimValidator {
        private readonly double timeout;

        public TimeStampClaimValidator(IEnumerable<Claim> claims, double timeout) : base(claims) {
            this.timeout = timeout;
        }

        public override void Execute() {
            Claim claim;

            if (!Validate("TimeStamp", out claim)) {
                IsValid = false;
                return;
            }

            var timeStamp = Convert.ToDateTime(claim.Value);
            var difference = DateTime.UtcNow.Subtract(timeStamp).TotalMilliseconds;

            IsValid = timeout > difference;
        }
    }
}