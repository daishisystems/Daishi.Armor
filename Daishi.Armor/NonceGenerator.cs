#region Includes

using System;
using System.Security.Cryptography;

#endregion

namespace Daishi.Armor {
    public class NonceGenerator : ICommand {
        public object Result { get { return Nonce; } }
        public Int64 Nonce { get; private set; }

        public void Execute() {
            using (var nonceGenerator = new RNGCryptoServiceProvider()) {
                var data = new byte[32];
                nonceGenerator.GetBytes(data);

                Nonce = BitConverter.ToInt64(data, 0);
            }
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}