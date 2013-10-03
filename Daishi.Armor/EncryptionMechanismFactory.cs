namespace Daishi.Armor {
    public abstract class EncryptionMechanismFactory {
        protected readonly byte[] key;
        protected byte[] input;

        protected EncryptionMechanismFactory(byte[] key) {
            this.key = key;
        }

        protected EncryptionMechanismFactory(byte[] key, byte[] input) : this(key) {
            this.input = input;
        }

        public abstract EncryptionMechanism CreateEncryptionMechanism();

        public EncryptionMechanism CreateEncryptionMechanism(byte[] input) {
            this.input = input;
            return CreateEncryptionMechanism();
        }
    }
}