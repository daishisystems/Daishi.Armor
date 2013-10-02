#region Includes

using System;
using System.Security.Cryptography;

#endregion

namespace Daishi.Armor {
    public class RijndaelEncryptionMechanism : EncryptionMechanism {
        private readonly CryptographicTransformerFactory cryptographicTransformerFactory;

        public RijndaelEncryptionMechanism(byte[] key, CryptographicTransformerFactory cryptographicTransformerFactory, byte[] input) : base(key, input) {
            this.cryptographicTransformerFactory = cryptographicTransformerFactory;
        }

        public override void Execute() {
            var initialisationVector = new byte[16];
            using (var rng = new RNGCryptoServiceProvider()) rng.GetBytes(initialisationVector);

            byte[] inputCipher;

            using (var provider = new RijndaelManaged()) {
                var transformer = cryptographicTransformerFactory.CreateCryptographicTransformer(provider, input, key, initialisationVector);
                transformer.Execute();

                inputCipher = transformer.Cipher;
            }

            var cipher = new byte[initialisationVector.Length + inputCipher.Length];

            Buffer.BlockCopy(initialisationVector, 0, cipher, 0, initialisationVector.Length);
            Buffer.BlockCopy(inputCipher, 0, cipher, initialisationVector.Length, inputCipher.Length);

            Output = Convert.ToBase64String(cipher);
        }
    }
}