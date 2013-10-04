#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenValidator : ICommand {
        private readonly byte[] armorToken;
        private readonly ArmorTokenValidationStep first;

        public object Result { get { return ArmorTokenValidationStepResult; } }
        public ArmorTokenValidationStepResult ArmorTokenValidationStepResult { get; private set; }

        public ArmorTokenValidator(byte[] armorToken, ArmorTokenValidationStep first) {
            this.armorToken = armorToken;
            this.first = first;
        }

        public void Execute() {
            first.Validate(armorToken);
            ArmorTokenValidationStepResult = first.ArmorTokenValidationStepResult;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}