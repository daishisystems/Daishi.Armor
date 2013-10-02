namespace Daishi.Armor {
    public class HMACSHA512ArmorTokenHasherFactory : ArmorTokenHasherFactory {
        public HMACSHA512ArmorTokenHasherFactory(byte[] key) : base(key) {}

        public override ArmorTokenHasher CreateArmorTokenHasher(byte[] input) {
            return new ArmorTokenHasher(new HMACSHA512HashingMechanismFactory(key, input));
        }
    }
}