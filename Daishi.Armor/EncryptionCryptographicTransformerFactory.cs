#region Includes

using System.Security.Cryptography;

#endregion

namespace Daishi.Armor {
    public class EncryptionCryptographicTransformerFactory : CryptographicTransformerFactory {
        public override CryptographicTransformer CreateCryptographicTransformer(SymmetricAlgorithm provider, byte[] input, byte[] key, byte[] initialisationVector) {
            return new CryptographicTransformer(input, provider.CreateEncryptor(key, initialisationVector));
        }
    }
}