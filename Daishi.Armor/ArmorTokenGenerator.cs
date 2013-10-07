#region Includes

using System;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenGenerator : ICommand {
        private readonly ArmorToken armorToken;
        private readonly ArmorTokenGenerationStep first;

        public object Result { get { return ArmorToken; } }
        public string ArmorToken { get; private set; }

        public ArmorTokenGenerator(ArmorToken armorToken, ArmorTokenGenerationStep first) {
            this.armorToken = armorToken;
            this.first = first;
        }

        public void Execute() {
            first.Generate(armorToken);
            ArmorToken = (string) first.ArmorTokenGenerationStepResult.Output;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }
}