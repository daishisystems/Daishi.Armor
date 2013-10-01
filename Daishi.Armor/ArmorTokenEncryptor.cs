#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenEncryptor : ICommand {
        private readonly EncryptionMechanismFactory encryptionMechanismFactory;

        public object Result { get { return EncryptedArmorToken; } }
        public string EncryptedArmorToken { get; private set; }

        public ArmorTokenEncryptor(EncryptionMechanismFactory encryptionMechanismFactory) {
            this.encryptionMechanismFactory = encryptionMechanismFactory;
        }

        public void Execute() {
            var encryptionMechanism = encryptionMechanismFactory.CreateEncryptionMechanism();

            encryptionMechanism.Execute();
            EncryptedArmorToken = encryptionMechanism.Output;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}