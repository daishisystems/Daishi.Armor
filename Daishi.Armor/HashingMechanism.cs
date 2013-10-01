#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public abstract class HashingMechanism : ICommand {
        protected readonly byte[] key;
        protected readonly byte[] input;

        public object Result { get; private set; }
        public string Output { get; set; }

        protected HashingMechanism(byte[] key, byte[] input) {
            this.key = key;
            this.input = input;
        }

        public abstract void Execute();

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}