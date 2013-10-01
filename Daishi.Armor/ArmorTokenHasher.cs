#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenHasher : ICommand {
        private readonly HashingMode hashingMode;
        private readonly byte[] key;
        private readonly byte[] encryptedArmorToken;

        private HashingMechanism hashingMechanism;

        public object Result { get { return HashedArmorToken; } }
        public string HashedArmorToken { get; private set; }

        public ArmorTokenHasher(HashingMode hashingMode, byte[] key, byte[] encryptedArmorToken) {
            this.hashingMode = hashingMode;
            this.key = key;
            this.encryptedArmorToken = encryptedArmorToken;
        }

        public void Execute() {
            switch (hashingMode) {
                case HashingMode.HMACSHA512:
                    hashingMechanism = new HMACSHA512HashingMechanism(key, encryptedArmorToken);
                    hashingMechanism.Execute();

                    HashedArmorToken = hashingMechanism.Output;
                    break;
            }
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}