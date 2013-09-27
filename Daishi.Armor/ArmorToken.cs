#region Includes

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;

#endregion

namespace Daishi.Armor {
    public class ArmorToken {
        private List<Claim> claims;

        public IEnumerable<Claim> Claims { get { return claims; } set { claims = (List<Claim>) value; } }

        public ArmorToken() {}

        public ArmorToken(string userId, string platform, Int64 nonce) {
            claims = new List<Claim> {
                new Claim("UserId", userId),
                new Claim("Platform", platform),
                new Claim("TimeStamp", DateTime.UtcNow.ToString("o")),
                new Claim("Nonce", nonce.ToString(CultureInfo.CurrentCulture))
            };
        }

        public ArmorToken(string userId, string platform, Int64 nonce, IEnumerable<Claim> claims) : this(userId, platform, nonce) {
            this.claims.AddRange(claims);
        }
    }
}