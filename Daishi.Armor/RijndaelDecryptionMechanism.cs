#region Includes

using System.Security.Cryptography;
using System.Text;

#endregion

namespace Daishi.Armor {
    public class RijndaelDecryptionMechanism : EncryptionMechanism {
        public RijndaelDecryptionMechanism(byte[] key, byte[] input) : base(key, input) {}

        public override void Execute() {
            var cipherTextParser = new CipherTextParser(input);
            cipherTextParser.Execute();

            using (var provider = new RijndaelManaged()) {
                var transformer = new CryptographicTransformer(cipherTextParser.Cipher.Message, provider.CreateDecryptor(key, cipherTextParser.Cipher.InitialisationVector));
                transformer.Execute();

                Output = Encoding.UTF8.GetString(transformer.Cipher);
            }
        }
    }
}