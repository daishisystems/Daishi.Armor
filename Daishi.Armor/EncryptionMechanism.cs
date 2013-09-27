#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public abstract class EncryptionMechanism : ICommand {
        protected readonly byte[] key;
        protected readonly byte[] input;

        public object Result { get { return Output; } }
        public string Output { get; protected set; }

        protected EncryptionMechanism(Byte[] key, byte[] input) {
            this.key = key;
            this.input = input;
        }

        public abstract void Execute();

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}