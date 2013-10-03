#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenValidator : ICommand {
        private readonly byte[] armorToken;
        private readonly ArmorTokenValidationStep[] armorTokenValidationSteps;

        public object Result { get { return ValidationStepResult; } }
        public ValidationStepResult ValidationStepResult { get; private set; }

        public ArmorTokenValidator(byte[] armorToken, ArmorTokenValidationStep[] armorTokenValidationSteps) {
            this.armorToken = armorToken;
            this.armorTokenValidationSteps = armorTokenValidationSteps;
        }

        public void Execute() {
            //var stepCount = armorTokenValidationSteps.Length;
            //var i = 0;
            //ArmorTokenValidationStep currentStep;

            //do {
            //    currentStep = armorTokenValidationSteps[i];
            //    currentStep.Validate(armorToken);
            //    armorToken = currentStep.ValidationStepResult.Output;

            //    i++;
            //} while (i < stepCount && currentStep.ValidationStepResult.IsValid);

            //ValidationStepResult = currentStep.ValidationStepResult;

            armorTokenValidationSteps[0].Validate(armorToken);
            ValidationStepResult = armorTokenValidationSteps[0].ValidationStepResult;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}