namespace Daishi.Armor {
    public class EmptyEncryptedArmorTokenValidationStep : ArmorTokenValidationStep {
        private ArmorToken armorToken;

        public EmptyEncryptedArmorTokenValidationStep() : base(null) {}

        public override void Execute() {
            ArmorTokenValidationStepResult = new ArmorTokenValidationStepResult {
                IsValid = true,
                Message = "Untampered",
                Output = armorToken
            };
        }

        public override void Validate(object armorToken) {
            this.armorToken = (ArmorToken) armorToken;
            Execute();
        }
    }
}