#region Includes

using System;
using System.IO;
using Newtonsoft.Json;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenSerialisor : ICommand {
        private ArmorToken armorToken;

        public object Result { get { return SerialisedArmorToken; } }
        public string SerialisedArmorToken { get; private set; }

        public ArmorTokenSerialisor() {}

        public ArmorTokenSerialisor(ArmorToken armorToken) {
            this.armorToken = armorToken;
        }

        public void Execute() {
            var stringWriter = new StringWriter();

            using (var jsonTextWriter = new JsonTextWriter(stringWriter)) {
                jsonTextWriter.Formatting = Formatting.None;

                jsonTextWriter.WriteStartObject();
                jsonTextWriter.WritePropertyName("armortoken");
                jsonTextWriter.WriteStartObject();
                jsonTextWriter.WritePropertyName("claims");
                jsonTextWriter.WriteStartArray();

                foreach (var claim in armorToken.Claims) {
                    jsonTextWriter.WriteStartObject();
                    jsonTextWriter.WritePropertyName(claim.Type);
                    jsonTextWriter.WriteValue(claim.Value);
                    jsonTextWriter.WriteEndObject();
                }

                jsonTextWriter.WriteEndArray();
                jsonTextWriter.WriteEndObject();
                jsonTextWriter.WriteEndObject();

                SerialisedArmorToken = stringWriter.ToString();
            }
        }

        public void Serialise() {
            Execute();
        }

        public void Serialise(ArmorToken armorToken) {
            this.armorToken = armorToken;
            Execute();
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}