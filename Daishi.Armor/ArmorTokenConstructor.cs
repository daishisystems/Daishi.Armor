namespace Daishi.Armor {
    public class ArmorTokenConstructor {
        public void Construct(SecureArmorTokenBuilder builder) {
            builder.Serialise();
            builder.Encrypt();
            builder.Hash();
        }
    }
}