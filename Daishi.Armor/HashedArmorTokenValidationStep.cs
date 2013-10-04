#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class HashedArmorTokenValidationStep : ArmorTokenValidationStep {
        private readonly HashedArmorTokenParser hashedArmorTokenParser;
        private readonly ArmorTokenHasherFactory armorTokenHasherFactory;

        public HashedArmorTokenValidationStep(ArmorTokenValidationStep next, HashedArmorTokenParser hashedArmorTokenParser, ArmorTokenHasherFactory armorTokenHasherFactory) : base(next) {
            this.hashedArmorTokenParser = hashedArmorTokenParser;
            this.armorTokenHasherFactory = armorTokenHasherFactory;
        }

        public override void Execute() {
            hashedArmorTokenParser.Execute();
            var armorTokenHasher = armorTokenHasherFactory.CreateArmorTokenHasher(hashedArmorTokenParser.ParsedArmorToken.ArmorToken);

            armorTokenHasher.Execute();
            var isValid = armorTokenHasher.Hash.Equals(Convert.ToBase64String(hashedArmorTokenParser.ParsedArmorToken.Hash));

            ArmorTokenValidationStepResult = new ArmorTokenValidationStepResult {
                IsValid = isValid,
                Message = isValid ? "Untampered" : "Tampered",
                Output = hashedArmorTokenParser.ParsedArmorToken.ArmorToken
            };
        }

        public override void Validate(object armorToken) {
            hashedArmorTokenParser.HashedArmorToken = (byte[]) armorToken;
            base.Validate(armorToken);
        }
    }
}