namespace Daishi.Armor {
    public class ClaimsArmorTokenValidationStep : ArmorTokenValidationStep {
        private readonly UserIdClaimValidatorFactory userIdClaimValidatorFactory;
        private readonly TimeStampClaimValidatorFactory timeStampClaimValidatorFactory;
        private readonly string userId;
        private readonly int timeout;
        private ArmorToken armorToken;

        public ClaimsArmorTokenValidationStep(ArmorTokenValidationStep next, UserIdClaimValidatorFactory userIdClaimValidatorFactory,
                                              TimeStampClaimValidatorFactory timeStampClaimValidatorFactory, string userId, int timeout) : base(next) {
            this.userIdClaimValidatorFactory = userIdClaimValidatorFactory;
            this.timeStampClaimValidatorFactory = timeStampClaimValidatorFactory;
            this.userId = userId;
            this.timeout = timeout;
        }

        public override void Execute() {
            var userIdClaimValidator = userIdClaimValidatorFactory.CreateClaimValidator(armorToken.Claims);
            var timeStampClaimValidator = timeStampClaimValidatorFactory.CreateClaimValidator(armorToken.Claims);

            ValidationStepResult = new ValidationStepResult();

            if (!userIdClaimValidator.Validate(userId)) {
                ValidationStepResult.IsValid = false;
                ValidationStepResult.Message = "Tampered";
                return;
            }
            if (!timeStampClaimValidator.Validate(timeout)) {
                ValidationStepResult.IsValid = false;
                ValidationStepResult.Message = "Tampered";
                return;
            }

            ValidationStepResult.IsValid = true;
            ValidationStepResult.Message = "Untampered";
        }

        public override void Validate(object armorToken) {
            this.armorToken = (ArmorToken) armorToken;
            base.Validate(armorToken);
        }
    }
}