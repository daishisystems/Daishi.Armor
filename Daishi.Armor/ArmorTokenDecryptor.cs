#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenDecryptor : ICommand {
        private readonly EncryptionMode encryptionMode;
        private readonly byte[] key;
        private readonly byte[] encryptedArmorToken;

        private EncryptionMechanism encryptionMechanism;

        public object Result { get { return DecryptedArmorToken; } }
        public string DecryptedArmorToken { get; private set; }

        public ArmorTokenDecryptor(EncryptionMode encryptionMode, byte[] key, byte[] encryptedArmorToken) {
            this.encryptionMode = encryptionMode;
            this.key = key;
            this.encryptedArmorToken = encryptedArmorToken;
        }

        public void Execute() {
            switch (encryptionMode) {
                case EncryptionMode.Rijndael:
                    encryptionMechanism = new RijndaelDecryptionMechanism(key, encryptedArmorToken);
                    break;
            }

            encryptionMechanism.Execute();
            DecryptedArmorToken = encryptionMechanism.Output;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}