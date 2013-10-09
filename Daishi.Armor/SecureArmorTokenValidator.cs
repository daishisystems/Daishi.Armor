#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class SecureArmorTokenValidator : ICommand {
        private readonly string secureArmorToken;
        private readonly byte[] encryptionKey;
        private readonly byte[] hashingKey;
        private readonly string userId;
        private readonly long timeout;

        public object Result { get { return ArmorTokenValidationStepResult; } }
        public ArmorTokenValidationStepResult ArmorTokenValidationStepResult { get; private set; }

        public SecureArmorTokenValidator(string secureArmorToken, byte[] encryptionKey, byte[] hashingKey, string userId, long timeout) {
            this.secureArmorToken = secureArmorToken;
            this.encryptionKey = encryptionKey;
            this.hashingKey = hashingKey;
            this.userId = userId;
            this.timeout = timeout;
        }

        public void Execute() {
            var validate = new ClaimsArmorTokenValidationStep(new EmptyEncryptedArmorTokenValidationStep(), new UserIdClaimValidatorFactory(userId), new TimeStampClaimValidatorFactory(timeout));
            var deserialise = new SerialisedArmorTokenValidationStep(new ArmorTokenDeserialisor(), validate);
            var decrypt = new EncryptedArmorTokenValidationStep(deserialise, new RijndaelDecryptionMechanismFactory(encryptionKey));
            var hash = new HashedArmorTokenValidationStep(decrypt, new HashedArmorTokenParser(HashingMode.HMACSHA512), new HMACSHA512ArmorTokenHasherFactory(hashingKey));

            var armorTokenValidator = new ArmorTokenValidator(Convert.FromBase64String(secureArmorToken), hash);
            armorTokenValidator.Execute();

            ArmorTokenValidationStepResult = armorTokenValidator.ArmorTokenValidationStepResult;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}