#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class CipherTextParser : ICommand {
        private readonly byte[] cipherText;

        public object Result { get { return Cipher; } }
        public Cipher Cipher { get; private set; }

        public CipherTextParser(byte[] cipherText) {
            this.cipherText = cipherText;
        }

        public void Execute() {
            var initialisationVector = new byte[16];
            Buffer.BlockCopy(cipherText, 0, initialisationVector, 0, 16);

            var message = new byte[cipherText.Length - initialisationVector.Length];
            Buffer.BlockCopy(cipherText, initialisationVector.Length, message, 0, message.Length);

            Cipher = new Cipher {
                InitialisationVector = initialisationVector,
                Message = message
            };
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}