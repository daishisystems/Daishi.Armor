#region Includes

using System;
using System.Text;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenEncryptor : ICommand {
        private readonly EncryptionMode encryptionMode;
        private readonly byte[] key;
        private readonly string serialisedArmorToken;

        private EncryptionMechanism encryptionMechanism;

        public object Result { get { return EncryptedArmorToken; } }
        public string EncryptedArmorToken { get; private set; }

        public ArmorTokenEncryptor(EncryptionMode encryptionMode, byte[] key, string serialisedArmorToken) {
            this.encryptionMode = encryptionMode;
            this.key = key;
            this.serialisedArmorToken = serialisedArmorToken;
        }

        public void Execute() {
            switch (encryptionMode) {
                case EncryptionMode.Rijndael:
                    encryptionMechanism = new RijndaelEncryptionMechanism(key, Encoding.UTF8.GetBytes(serialisedArmorToken));
                    break;
            }

            encryptionMechanism.Execute();
            EncryptedArmorToken = encryptionMechanism.Output;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}