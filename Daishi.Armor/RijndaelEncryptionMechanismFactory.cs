namespace Daishi.Armor {
    public class RijndaelEncryptionMechanismFactory : EncryptionMechanismFactory {
        public RijndaelEncryptionMechanismFactory(byte[] key) : base(key) {}

        public RijndaelEncryptionMechanismFactory(byte[] key, byte[] input) : base(key, input) {}

        public override EncryptionMechanism CreateEncryptionMechanism() {
            return new RijndaelEncryptionMechanism(key, new EncryptionCryptographicTransformerFactory(), input);
        }
    }
}