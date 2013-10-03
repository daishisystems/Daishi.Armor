#region Includes

using System.Security.Cryptography;

#endregion

namespace Daishi.Armor {
    public class EncryptedArmorTokenValidationStep : ArmorTokenValidationStep {
        private readonly EncryptionMechanismFactory encryptionMechanismFactory;
        private EncryptionMechanism encryptionMechanism;

        public EncryptedArmorTokenValidationStep(ArmorTokenValidationStep next, EncryptionMechanismFactory encryptionMechanismFactory) : base(next) {
            this.encryptionMechanismFactory = encryptionMechanismFactory;
        }

        public override void Execute() {
            try {
                encryptionMechanism.Execute();
                ValidationStepResult = new ValidationStepResult {
                    IsValid = true,
                    Message = "Untampered",
                    Output = encryptionMechanism.Output
                };
            }
            catch (CryptographicException) {
                ValidationStepResult = new ValidationStepResult {
                    IsValid = false,
                    Message = "Tampered"
                };
            }
        }

        public override void Validate(object armorToken) {
            encryptionMechanism = encryptionMechanismFactory.CreateEncryptionMechanism((byte[]) armorToken);
            base.Validate(armorToken);
        }
    }
}