#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenHasher : ICommand {
        private readonly HashingMechanismFactory hashingMechanismFactory;

        public object Result { get { return HashedArmorToken; } }
        public string HashedArmorToken { get; private set; }

        public ArmorTokenHasher(HashingMechanismFactory hashingMechanismFactory) {
            this.hashingMechanismFactory = hashingMechanismFactory;
        }

        public void Execute() {
            var hashingMechanism = hashingMechanismFactory.CreateHashingMechanism();

            hashingMechanism.Execute();
            HashedArmorToken = hashingMechanism.Output;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}