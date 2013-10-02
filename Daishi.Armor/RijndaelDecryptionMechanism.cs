#region Includes

using System.Security.Cryptography;
using System.Text;

#endregion

namespace Daishi.Armor {
    public class RijndaelDecryptionMechanism : EncryptionMechanism {
        private readonly CryptographicTransformerFactory cryptographicTransformerFactory;
        private readonly CipherTextParser cipherTextParser;

        public RijndaelDecryptionMechanism(byte[] key, CryptographicTransformerFactory cryptographicTransformerFactory, CipherTextParser cipherTextParser) : base(key, cipherTextParser.Input) {
            this.cryptographicTransformerFactory = cryptographicTransformerFactory;
            this.cipherTextParser = cipherTextParser;
        }

        public override void Execute() {
            cipherTextParser.Execute();

            using (var provider = new RijndaelManaged()) {
                var transformer = cryptographicTransformerFactory.CreateCryptographicTransformer(provider, cipherTextParser.Cipher.Message, key, cipherTextParser.Cipher.InitialisationVector);
                transformer.Execute();

                Output = Encoding.UTF8.GetString(transformer.Cipher);
            }
        }
    }
}