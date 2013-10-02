#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class CipherTextParser : ICommand {
        private readonly byte[] cipherText;

        public object Result { get { return Cipher; } }
        public Cipher Cipher { get; private set; }
        public byte[] Input { get { return cipherText; } }

        public CipherTextParser(byte[] cipherText) {
            this.cipherText = cipherText;
        }

        public void Execute() {
            Cipher = new Cipher {
                InitialisationVector = new byte[16],
                Message = new byte[cipherText.Length - 16]
            };

            Buffer.BlockCopy(cipherText, 0, Cipher.InitialisationVector, 0, 16);
            Buffer.BlockCopy(cipherText, Cipher.InitialisationVector.Length, Cipher.Message, 0, Cipher.Message.Length);
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}