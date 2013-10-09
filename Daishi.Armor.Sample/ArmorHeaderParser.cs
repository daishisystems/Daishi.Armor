#region Includes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

#endregion

namespace Daishi.Armor.Sample {
    public class ArmorHeaderParser : ICommand {
        private readonly HttpRequestHeaders headers;

        public object Result { get { return ArmorTokenHeader; } }
        public ArmorTokenHeader ArmorTokenHeader { get; private set; }

        public ArmorHeaderParser(HttpRequestHeaders headers) {
            this.headers = headers;
        }

        public void Execute() {
            IEnumerable<string> authHeaders;
            headers.TryGetValues("Authorization", out authHeaders);

            var armorTokenHeader = authHeaders.SingleOrDefault(h => h.StartsWith("ARMOR"));
            var isValid = armorTokenHeader != null;

            ArmorTokenHeader = new ArmorTokenHeader {
                IsValid = isValid,
                ArmorToken = isValid ? armorTokenHeader.Replace("ARMOR ", string.Empty) : string.Empty
            };
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}