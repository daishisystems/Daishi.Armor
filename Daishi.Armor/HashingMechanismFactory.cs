namespace Daishi.Armor {
    public abstract class HashingMechanismFactory {
        protected readonly byte[] key;
        protected readonly byte[] input;

        protected HashingMechanismFactory(byte[] key, byte[] input) {
            this.key = key;
            this.input = input;
        }

        public abstract HashingMechanism CreateHashingMechanism();
    }
}