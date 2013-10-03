namespace Daishi.Armor {
    public class ClaimsArmorTokenValidationStep : ArmorTokenValidationStep {
        private readonly ClaimValidatorFactory userIdClaimValidatorFactory;
        private readonly ClaimValidatorFactory timeStampClaimValidatorFactory;
        private ArmorToken armorToken;

        public ClaimsArmorTokenValidationStep(ArmorTokenValidationStep next, ClaimValidatorFactory userIdClaimValidatorFactory,
                                              ClaimValidatorFactory timeStampClaimValidatorFactory) : base(next) {
            this.userIdClaimValidatorFactory = userIdClaimValidatorFactory;
            this.timeStampClaimValidatorFactory = timeStampClaimValidatorFactory;
        }

        public override void Execute() {
            var userIdClaimValidator = userIdClaimValidatorFactory.CreateClaimValidator(armorToken.Claims);
            userIdClaimValidator.Execute();

            if (!userIdClaimValidator.IsValid) {
                ValidationStepResult = new ValidationStepResult {
                    IsValid = false,
                    Message = "Tampered"
                };

                return;
            }

            var timeStampClaimValidator = timeStampClaimValidatorFactory.CreateClaimValidator(armorToken.Claims);
            timeStampClaimValidator.Execute();

            if (!timeStampClaimValidator.IsValid) {
                ValidationStepResult = new ValidationStepResult {
                    IsValid = false,
                    Message = "Tampered"
                };

                return;
            }

            ValidationStepResult = new ValidationStepResult {
                IsValid = true,
                Message = "Untampered"
            };
        }

        public override void Validate(object armorToken) {
            this.armorToken = (ArmorToken) armorToken;
            base.Validate(armorToken);
        }
    }
}