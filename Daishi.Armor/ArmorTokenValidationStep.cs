#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public abstract class ArmorTokenValidationStep : ICommand {
        public object Result { get { return ValidationStepResult; } }
        public ValidationStepResult ValidationStepResult { get; protected set; }

        public abstract void Execute();

        public virtual void Validate(byte[] armorToken) {
            Execute();
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }

    public class ArmorTokenValidationStepManager {
        public byte[] ArmorToken { get; set; }

        public ArmorTokenValidationStep[] ArmorTokenValidationSteps { get; set; }
    }
}