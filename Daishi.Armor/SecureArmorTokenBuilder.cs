namespace Daishi.Armor {
    public abstract class SecureArmorTokenBuilder {
        protected readonly ArmorToken armorToken;
        protected readonly byte[] encryptionKey;
        protected readonly byte[] hashingKey;

        public string SecureArmorToken { get; protected set; }

        protected SecureArmorTokenBuilder(ArmorToken armorToken, byte[] encryptionKey, byte[] hashingKey) {
            this.armorToken = armorToken;
            this.encryptionKey = encryptionKey;
            this.hashingKey = hashingKey;
        }

        public abstract void Serialise();
        public abstract void Encrypt();
        public abstract void Hash();
    }
}