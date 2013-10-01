namespace Daishi.Armor {
    public class HMACSHA256HashingMechanismFactory : HashingMechanismFactory {
        public HMACSHA256HashingMechanismFactory(byte[] key, byte[] input) : base(key, input) {}

        public override HashingMechanism CreateHashingMechanism() {
            return new HMACSHA256HashingMechanism(key, input);
        }
    }
}