#region Includes

using System.Text;

#endregion

namespace Daishi.Armor {
    public class EncryptArmorTokenGenerationStep : ArmorTokenGenerationStep {
        private readonly EncryptionMechanismFactory encryptionMechanismFactory;
        private string armorToken;

        public EncryptArmorTokenGenerationStep(EncryptionMechanismFactory encryptionMechanismFactory) : base(new EmptyArmorTokenGenerationStep()) {
            this.encryptionMechanismFactory = encryptionMechanismFactory;
        }

        public EncryptArmorTokenGenerationStep(EncryptionMechanismFactory encryptionMechanismFactory, ArmorTokenGenerationStep next) : base(next) {
            this.encryptionMechanismFactory = encryptionMechanismFactory;
        }

        public override void Execute() {
            var encrytionMechanism = encryptionMechanismFactory.CreateEncryptionMechanism(Encoding.UTF8.GetBytes(armorToken));
            encrytionMechanism.Execute();

            ArmorTokenGenerationStepResult = new ArmorTokenGenerationStepResult {
                Output = encrytionMechanism.Output
            };
        }

        public override void Generate(object armorToken) {
            this.armorToken = (string) armorToken;
            base.Generate(armorToken);
        }
    }
}