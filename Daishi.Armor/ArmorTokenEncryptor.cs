#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenEncryptor : ICommand {
        private readonly EncryptionMechanismFactory encryptionMechanismFactory;

        public object Result { get { return Output; } }
        public string Output { get; private set; }

        public ArmorTokenEncryptor(EncryptionMechanismFactory encryptionMechanismFactory) {
            this.encryptionMechanismFactory = encryptionMechanismFactory;
        }

        public void Execute() {
            var encryptionMechanism = encryptionMechanismFactory.CreateEncryptionMechanism();

            encryptionMechanism.Execute();
            Output = encryptionMechanism.Output;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}