#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class HashedArmorTokenParser : ICommand {
        private readonly int hashSize;

        public byte[] HashedArmorToken { get; set; }
        public object Result { get { return ParsedArmorToken; } }
        public ParsedArmorToken ParsedArmorToken { get; private set; }

        public HashedArmorTokenParser(HashingMode hashingMode) {
            switch (hashingMode) {
                case HashingMode.HMACSHA256:
                    hashSize = 32;
                    break;
                case HashingMode.HMACSHA512:
                    hashSize = 64;
                    break;
            }
        }

        public HashedArmorTokenParser(HashingMode hashingMode, byte[] hashedArmorToken) : this(hashingMode) {
            HashedArmorToken = hashedArmorToken;
        }

        public void Execute() {
            ParsedArmorToken = new ParsedArmorToken {
                Hash = new byte[hashSize],
                ArmorToken = new byte[HashedArmorToken.Length - hashSize]
            };

            Buffer.BlockCopy(HashedArmorToken, 0, ParsedArmorToken.Hash, 0, hashSize);
            Buffer.BlockCopy(HashedArmorToken, hashSize, ParsedArmorToken.ArmorToken, 0, ParsedArmorToken.ArmorToken.Length);
        }

        public void Execute(byte[] hashedArmorToken) {
            HashedArmorToken = hashedArmorToken;
            Execute();
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}