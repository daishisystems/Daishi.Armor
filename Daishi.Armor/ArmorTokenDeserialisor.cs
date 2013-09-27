#region Includes

using System;
using System.IO;
using System.Linq;
using System.Text;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenDeserialisor : ICommand {
        private readonly string serialisedArmorToken;

        public object Result { get { return DeserialisedArmorToken; } }
        public ArmorToken DeserialisedArmorToken { get; private set; }

        public ArmorTokenDeserialisor(string serialisedArmorToken) {
            this.serialisedArmorToken = serialisedArmorToken;
        }

        public void Execute() {
            var armorTokenBytes = Encoding.UTF8.GetBytes(serialisedArmorToken);

            using (Stream stream = new MemoryStream(armorTokenBytes)) {
                var parser = new ArmorTokenJsonParser(stream, "claims");
                parser.Parse();

                DeserialisedArmorToken = parser.Result.SingleOrDefault();
            }
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}