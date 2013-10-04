#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public abstract class ArmorTokenValidationStep : ICommand {
        private readonly ArmorTokenValidationStep next;

        public object Result { get { return ArmorTokenValidationStepResult; } }
        public ArmorTokenValidationStepResult ArmorTokenValidationStepResult { get; protected set; }

        protected ArmorTokenValidationStep(ArmorTokenValidationStep next) {
            this.next = next;
        }

        public abstract void Execute();

        public virtual void Validate(object armorToken) {
            Execute();
            if (!ArmorTokenValidationStepResult.IsValid) return;

            next.Validate(ArmorTokenValidationStepResult.Output);
            ArmorTokenValidationStepResult = next.ArmorTokenValidationStepResult;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}