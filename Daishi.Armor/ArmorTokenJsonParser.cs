#region Includes

using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using Daishi.JsonParser;
using Newtonsoft.Json;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenJsonParser : JsonParser<ArmorToken> {
        private readonly List<Claim> claims = new List<Claim>();

        public ArmorTokenJsonParser(Stream json, string jsonPropertyName) : base(json, jsonPropertyName) {}

        protected override void Build(ArmorToken parsable, JsonTextReader reader) {
            if (!reader.TokenType.Equals(JsonToken.PropertyName)) return;

            var claimType = reader.Value.ToString();
            reader.Read();
            var claimValue = reader.Value.ToString();

            claims.Add(new Claim(claimType, claimValue));
        }

        protected override bool IsBuilt(ArmorToken parsable, JsonTextReader reader) {
            var isBuilt = reader.TokenType.Equals(JsonToken.EndArray);
            if (isBuilt) parsable.Claims = claims;

            return isBuilt || base.IsBuilt(parsable, reader);
        }
    }
}