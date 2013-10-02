#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class HashedArmorTokenParser : ICommand {
        private readonly int hashSize;
        private readonly byte[] hashedArmorToken;

        public object Result { get { return HashedArmorToken; } }
        public HashedArmorToken HashedArmorToken { get; private set; }

        public HashedArmorTokenParser(HashingMode hashingMode, byte[] hashedArmorToken) {
            switch (hashingMode) {
                case HashingMode.HMACSHA256:
                    hashSize = 32;
                    break;
                case HashingMode.HMACSHA512:
                    hashSize = 64;
                    break;
            }

            this.hashedArmorToken = hashedArmorToken;
        }

        public void Execute() {
            HashedArmorToken = new HashedArmorToken {
                Hash = new byte[hashSize],
                ArmorToken = new byte[hashedArmorToken.Length - hashSize]
            };

            Buffer.BlockCopy(hashedArmorToken, 0, HashedArmorToken.Hash, 0, hashSize);
            Buffer.BlockCopy(hashedArmorToken, hashSize, HashedArmorToken.ArmorToken, 0, HashedArmorToken.ArmorToken.Length);
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}