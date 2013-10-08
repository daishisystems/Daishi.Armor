namespace Daishi.Armor {
    public class SerialiseArmorTokenGenerationStep : ArmorTokenGenerationStep {
        private readonly ArmorTokenSerialisor armorTokenSerialisor;
        private ArmorToken armorToken;

        public SerialiseArmorTokenGenerationStep(ArmorTokenSerialisor armorTokenSerialisor) : base(new EmptyArmorTokenGenerationStep()) {
            this.armorTokenSerialisor = armorTokenSerialisor;
        }

        public SerialiseArmorTokenGenerationStep(ArmorTokenSerialisor armorTokenSerialisor, ArmorTokenGenerationStep next) : base(next) {
            this.armorTokenSerialisor = armorTokenSerialisor;
        }

        public override void Execute() {
            armorTokenSerialisor.Serialise(armorToken);

            ArmorTokenGenerationStepResult = new ArmorTokenGenerationStepResult {
                Output = armorTokenSerialisor.SerialisedArmorToken
            };
        }

        public override void Generate(object armorToken) {
            this.armorToken = (ArmorToken) armorToken;
            base.Generate(armorToken);
        }
    }
}