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
            var armorTokenHasher = armorTokenHasherFactory.CreateArmorTokenHasher(hashedArmorTokenParser.ParsedArmorToken.ArmorToken);

            armorTokenHasher.Execute();
            var isValid = armorTokenHasher.Hash.Equals(Convert.ToBase64String(hashedArmorTokenParser.ParsedArmorToken.Hash));

            ValidationStepResult = new ValidationStepResult {
                IsValid = isValid,
                Message = isValid ? "Untampered" : "Tampered",
                Output = hashedArmorTokenParser.ParsedArmorToken.ArmorToken
            };
        }

        public override void Validate(byte[] armorToken) {
            hashedArmorTokenParser.HashedArmorToken = armorToken;
            base.Validate(armorToken);
        }
    }
}