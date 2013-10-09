namespace Daishi.Armor {
    public class SerialisedArmorTokenValidationStep : ArmorTokenValidationStep {
        private readonly ArmorTokenDeserialisor armorTokenDeserialisor;
        private string armorToken;

        public SerialisedArmorTokenValidationStep(ArmorTokenDeserialisor armorTokenDeserialisor) : base(new EmptyEncryptedArmorTokenValidationStep()) {
            this.armorTokenDeserialisor = armorTokenDeserialisor;
        }

        public SerialisedArmorTokenValidationStep(ArmorTokenDeserialisor armorTokenDeserialisor, ArmorTokenValidationStep next) : base(next) {
            this.armorTokenDeserialisor = armorTokenDeserialisor;
        }

        public override void Execute() {
            armorTokenDeserialisor.Deserialise(armorToken);
            ArmorTokenValidationStepResult = new ArmorTokenValidationStepResult {
                IsValid = true,
                Message = "Untampered",
                Output = armorTokenDeserialisor.DeserialisedArmorToken
            };
        }

        public override void Validate(object armorToken) {
            this.armorToken = (string) armorToken;
            base.Validate(armorToken);
        }
    }
}