#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenValidator : ICommand {
        private readonly ArmorTokenValidationStep[] armorTokenValidationSteps;

        public object Result { get { return ValidationStepResult; } }
        public ValidationStepResult ValidationStepResult { get; private set; }

        public ArmorTokenValidator(params ArmorTokenValidationStep[] armorTokenValidationSteps) {
            this.armorTokenValidationSteps = armorTokenValidationSteps;
        }

        public void Execute() {
            var stepCount = armorTokenValidationSteps.Length;
            var i = 0;
            ArmorTokenValidationStep currentStep;

            do {
                currentStep = armorTokenValidationSteps[i];
                currentStep.Execute();

                i++;
            } while (i < stepCount && currentStep.ValidationStepResult.IsValid);

            ValidationStepResult = currentStep.ValidationStepResult;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}