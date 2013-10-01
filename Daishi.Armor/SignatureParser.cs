#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class SignatureParser : ICommand {
        private readonly int hashSize;
        protected readonly byte[] hashedArmorToken;

        public object Result { get { return Signature; } }
        public Signature Signature { get; protected set; }

        public SignatureParser(HashingMode hashingMode, byte[] hashedArmorToken) {
            switch (hashingMode) {
                case HashingMode.HMACSHA512:
                    hashSize = 64;
                    break;
            }

            this.hashedArmorToken = hashedArmorToken;
        }

        public void Execute() {
            Signature = new Signature {
                Hash = new byte[hashSize],
                Message = new byte[hashedArmorToken.Length - hashSize]
            };

            Buffer.BlockCopy(hashedArmorToken, 0, Signature.Hash, 0, hashSize);
            Buffer.BlockCopy(hashedArmorToken, hashSize, Signature.Message, 0, Signature.Message.Length);
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}