﻿#region Includes

using System.Security.Cryptography;

#endregion

namespace Daishi.Armor {
    public class HMACSHA256HashingMechanism : HashingMechanism {
        public HMACSHA256HashingMechanism(byte[] key, byte[] input) : base(key, input) {
            if (!key.Length.Equals(32)) {
                // todo: Throw KeySizeException.
            }
        }

        public override void Execute() {
            using (var hmac = new HMACSHA256(key)) ComputeHash(hmac);
        }
    }
}