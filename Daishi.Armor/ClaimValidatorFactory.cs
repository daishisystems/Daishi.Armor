#region Includes

using System.Collections.Generic;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public abstract class ClaimValidatorFactory<TValidator, TOutput> where TValidator : ClaimValidator<TOutput> {
        public abstract TValidator CreateClaimValidator(IEnumerable<Claim> claims);
    }
}