namespace Daishi.Armor {
    public class EmptyEncryptedArmorTokenValidationStep : ArmorTokenValidationStep {
        public EmptyEncryptedArmorTokenValidationStep() : base(null) {}

        public override void Execute() {
            ArmorTokenValidationStepResult = new ArmorTokenValidationStepResult {
                IsValid = true,
                Message = "Untampered"
            };
        }

        public override void Validate(object armorToken) {
            Execute();
        }
    }
}