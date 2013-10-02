namespace Daishi.Armor {
    public class RijndaelDecryptionMechanismFactory : EncryptionMechanismFactory {
        public RijndaelDecryptionMechanismFactory(byte[] key, byte[] input) : base(key, input) {}

        public override EncryptionMechanism CreateEncryptionMechanism() {
            return new RijndaelDecryptionMechanism(key, new DecryptionCryptographicTransformerFactory(), new CipherTextParser(input));
        }
    }
}