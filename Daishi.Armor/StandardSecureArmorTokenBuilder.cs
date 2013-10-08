namespace Daishi.Armor {
    public class StandardSecureArmorTokenBuilder : SecureArmorTokenBuilder {
        public StandardSecureArmorTokenBuilder(ArmorToken armorToken, byte[] encryptionKey, byte[] hashingKey) : base(armorToken, encryptionKey, hashingKey) {}

        public override void Serialise() {
            var serialise = new SerialiseArmorTokenGenerationStep(new ArmorTokenSerialisor());
            serialise.Generate(armorToken);
            SecureArmorToken = (string) serialise.ArmorTokenGenerationStepResult.Output;
        }

        public override void Encrypt() {
            var encrypt = new EncryptArmorTokenGenerationStep(new RijndaelEncryptionMechanismFactory(encryptionKey));
            encrypt.Generate(SecureArmorToken);
            SecureArmorToken = (string) encrypt.ArmorTokenGenerationStepResult.Output;
        }

        public override void Hash() {
            var hash = new HashArmorTokenGenerationStep(new HMACSHA512HashingMechanismFactory(hashingKey));
            hash.Generate(SecureArmorToken);
            SecureArmorToken = (string) hash.ArmorTokenGenerationStepResult.Output;
        }
    }
}