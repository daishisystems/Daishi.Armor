namespace Daishi.Armor {
    public class SerialisedArmorTokenValidationStep : ArmorTokenValidationStep {
        private ArmorTokenDeserialisor armorTokenDeserialisor;

        public SerialisedArmorTokenValidationStep(ArmorTokenValidationStep next) : base(next) {}

        public override void Execute() {
            armorTokenDeserialisor.Execute();
            ArmorTokenValidationStepResult = new ArmorTokenValidationStepResult {
                IsValid = true,
                Message = "Untampered",
                Output = armorTokenDeserialisor.DeserialisedArmorToken
            };
        }

        public override void Validate(object armorToken) {
            armorTokenDeserialisor = new ArmorTokenDeserialisor((string) armorToken);
            base.Validate(armorToken);
        }
    }
}