namespace Daishi.Armor {
    public abstract class ArmorTokenHasherFactory {
        protected readonly byte[] key;

        protected ArmorTokenHasherFactory(byte[] key) {
            this.key = key;
        }

        public abstract ArmorTokenHasher CreateArmorTokenHasher(byte[] input);
    }
}