#region Includes

using System;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace Daishi.Armor {
    public class RijndaelDecryptionMechanism : EncryptionMechanism {
        public RijndaelDecryptionMechanism(byte[] key, byte[] input) : base(key, input) {}

        public override void Execute() {
            var initialisationVector = new byte[16];
            Buffer.BlockCopy(input, 0, initialisationVector, 0, 16);

            var unobfuscatedInput = new byte[input.Length - initialisationVector.Length];
            Buffer.BlockCopy(input, initialisationVector.Length, unobfuscatedInput, 0, unobfuscatedInput.Length);

            // todo: Abstract to Parser

            using (var provider = new RijndaelManaged()) {
                var transformer = new CryptographicTransformer(unobfuscatedInput, provider.CreateDecryptor(key, initialisationVector));
                transformer.Execute();

                Output = Encoding.UTF8.GetString(transformer.Cipher);
            }
        }
    }
}