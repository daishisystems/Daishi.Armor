#region Includes

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public abstract class ClaimValidator<T> {
        private readonly IEnumerable<Claim> claims;

        protected ClaimValidator(IEnumerable<Claim> claims) {
            this.claims = claims;
        }

        public abstract bool Validate(T expected);

        protected bool Validate(string claimName, out Claim claim) {
            claim = claims.SingleOrDefault(c => c.Type.Equals(claimName));
            return claim != null;
        }
    }
}