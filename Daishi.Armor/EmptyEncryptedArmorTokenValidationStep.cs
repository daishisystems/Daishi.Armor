namespace Daishi.Armor {
    public class EmptyEncryptedArmorTokenValidationStep : ArmorTokenValidationStep {
        public EmptyEncryptedArmorTokenValidationStep() : base(null) {}

        public override void Execute() {
            ValidationStepResult = new ValidationStepResult {
                IsValid = true,
                Message = "Untampered"
            };
        }

        public override void Validate(object armorToken) {
            Execute();
        }
    }
}