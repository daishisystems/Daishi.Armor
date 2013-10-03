#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public abstract class ArmorTokenValidationStep : ICommand {
        private readonly ArmorTokenValidationStep next;

        public object Result { get { return ValidationStepResult; } }
        public ValidationStepResult ValidationStepResult { get; protected set; }

        protected ArmorTokenValidationStep(ArmorTokenValidationStep next) {
            this.next = next;
        }

        public abstract void Execute();

        public virtual void Validate(object armorToken) {
            Execute();
            if (!ValidationStepResult.IsValid) return;

            next.Validate(ValidationStepResult.Output);
            ValidationStepResult = next.ValidationStepResult;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }

    public class SerialisedArmorTokenValidationStep : ArmorTokenValidationStep {
        private ArmorTokenDeserialisor armorTokenDeserialisor;

        public SerialisedArmorTokenValidationStep(ArmorTokenValidationStep next) : base(next) {}

        public override void Execute() {
            armorTokenDeserialisor.Execute();
            ValidationStepResult = new ValidationStepResult {
                IsValid = true,
                Message = "Untampered"
            };
        }

        public override void Validate(object armorToken) {
            armorTokenDeserialisor = new ArmorTokenDeserialisor((string) armorToken);
            base.Validate(armorToken);
        }
    }
}