namespace Daishi.Armor {
    public abstract class HashingMechanismFactory {
        protected readonly byte[] key;
        protected byte[] input;

        protected HashingMechanismFactory(byte[] key) {
            this.key = key;
        }

        protected HashingMechanismFactory(byte[] key, byte[] input) : this(key) {
            this.input = input;
        }

        public abstract HashingMechanism CreateHashingMechanism();

        public abstract HashingMechanism CreateHashingMechanism(byte[] input);
    }
}