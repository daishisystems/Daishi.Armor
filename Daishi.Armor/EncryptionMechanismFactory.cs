namespace Daishi.Armor {
    public abstract class EncryptionMechanismFactory {
        protected readonly byte[] key;
        protected readonly byte[] input;

        protected EncryptionMechanismFactory(byte[] key, byte[] input) {
            this.key = key;
            this.input = input;
        }

        public abstract EncryptionMechanism CreateEncryptionMechanism();
    }
}