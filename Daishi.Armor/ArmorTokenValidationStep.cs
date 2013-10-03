#region Includes

using System;
using System.Linq;

#endregion

namespace Daishi.Armor {
    public abstract class ArmorTokenValidationStep : ICommand {
        private readonly ArmorTokenValidationStep next;

        public object Result { get { return ValidationStepResult; } }
        public ValidationStepResult ValidationStepResult { get; protected set; }

        protected ArmorTokenValidationStep(ArmorTokenValidationStep next) {
            this.next = next;
        }

        public abstract void Execute();

        public virtual void Validate(object armorToken) {
            Execute();
            if (!ValidationStepResult.IsValid) return;

            next.Validate(ValidationStepResult.Output);
            ValidationStepResult = next.ValidationStepResult;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }

    public class ClaimsArmorTokenValidationStep : ArmorTokenValidationStep {
        private readonly string userId;
        private readonly int timeout;
        private ArmorToken armorToken;

        public ClaimsArmorTokenValidationStep(ArmorTokenValidationStep next, string userId, int timeout) : base(next) {
            this.userId = userId;
            this.timeout = timeout;
        }

        public override void Execute() {
            var isValid = true;

            var userIdClaim = armorToken.Claims.SingleOrDefault(c => c.Type.Equals("UserId"));
            if (userIdClaim == null) isValid = false;

            var timeStampClaim = armorToken.Claims.SingleOrDefault(c => c.Type.Equals("TimeStamp"));
            if (timeStampClaim == null) isValid = false;

            if (!userId.Equals(userIdClaim.Value)) isValid = false;

            var timeStamp = Convert.ToDateTime(timeStampClaim.Value);
            var difference = DateTime.UtcNow.Subtract(timeStamp).TotalMilliseconds;

            if (difference > timeout) isValid = false;

            ValidationStepResult = new ValidationStepResult {
                IsValid = isValid
            };
        }

        public override void Validate(object armorToken) {
            this.armorToken = (ArmorToken) armorToken;
            base.Validate(armorToken);
        }
    }
}