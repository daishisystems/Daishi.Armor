#region Includes

using System;
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
            using (var hmac = new HMACSHA512(key)) {
                var hashed = hmac.ComputeHash(input);
                var output = new byte[hashed.Length + input.Length];

                Buffer.BlockCopy(hashed, 0, output, 0, hashed.Length);
                Buffer.BlockCopy(input, 0, output, hashed.Length, input.Length);

                Output = Convert.ToBase64String(output);
            }
        }
    }
}