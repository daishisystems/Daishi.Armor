#region Includes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public abstract class ClaimValidator : ICommand {
        private readonly IEnumerable<Claim> claims;

        public object Result { get { return IsValid; } }
        public bool IsValid { get; protected set; }

        protected ClaimValidator(IEnumerable<Claim> claims) {
            this.claims = claims;
        }

        public abstract void Execute();

        protected bool Validate(string claimName, out Claim claim) {
            claim = claims.SingleOrDefault(c => c.Type.Equals(claimName));
            return claim != null;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}