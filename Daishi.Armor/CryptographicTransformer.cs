#region Includes

using System;
using System.IO;
using System.Security.Cryptography;

#endregion

namespace Daishi.Armor {
    internal class CryptographicTransformer : ICommand {
        private readonly byte[] input;
        private readonly ICryptoTransform transform;

        public object Result { get { return Cipher; } }
        public byte[] Cipher { get; private set; }

        public CryptographicTransformer(byte[] input, ICryptoTransform transform) {
            this.input = input;
            this.transform = transform;
        }

        public void Execute() {
            var buffer = new MemoryStream();

            using (var stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write)) {
                stream.Write(input, 0, input.Length);
                stream.FlushFinalBlock();
                Cipher = buffer.ToArray();
            }
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}