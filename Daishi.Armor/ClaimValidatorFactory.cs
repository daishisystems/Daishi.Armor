#region Includes

using System.Collections.Generic;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public abstract class ClaimValidatorFactory {
        public abstract ClaimValidator CreateClaimValidator(IEnumerable<Claim> claims);
    }
}