#region Includes

using System.Security.Cryptography;

#endregion

namespace Daishi.Armor {
    public class HMACSHA512HashingMechanism : HashingMechanism {
        public HMACSHA512HashingMechanism(byte[] key, byte[] input) : base(key, input) {
            if (!key.Length.Equals(64)) {
                // todo: Throw KeySizeException.
            }
        }

        public override void Execute() {
            using (var hmac = new HMACSHA512(key)) ComputeHash(hmac);
        }
    }
}