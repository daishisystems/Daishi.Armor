#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class HashedArmorTokenValidationStep : ArmorTokenValidationStep {
        private readonly HashedArmorTokenParser hashedArmorTokenParser;
        private readonly ArmorTokenHasherFactory armorTokenHasherFactory;

        public HashedArmorTokenValidationStep(HashedArmorTokenParser hashedArmorTokenParser, ArmorTokenHasherFactory armorTokenHasherFactory) {
            this.hashedArmorTokenParser = hashedArmorTokenParser;
            this.armorTokenHasherFactory = armorTokenHasherFactory;
        }

        public override void Execute() {
            hashedArmorTokenParser.Execute();
            var armorTokenHasher = armorTokenHasherFactory.CreateArmorTokenHasher(hashedArmorTokenParser.HashedArmorToken.ArmorToken);

            armorTokenHasher.Execute();
            var isValid = armorTokenHasher.Hash.Equals(Convert.ToBase64String(hashedArmorTokenParser.HashedArmorToken.Hash));

            ValidationStepResult = new ValidationStepResult {
                IsValid = isValid,
                Message = isValid ? "Untampered" : "Tampered",
                Output = hashedArmorTokenParser.HashedArmorToken.ArmorToken
            };
        }
    }
}