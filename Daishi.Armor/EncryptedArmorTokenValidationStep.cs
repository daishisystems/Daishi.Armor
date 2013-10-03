#region Includes

using System.Security.Cryptography;

#endregion

namespace Daishi.Armor {
    public class EncryptedArmorTokenValidationStep : ArmorTokenValidationStep {
        private readonly EncryptionMechanismFactory encryptionMechanismFactory;
        private EncryptionMechanism encryptionMechanism;

        public EncryptedArmorTokenValidationStep(EncryptionMechanismFactory encryptionMechanismFactory) {
            this.encryptionMechanismFactory = encryptionMechanismFactory;
        }

        public override void Execute() {
            try {
                encryptionMechanism.Execute();
                ValidationStepResult = new ValidationStepResult {
                    IsValid = true,
                    Message = "Untampered"
                };
            }
            catch (CryptographicException) {
                ValidationStepResult = new ValidationStepResult {
                    IsValid = false,
                    Message = "Tampered"
                };
            }
        }

        public override void Validate(byte[] armorToken) {
            encryptionMechanism = encryptionMechanismFactory.CreateEncryptionMechanism(armorToken);
            base.Validate(armorToken);
        }
    }
}