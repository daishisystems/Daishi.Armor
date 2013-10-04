namespace Daishi.Armor {
    public class EmptyArmorTokenGenerationStep : ArmorTokenGenerationStep {
        private string armorToken;

        public EmptyArmorTokenGenerationStep() : base(null) {}

        public override void Execute() {
            ArmorTokenGenerationStepResult = new ArmorTokenGenerationStepResult {
                Output = armorToken
            };
        }

        public override void Generate(object armorToken) {
            this.armorToken = (string) armorToken;
            Execute();
        }
    }
}