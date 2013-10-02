#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenValidator : ICommand {
        private readonly string securedArmorToken;

        public object Result { get { return IsValid; } }
        public bool IsValid { get; private set; }

        public ArmorTokenValidator(string securedArmorToken) {
            this.securedArmorToken = securedArmorToken;
        }

        // todo: Chain of Responsibility, DI - look at CipherTextParser as dependency
        public void Execute() {}

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}