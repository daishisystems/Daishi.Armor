#region Includes

using System.Security.Cryptography;

#endregion

namespace Daishi.Armor {
    public abstract class CryptographicTransformerFactory {
        public abstract CryptographicTransformer CreateCryptographicTransformer(SymmetricAlgorithm provider, byte[] input, byte[] key, byte[] initialisationVector);
    }
}