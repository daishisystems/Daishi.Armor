namespace Daishi.Armor {
    public class HMACSHA256ArmorTokenHasherFactory : ArmorTokenHasherFactory {
        public HMACSHA256ArmorTokenHasherFactory(byte[] key) : base(key) {}

        public override ArmorTokenHasher CreateArmorTokenHasher(byte[] input) {
            return new ArmorTokenHasher(new HMACSHA256HashingMechanismFactory(key, input));
        }
    }
}