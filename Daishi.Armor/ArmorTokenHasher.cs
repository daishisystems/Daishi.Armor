#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenHasher : ICommand {
        private readonly HashingMode hashingMode;
        private readonly byte[] key;
        private readonly byte[] encryptedArmorToken;

        private HashingMechanism hashingMechanism;

        private HashingMechanismFactory hashingMechanismFactory;

        public object Result { get { return HashedArmorToken; } }
        public string HashedArmorToken { get; private set; }

        public ArmorTokenHasher(HashingMode hashingMode, byte[] key, byte[] encryptedArmorToken) {
            this.hashingMode = hashingMode;
            this.key = key;
            this.encryptedArmorToken = encryptedArmorToken;
        }

        public void Execute() {
            switch (hashingMode) {
                case HashingMode.HMACSHA256:
                    hashingMechanism = new HMACSHA256HashingMechanism(key, encryptedArmorToken);
                    break;
                case HashingMode.HMACSHA512:
                    hashingMechanism = new HMACSHA512HashingMechanism(key, encryptedArmorToken);
                    break;
            }

            hashingMechanism.Execute();
            HashedArmorToken = hashingMechanism.Output;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }

    internal abstract class HashingMechanismFactory {
        protected readonly byte[] key;
        protected readonly byte[] input;

        protected HashingMechanismFactory(byte[] key, byte[] input) {
            this.key = key;
            this.input = input;
        }

        public abstract HashingMechanism CreateHashingMechanism();
    }

    internal class HMACSHA256HashingMechanismFactory : HashingMechanismFactory {
        public HMACSHA256HashingMechanismFactory(byte[] key, byte[] input) : base(key, input) {}

        public override HashingMechanism CreateHashingMechanism() {
            return new HMACSHA256HashingMechanism(key, input);
        }
    }

    internal class HMACSHA512HashingMechanismFactory : HashingMechanismFactory {
        public HMACSHA512HashingMechanismFactory(byte[] key, byte[] input) : base(key, input) {}

        public override HashingMechanism CreateHashingMechanism() {
            return new HMACSHA512HashingMechanism(key, input);
        }
    }
}