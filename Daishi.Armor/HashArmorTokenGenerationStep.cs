#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class HashArmorTokenGenerationStep : ArmorTokenGenerationStep {
        private readonly HashingMechanismFactory hashingMechanismFactory;
        private byte[] armorToken;

        public HashArmorTokenGenerationStep(HashingMechanismFactory hashingMechanismFactory, ArmorTokenGenerationStep next) : base(next) {
            this.hashingMechanismFactory = hashingMechanismFactory;
        }

        public override void Execute() {
            var hashingMechanism = hashingMechanismFactory.CreateHashingMechanism(armorToken);
            hashingMechanism.Execute();

            ArmorTokenGenerationStepResult = new ArmorTokenGenerationStepResult {
                Output = hashingMechanism.Output
            };
        }

        public override void Generate(object armorToken) {
            this.armorToken = Convert.FromBase64String((string) armorToken);
            base.Generate(armorToken);
        }
    }
}